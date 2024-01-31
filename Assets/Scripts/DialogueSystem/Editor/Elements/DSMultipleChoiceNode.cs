using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Elements
{
    using Window;
    using Utilities;
    using Enumerations;
    
    public class DSMultipleChoiceNode : DSNode
    {
        public override void Initialize(DSGraphView dsGraphView, Vector2 position)
        {
            base.Initialize(dsGraphView, position);

            DialogueType = DSDialogueType.MultipleChoice;
            Choices.Add("New Choice");
        }

        public override void Draw()
        {
            base.Draw();

            Button addChoiceBtn = DSElementUtility.CreateButton("Add Choice", () =>
            {
                Port choicePort = CreateChoicePort("New Choice");
                Choices.Add("New Choice");
                outputContainer.Add(choicePort);
            });
            addChoiceBtn.AddToClassList("ds-node__button");
            
            mainContainer.Insert(1, addChoiceBtn);
            
            foreach (string choice in Choices)
            {
                Port choicePort = CreateChoicePort(choice);
                
                outputContainer.Add(choicePort);
            }
            RefreshExpandedState();
        }

        private Port CreateChoicePort(string choice)
        {
            Port choicePort = this.CreatePort("", Orientation.Horizontal, Direction.Output, Port.Capacity.Single);
                
            Button deleteChoiceBtn = DSElementUtility.CreateButton("X");
            deleteChoiceBtn.AddToClassList("ds-node__button");
                
            TextField choiceTextField = DSElementUtility.CreateTextField(choice);
                
            choiceTextField.style.flexDirection = FlexDirection.Column;
            choiceTextField.AddClasses(
                "ds-node__text-field",
                "ds-node__choice-text-field",
                "ds-node__text-field__hidden"
            );
                
            choicePort.Add(choiceTextField);
            choicePort.Add(deleteChoiceBtn);
            return choicePort;
        }
    }
    
}
