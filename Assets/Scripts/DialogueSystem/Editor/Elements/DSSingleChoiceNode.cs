using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DS.Elements
{
    using Utilities;
    using Enumerations;
    using Window;
    
    public class DSSingleChoiceNode : DSNode
    {
        public override void Initialize(DSGraphView dsGraphView, Vector2 position)
        {
            base.Initialize(dsGraphView, position);
            
            DialogueType = DSDialogueType.SingleChoice;
            Choices.Add("Next Dialogue");
        }

        public override void Draw()
        {
            base.Draw();

            foreach (string choice in Choices)
            {
                Port choicePort = this.CreatePort(choice, Orientation.Horizontal, Direction.Output, Port.Capacity.Single);
                outputContainer.Add(choicePort);
            }
            RefreshExpandedState();
        }
    }
    
}
