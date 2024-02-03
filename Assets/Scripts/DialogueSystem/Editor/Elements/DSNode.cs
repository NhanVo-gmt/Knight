using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Elements
{
    using Enumerations;
    using Utilities;
    using Window;
    
    public class DSNode : UnityEditor.Experimental.GraphView.Node
    {
        public string ID { get; set; }
        public string DialogueName { get; set; }
        public List<string> Choices { get; set; }
        public string Text { get; set; }
        public DSDialogueType DialogueType { get; set; }
        public DSGroup Group { get; set; }

        private DSGraphView graphView;
        private Color defaultBackgroundColor;

        public virtual void Initialize(DSGraphView dsGraphView, Vector2 position)
        {
            ID = Guid.NewGuid().ToString();
            DialogueName = "DialogueName";
            Choices = new List<string>();
            Text = "Dialogue Text.";

            graphView = dsGraphView;
            defaultBackgroundColor = new Color(29f / 255f, 29f / 255f, 30f / 255f);
            
            SetPosition(new Rect(position, Vector2.zero));
            
            mainContainer.AddToClassList("ds-node__main-container");
            extensionContainer.AddToClassList("ds-node__extension-container");
        }

        #region Overriding Methods

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Disconnect Input Ports", actionEvent => DisconnectInputPort());
            evt.menu.AppendAction("Disconnect Output Ports", actionEvent => DisconnectOutputPort());
            base.BuildContextualMenu(evt);
        }

        #endregion

        public virtual void Draw()
        {
            // Title container
            TextField dialogueNameTextField = DSElementUtility.CreateTextField(DialogueName, null, callback =>
            {
                TextField target = (TextField)callback.target;
                target.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();
                
                if (Group != null)
                {
                    DSGroup currentGroup = Group;
                    graphView.RemoveGroupedNode(this, Group);
                    DialogueName = target.value;
                    graphView.AddGroupedNode(this, currentGroup);
                }
                else
                {
                    graphView.RemoveUngroupedNode(this);
                    DialogueName = target.value;
                    graphView.AddUngroupedNode(this);
                }
            });

            dialogueNameTextField.AddClasses(
                "ds-node__text-field",
                "ds-node__filename-text-field",
                "ds-node__text-field__hidden"
            );
            
            titleContainer.Insert(0, dialogueNameTextField);
            
            // Input container
            Port inputPort = this.CreatePort("Dialogue Connection", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);
            inputContainer.Add(inputPort);
            
            // Extension container
            VisualElement customDataContainer = new VisualElement();
            customDataContainer.AddClasses("ds-node__custom-data-container");

            Foldout textFoldout = DSElementUtility.CreateFoldout("Dialogue Text");

            TextField textField = DSElementUtility.CreateTextArea(Text);

            textField.AddClasses(
                "ds-node__textfield",
                "ds-node__quote-textfield"
            );
            
            textFoldout.Add(textField);
            customDataContainer.Add(textFoldout);
            
            extensionContainer.Add(customDataContainer);
        }

        #region Utility Methods

        public void DisconnectAllPorts()
        {
            DisconnectPorts(inputContainer);
            DisconnectPorts(outputContainer);
        }

        public void DisconnectInputPort()
        {
            DisconnectPorts(inputContainer);
        }

        public void DisconnectOutputPort()
        {
            DisconnectPorts(outputContainer);
        }
        
        public void DisconnectPorts(VisualElement container)
        {
            foreach (Port port in container.Children())
            {
                if (port.connected)
                {
                    graphView.DeleteElements(port.connections);
                }
            }
        }

        public void SetErrorStyle(Color color)
        {
            mainContainer.style.backgroundColor = color;
        }

        public void ResetStyle()
        {
            mainContainer.style.backgroundColor = defaultBackgroundColor;
        }
        #endregion
    }
}
