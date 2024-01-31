using System;
using System.Collections;
using System.Collections.Generic;
using DS.Elements;
using DS.Enumerations;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Window
{
    public class DSGraphView : GraphView
    {
        private string graphViewPath = "Assets/Scripts/DialogueSystem/Editor/View/DSGraphViewStyles.uss";
        private string nodePath = "Assets/Scripts/DialogueSystem/Editor/View/DSNodeStyles.uss";
        
        public DSGraphView()
        {
            AddManipulators();
            AddGridBackground();
            
            AddStyles();
        }

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
                menuEvent => menuEvent.menu.AppendAction("Add Group", actionEvent => AddElement(CreateGroup("Dialogue Group", actionEvent.eventInfo.localMousePosition)))
            );
            return contextualMenuManipulator;
        }

        private GraphElement CreateGroup(string title, Vector2 position)
        {
            Group group = new Group()
            {
                title = title
            };
            group.SetPosition(new Rect(position, Vector2.zero));
            return group;
        }

        private IManipulator CreateNodeContextualMenu(string actionTitle, DSDialogueType dialogueType)
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction(actionTitle, actionEvent => AddElement(CreateNode(dialogueType, actionEvent.eventInfo.localMousePosition)))
            );
            return contextualMenuManipulator;
        }
        
        private DSNode CreateNode(DSDialogueType dialogueType, Vector2 position)
        {
            Type nodeType = Type.GetType($"DS.Elements.DS{dialogueType}Node");
            DSNode node = (DSNode)Activator.CreateInstance(nodeType);
            
            node.Initialize(position);
            node.Draw();
            
            return node;
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }
        
        private void AddStyles()
        {
            StyleSheet graphViewStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(graphViewPath);
            StyleSheet nodeStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(nodePath);
            
            
            styleSheets.Add(graphViewStyleSheet);
            styleSheets.Add(nodeStyleSheet);
        }
    }
}
