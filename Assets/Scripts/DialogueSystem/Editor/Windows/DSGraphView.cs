using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Window
{
    using Data.Error;
    using Elements;
    using Enumerations;
    using Utilities;
    
    public class DSGraphView : GraphView
    {
        private string graphViewPath = "Assets/Scripts/DialogueSystem/Editor/View/DSGraphViewStyles.uss";
        private string nodePath = "Assets/Scripts/DialogueSystem/Editor/View/DSNodeStyles.uss";

        private DSEditorWindow editorWindow;
        private DSSearchWindow searchWindow;

        private SerializableDictionary<string, DSNodeErrorData> ungroupedNodes;
        private SerializableDictionary<string, DSGroupErrorData> groups;
        private SerializableDictionary<Group, SerializableDictionary<string, DSNodeErrorData>> groupedNodes;
        
        public DSGraphView(DSEditorWindow dsEditorWindow)
        {
            editorWindow = dsEditorWindow;

            ungroupedNodes = new SerializableDictionary<string, DSNodeErrorData>();
            groups = new SerializableDictionary<string, DSGroupErrorData>();
            groupedNodes = new SerializableDictionary<Group, SerializableDictionary<string, DSNodeErrorData>>();
            
            AddManipulators();
            AddGridBackground();
            AddSearchWindow();
            
            OnElementsDeleted();
            OnGroupElementAdded();
            OnGroupElementRemoved();
            OnGroupRenamed();
            
            AddStyles();
        }

        private void AddSearchWindow()
        {
            if (searchWindow == null)
            {
                searchWindow = ScriptableObject.CreateInstance<DSSearchWindow>();
                searchWindow.Initialize(this);
            }

            nodeCreationRequest = context =>
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
        }

        #region Overriding Methods
        
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();

            ports.ForEach(port =>
            {
                if (startPort == port) return;
                if (startPort.node == port.node) return;
                if (startPort.direction == port.direction) return;
                compatiblePorts.Add(port);
            });
            
            return compatiblePorts;
        }
        #endregion

        #region Manipulators
        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            this.AddManipulator(CreateNodeContextualMenu("Add Node (Single Choice)", DSDialogueType.SingleChoice));
            this.AddManipulator(CreateNodeContextualMenu("Add Node (Multiple Choice)", DSDialogueType.MultipleChoice));
            
            this.AddManipulator(CreateGroupContextualMenu());
        }
        
        private IManipulator CreateGroupContextualMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction("Add Group", actionEvent => AddElement(CreateGroup("Dialogue Group", GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))))
            );
            return contextualMenuManipulator;
        }

        private IManipulator CreateNodeContextualMenu(string actionTitle, DSDialogueType dialogueType)
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction(actionTitle, actionEvent => AddElement(CreateNode(dialogueType, GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))))
            );
            return contextualMenuManipulator;
        }
        
        #endregion

        #region Element Creation
        
        public DSGroup CreateGroup(string title, Vector2 position)
        {
            DSGroup group = new DSGroup(title, position);
            AddGroup(group);
            return group;
        }

        public DSNode CreateNode(DSDialogueType dialogueType, Vector2 position)
        {
            Type nodeType = Type.GetType($"DS.Elements.DS{dialogueType}Node");
            DSNode node = (DSNode)Activator.CreateInstance(nodeType);
            
            node.Initialize(this, position);
            node.Draw();
            AddUngroupedNode(node);
            
            return node;
        }
        #endregion

        #region Callbacks

        private void OnElementsDeleted()
        {
            deleteSelection = (operationName, askUser) =>
            {
                List<DSNode> nodesToDelete = new List<DSNode>();
                List<DSGroup> groupsToDelete = new List<DSGroup>();
                
                foreach (GraphElement element in selection)
                {
                    if (element is DSNode node)
                    {
                        nodesToDelete.Add(node);
                    }
                    else if (element is DSGroup dsGroup)
                    {
                        RemoveGroup(dsGroup);
                        groupsToDelete.Add(dsGroup);
                    }
                }

                foreach (DSGroup group in groupsToDelete)
                {
                    RemoveElement(group);
                }

                foreach (DSNode node in nodesToDelete)
                {
                    if (node.Group != null)
                    {
                        node.Group.RemoveElement(node);
                    }
                    RemoveUngroupedNode(node);
                    RemoveElement(node);
                }
            };
        }

        private void OnGroupElementAdded()
        {
            elementsAddedToGroup = (group, elements) =>
            {
                foreach (GraphElement element in elements)
                {
                    if (!(element is DSNode))
                    {
                        continue;
                    }

                    DSGroup dsGroup = (DSGroup)group;
                    DSNode node = (DSNode)element;
                    
                    RemoveUngroupedNode(node);
                    AddGroupedNode(node, dsGroup);
                }
            };
        }

        private void OnGroupElementRemoved()
        {
            elementsRemovedFromGroup = (group, elements) =>
            {
                foreach (GraphElement element in elements)
                {
                    if (!(element is DSNode))
                    {
                        continue;
                    }

                    DSNode node = (DSNode)element;
                    RemoveGroupedNode(node, group);
                    AddUngroupedNode(node);
                }
            };
        }

        private void OnGroupRenamed()
        {
            groupTitleChanged = (group, newTitle) =>
            {
                DSGroup dsGroup = (DSGroup)group;
                RemoveGroup(dsGroup);
                dsGroup.oldTitle = newTitle;
                AddGroup(dsGroup);
            };
        }

        #endregion

        #region Repeated Elements
        
        public void AddUngroupedNode(DSNode node)
        {
            string nodeName = node.DialogueName;
            if (!ungroupedNodes.ContainsKey(nodeName))
            {
                DSNodeErrorData nodeErrorData = new DSNodeErrorData();
                nodeErrorData.Nodes.Add(node);
                ungroupedNodes.Add(nodeName, nodeErrorData);
                return;
            }

            List<DSNode> ungroupedNodesList = ungroupedNodes[nodeName].Nodes;
            ungroupedNodesList.Add(node);
            
            Color errorColor = ungroupedNodes[nodeName].ErrorData.Color;
            node.SetErrorStyle(errorColor);
            if (ungroupedNodesList.Count == 2)
            {
                ungroupedNodesList[0].SetErrorStyle(errorColor);
            }
        }

        public void RemoveUngroupedNode(DSNode node)
        {
            string nodeName = node.DialogueName;
            List<DSNode> ungroupedNodeList = ungroupedNodes[nodeName].Nodes;
            ungroupedNodeList.Remove(node);
            node.ResetStyle();

            if (ungroupedNodeList.Count == 1)
            {
                ungroupedNodeList[0].ResetStyle();
                return;
            }

            if (ungroupedNodeList.Count == 0)
            {
                ungroupedNodes.Remove(nodeName);
            }
        }
        
        public void AddGroup(DSGroup group)
        {
            string groupName = group.title;

            if (!groups.ContainsKey(groupName))
            {
                DSGroupErrorData groupErrorData = new DSGroupErrorData();
                groupErrorData.Groups.Add(group);
                groups.Add(groupName, groupErrorData);
                return;
            }

            List<DSGroup> groupsList = groups[groupName].Groups;
            groupsList.Add(group);

            Color errorColor = groups[groupName].ErrorData.Color;
            group.SetErrorStyle(errorColor);
            if (groupsList.Count == 2)
            {
                groupsList[0].SetErrorStyle(errorColor);
            }
        }
        
        private void RemoveGroup(DSGroup group)
        {
            string oldGroupName = group.oldTitle;
            List<DSGroup> groupsList = groups[oldGroupName].Groups;
            groupsList.Remove(group);
            group.ResetStyle();

            if (groupsList.Count == 1)
            {
                groupsList[0].ResetStyle();
                return;
            }

            if (groupsList.Count == 0)
            {
                groups.Remove(oldGroupName);
            }
        }
        
        public void AddGroupedNode(DSNode node, DSGroup group)
        {
            string nodeName = node.DialogueName;
            node.Group = group;
            
            if (!groupedNodes.ContainsKey(group))
            {
                groupedNodes.Add(group, new SerializableDictionary<string, DSNodeErrorData>());
            }
            
            if (!groupedNodes[group].ContainsKey(nodeName))
            {
                DSNodeErrorData nodeErrorData = new DSNodeErrorData();
                nodeErrorData.Nodes.Add(node);
                groupedNodes[group].Add(nodeName, nodeErrorData);
                return;
            }

            List<DSNode> groupedNodesList = groupedNodes[group][nodeName].Nodes;
            groupedNodesList.Add(node);
            Color errorColor = groupedNodes[group][nodeName].ErrorData.Color;
            node.SetErrorStyle(errorColor);

            if (groupedNodesList.Count == 2)
            {
                groupedNodesList[0].SetErrorStyle(errorColor);
            }
        }

        public void RemoveGroupedNode(DSNode node, Group group)
        {
            string nodeName = node.DialogueName;
            node.Group = null;
            
            List<DSNode> groupedNodesList = groupedNodes[group][nodeName].Nodes;
            groupedNodesList.Remove(node);
            node.ResetStyle();

            if (groupedNodesList.Count == 1)
            {
                groupedNodesList[0].ResetStyle();
            }

            if (groupedNodesList.Count == 0)
            {
                groupedNodes[group].Remove(nodeName);
                if (groupedNodes[group].Count == 0)
                {
                    groupedNodes.Remove(group);
                }
            }
        }
        
        #endregion

        #region Element Addition

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }
        
        private void AddStyles()
        {
            this.AddStyleSheets(graphViewPath, nodePath);
        }
        
        #endregion

        #region Utilities

        public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false)
        {
            Vector2 worldMousePosition = mousePosition;
            
            if (isSearchWindow)
            {
                worldMousePosition -= editorWindow.position.position;
            }
            
            Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);
            return localMousePosition;
        }

        #endregion
    }
}
