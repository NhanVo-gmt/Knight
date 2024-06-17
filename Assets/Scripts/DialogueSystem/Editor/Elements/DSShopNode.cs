using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DS.Elements
{
    using DS.Enumerations;
    using DS.Utilities;
    using DS.Window;
    using UnityEditor.UIElements;
    using UnityEngine.UIElements;

    public class DSShopNode : DSNode
    {
        public ShopItemData itemData { get; private set; }
        
        public override void Initialize(string nodeName, DSGraphView dsGraphView, Vector2 position)
        {
            base.Initialize(nodeName, dsGraphView, position);

            DialogueType = DSDialogueType.Shop;
        }

        public override void Draw()
        {
            base.Draw();
            
            RefreshExpandedState();
        }

        public override void CreateExtensionContainer()
        {
            // Extension container
            VisualElement customDataContainer = new VisualElement();
            customDataContainer.AddClasses("ds-node__custom-data-container");

            Foldout textFoldout = DSElementUtility.CreateFoldout("Dialogue Text");

            ObjectField objectField = DSElementUtility.CreateObjectField<ShopItemData>(itemData, null, callback =>
            {
                Text = callback.newValue;
            });

            objectField.AddClasses(
                "ds-node__textfield",
                "ds-node__quote-textfield"
            );
            
            textFoldout.Add(objectField);
            customDataContainer.Add(textFoldout);
            
            extensionContainer.Add(customDataContainer);
        }
    }
    
}
