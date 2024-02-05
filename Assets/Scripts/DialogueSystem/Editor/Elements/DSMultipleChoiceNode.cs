using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Elements
{
    using Data.Save;
    using Window;
    using Utilities;
    using Enumerations;
    
    public class DSMultipleChoiceNode : DSNode
    {
        public override void Initialize(DSGraphView dsGraphView, Vector2 position)
        {
            base.Initialize(dsGraphView, position);

            DialogueType = DSDialogueType.MultipleChoice;
            DSChoiceSaveData choiceSaveData = new DSChoiceSaveData()
            {
                Text = "New Choice",
            };
            
            Choices.Add(choiceSaveData);
        }

        public override void Draw()
        {
            base.Draw();

            Button addChoiceBtn = DSElementUtility.CreateButton("Add Choice", () =>
            {
                DSChoiceSaveData choiceSaveData = new DSChoiceSaveData()
                {
                    Text = "New Choice",
                };

                Port choicePort = CreateChoicePort(choiceSaveData);
                outputContainer.Add(choicePort);
            });
            addChoiceBtn.AddToClassList("ds-node__button");
            
            mainContainer.Insert(1, addChoiceBtn);
            
            foreach (DSChoiceSaveData choice in Choices)
            {
                Port choicePort = CreateChoicePort(choice);
                
                outputContainer.Add(choicePort);
            }
            RefreshExpandedState();
        }

        private Port CreateChoicePort(object userData)
        {
            Port choicePort = this.CreatePort("", Orientation.Horizontal, Direction.Output, Port.Capacity.Single);
            choicePort.userData = userData;

            DSChoiceSaveData choiceData = (DSChoiceSaveData)userData;
                
            Button deleteChoiceBtn = DSElementUtility.CreateButton("X", () =>
            {
                if (Choices.Count == 1) return;

                if (choicePort.connected)
                {
                    graphView.DeleteElements(choicePort.connections);
                }

                Choices.Remove(choiceData);
                graphView.RemoveElement(choicePort);
            });
            deleteChoiceBtn.AddToClassList("ds-node__button");
                
            TextField choiceTextField = DSElementUtility.CreateTextField(choiceData.Text, null, callback =>
            {
                choiceData.Text = callback.newValue;
            });
                
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
