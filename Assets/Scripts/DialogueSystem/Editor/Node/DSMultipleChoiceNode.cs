using System.Collections;
using System.Collections.Generic;
using DS.Enumerations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Elements
{
    public class DSMultipleChoiceNode : DSNode
    {
        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);

            DialogueType = DSDialogueType.MultipleChoice;
            Choices.Add("New Choice");
        }

        public override void Draw()
        {
            base.Draw();

            Button addChoiceBtn = new Button()
            {
                text = "Add Choice"
            };
            
            mainContainer.Insert(1, addChoiceBtn);
            
            foreach (string choice in Choices)
            {
                Port choicePort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
                    typeof(bool));
                choicePort.portName = "";

                Button deleteChoiceBtn = new Button()
                {
                    text = "X"
                };

                TextField choiceTextField = new TextField()
                {
                    value = choice
                };
                choiceTextField.style.flexDirection = FlexDirection.Column;
                
                choicePort.Add(choiceTextField);
                choicePort.Add(deleteChoiceBtn);
                
                outputContainer.Add(choicePort);
            }
            RefreshExpandedState();
        }
    }
    
}
