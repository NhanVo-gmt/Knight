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

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            this.AddManipulator(CreateNodeContextualMenu("Add Node (Single Choice)", DSDialogueType.SingleChoice));
            this.AddManipulator(CreateNodeContextualMenu("Add Node (Multiple Choice)", DSDialogueType.MultipleChoice));
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