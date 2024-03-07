using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Utilities
{
    public static class DSStyleUtility
    {
        public static VisualElement AddClasses(this VisualElement element, params string[] classNames)
        {
            foreach (string className in classNames)
            {
                element.AddToClassList(className);
            }

            return element;
        }
        
        public static VisualElement AddStyleSheets(this VisualElement element, params string[] styleSheetPaths)
        {
            foreach (string path in styleSheetPaths)
            {
                StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
                element.styleSheets.Add(styleSheet);
            }

            return element;
        }
    }
}
