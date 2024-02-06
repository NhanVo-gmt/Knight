using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace DS.Inspectors
{
    using Utilities;
    using ScriptableObjects;
    
    [CustomEditor(typeof(DSDialogue))]
    public class DSInspector : Editor
    {
        // Dialogue Scriptable Objects
        private SerializedProperty dialogueContainerProperty;
        private SerializedProperty dialogueGroupProperty;
        private SerializedProperty dialogueProperty;
        
        // Filters
        private SerializedProperty groupedDialoguesProperty;
        private SerializedProperty startingDialoguesOnlyProperty;

        // Index
        private SerializedProperty selectedDialogueGroupIndexProperty;
        private SerializedProperty selectedDialogueIndexProperty;

        private void OnEnable()
        {
            dialogueContainerProperty = serializedObject.FindProperty("dialogueContainer");
            dialogueGroupProperty = serializedObject.FindProperty("dialogueGroup");
            dialogueProperty = serializedObject.FindProperty("dialogue");
            
            groupedDialoguesProperty = serializedObject.FindProperty("groupedDialogues");
            startingDialoguesOnlyProperty = serializedObject.FindProperty("startingDialoguesOnly");
            
            selectedDialogueGroupIndexProperty = serializedObject.FindProperty("selectedDialogueGroupIndex");
            selectedDialogueIndexProperty = serializedObject.FindProperty("selectedDialogueIndex");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            DrawContainerArea();

            DSDialogueContainerSO dialogueContainer =
                (DSDialogueContainerSO)dialogueContainerProperty.objectReferenceValue; 
            if (dialogueContainer == null)
            {
                StopDrawing("Select a Dialogue Container to see the rest of the Inspector");
                return;
            }
            
            DrawFilterArea();

            if (groupedDialoguesProperty.boolValue)
            {
                List<string> dialogueGroupNames = dialogueContainer.GetDialogueGroupNames();
                if (dialogueGroupNames.Count == 0)
                {
                    StopDrawing("There are no Dialogue Groups in this container");
                    return;
                }
                DrawDialogueGroupArea(dialogueContainer, dialogueGroupNames);
            }
            DrawDialogueArea();

            serializedObject.ApplyModifiedProperties();
        }

        #region Draw Methods
        private void DrawContainerArea()
        {
            DSInspectorUtility.DrawHeader("Dialogue Container");
            dialogueContainerProperty.DrawPropertyField();
            
            DSInspectorUtility.DrawSpace();
        }

        private void DrawFilterArea()
        {
            DSInspectorUtility.DrawHeader("Filter");
            
            groupedDialoguesProperty.DrawPropertyField();
            startingDialoguesOnlyProperty.DrawPropertyField();
            
            DSInspectorUtility.DrawSpace();
        }
        
        private void DrawDialogueGroupArea(DSDialogueContainerSO dialogueContainer, List<string> dialogueGroupNames)
        {
            DSInspectorUtility.DrawHeader("Dialogue Groups");

            int oldSelectedDialogueGroupIndex = selectedDialogueIndexProperty.intValue;

            DSDialogueGroupSO oldDialogueGroup = (DSDialogueGroupSO)dialogueGroupProperty.objectReferenceValue;
            bool isOldDialogueGroupNull = oldDialogueGroup == null;
            string oldDialogueGroupName = isOldDialogueGroupNull ? "" : oldDialogueGroup.GroupName;
            UpdateIndexOnNamesListUpdate(dialogueGroupNames, selectedDialogueGroupIndexProperty, oldSelectedDialogueGroupIndex, oldDialogueGroupName, isOldDialogueGroupNull);
            
            selectedDialogueGroupIndexProperty.intValue = DSInspectorUtility.DrawPopup("Dialogue Groups", selectedDialogueGroupIndexProperty, dialogueGroupNames.ToArray());

            string selectedDialogueGroupName = dialogueGroupNames[selectedDialogueGroupIndexProperty.intValue];
            DSDialogueGroupSO selectedDialogueGroup = DSIOUtility.LoadAsset<DSDialogueGroupSO>(
                $"Assets/DialogueSystem/Dialogues/{dialogueContainer.FileName}/Groups/{selectedDialogueGroupName}",
                selectedDialogueGroupName);
            dialogueGroupProperty.objectReferenceValue = selectedDialogueGroup;
            
            dialogueGroupProperty.DrawPropertyField();
            
            DSInspectorUtility.DrawSpace();
        }

        private void DrawDialogueArea()
        {
            DSInspectorUtility.DrawHeader("Dialogues");

            selectedDialogueIndexProperty.intValue = DSInspectorUtility.DrawPopup("Dialogues", selectedDialogueIndexProperty, new string[] { });
            
            dialogueProperty.DrawPropertyField();
        }
        
        private void StopDrawing(string message)
        {
            DSInspectorUtility.DrawHelpBox(message);
            serializedObject.ApplyModifiedProperties();
        }
        
        #endregion

        #region Index Methods

        private void UpdateIndexOnNamesListUpdate(List<string> optionNames, SerializedProperty indexProperty, int oldSelectedDialogueGroupIndex, string oldPropertyName, bool isOldPropertyNull)
        {
            if (isOldPropertyNull)
            {
                indexProperty.intValue = 0;
                return;
            }

            bool oldIndexIsOutOfBoundsOfNameListCount = oldSelectedDialogueGroupIndex > optionNames.Count - 1;
            bool oldNameIsDifferentThanSelectedName = oldIndexIsOutOfBoundsOfNameListCount ||
                                                      oldPropertyName != optionNames[oldSelectedDialogueGroupIndex];
            
            if (oldNameIsDifferentThanSelectedName)
            {
                indexProperty.intValue =
                    optionNames.IndexOf(oldPropertyName);
            }
            else
            {
                selectedDialogueIndexProperty.intValue = 0;
            }
        }

        #endregion
    }
}
