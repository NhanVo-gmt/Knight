using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DS.Elements
{
    using Data.Save;
    using Utilities;
    using Enumerations;
    using Window;
    
    public class DSSingleChoiceNode : DSNode
    {
        public override void Initialize(string nodeName, DSGraphView dsGraphView, Vector2 position)
        {
            base.Initialize(nodeName, dsGraphView, position);
            
            DialogueType = DSDialogueType.SingleChoice;
            DSChoiceSaveData choiceSaveData = new DSChoiceSaveData()
            {
                Text = "Next Dialogue"
            };
            Choices.Add(choiceSaveData);
        }

        public override void Draw()
        {
            base.Draw();

            foreach (DSChoiceSaveData choice in Choices)
            {
                Port choicePort = this.CreatePort(choice.Text, Orientation.Horizontal, Direction.Output, Port.Capacity.Single);
                choicePort.userData = choice;
                
                outputContainer.Add(choicePort);
            }
            RefreshExpandedState();
        }
    }
    
}
