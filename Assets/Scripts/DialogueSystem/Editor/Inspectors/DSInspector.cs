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
            using (new EditorGUI.DisabledScope(true))
                EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((DSDialogue)target), typeof(DSDialogue), false);        
            
            DrawContainerArea();

            DSDialogueContainerSO dialogueContainer =
                (DSDialogueContainerSO)dialogueContainerProperty.objectReferenceValue; 
            if (dialogueContainer == null)
            {
                StopDrawing("Select a Dialogue Container to see the rest of the Inspector");
                return;
            }
            
            DrawFilterArea();

            bool currentStartingDialogueOnlyFilter = startingDialoguesOnlyProperty.boolValue;
            
            List<string> dialogueNames;
            string dialogueFolderPath = $"Assets/DialogueSystem/Dialogues/{dialogueContainer.FileName}";
            string dialogueInfoMessage = "";

            if (groupedDialoguesProperty.boolValue)
            {
                List<string> dialogueGroupNames = dialogueContainer.GetDialogueGroupNames();
                if (dialogueGroupNames.Count == 0)
                {
                    StopDrawing("There are no Dialogue Groups in this container");
                    return;
                }
                DrawDialogueGroupArea(dialogueContainer, dialogueGroupNames);

                DSDialogueGroupSO dialogueGroup = (DSDialogueGroupSO)dialogueGroupProperty.objectReferenceValue;
                dialogueNames = dialogueContainer.GetGroupedDialogueNames(dialogueGroup, currentStartingDialogueOnlyFilter);
                dialogueFolderPath += $"/Groups/{dialogueGroup.GroupName}/Dialogues";
                dialogueInfoMessage = "There are no" + (currentStartingDialogueOnlyFilter ? " Starting " : " ") + "Dialogues in this Dialogue Group.";
            }
            else
            {
                dialogueNames = dialogueContainer.GetUngroupedDialogueNames(currentStartingDialogueOnlyFilter);
                
                dialogueFolderPath += "/Global/Dialogues";
                dialogueInfoMessage = "There are no" + (currentStartingDialogueOnlyFilter ? " Starting " : " ") + "Ungrouped Dialogues in this Dialogue Container";
            }

            if (dialogueNames.Count == 0)
            {
                
                StopDrawing(dialogueInfoMessage);
                return;
            }
            
            
            DrawDialogueArea(dialogueNames, dialogueFolderPath);

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
            
            DSInspectorUtility.DrawDisableField(() => dialogueGroupProperty.DrawPropertyField());
            
            DSInspectorUtility.DrawSpace();
        }

        private void DrawDialogueArea(List<string> dialogueNames, string dialogueFolderPath)
        {
            DSInspectorUtility.DrawHeader("Dialogues");

            int oldSelectedDialogueIndex = selectedDialogueIndexProperty.intValue;
            DSDialogueSO oldDialogue = (DSDialogueSO)dialogueProperty.objectReferenceValue;
            
            bool isOldDialogueNull = oldDialogue == null;
            string oldDialogueName = isOldDialogueNull ? "" : oldDialogue.DialogueName;
            
            UpdateIndexOnNamesListUpdate(dialogueNames, selectedDialogueIndexProperty, oldSelectedDialogueIndex, oldDialogueName, isOldDialogueNull);

            selectedDialogueIndexProperty.intValue = DSInspectorUtility.DrawPopup("Dialogues", selectedDialogueIndexProperty, dialogueNames.ToArray());

            string selectedDialogueName = dialogueNames[selectedDialogueIndexProperty.intValue];
            DSDialogueSO selectedDialogue =
                DSIOUtility.LoadAsset<DSDialogueSO>(dialogueFolderPath, selectedDialogueName);
            dialogueProperty.objectReferenceValue = selectedDialogue;
            
            DSInspectorUtility.DrawDisableField(() => dialogueProperty.DrawPropertyField());
        }
        
        private void StopDrawing(string message)
        {
            DSInspectorUtility.DrawHelpBox(message);
            serializedObject.ApplyModifiedProperties();
        }
        
        #endregion

        #region Index Methods

        private void UpdateIndexOnNamesListUpdate(List<string> optionNames, SerializedProperty indexProperty, int oldSelectedPropertyIndex, string oldPropertyName, bool isOldPropertyNull)
        {
            if (isOldPropertyNull)
            {
                indexProperty.intValue = 0;

                return;
            }

            bool oldIndexIsOutOfBoundsOfNamesListCount = oldSelectedPropertyIndex > optionNames.Count - 1;
            bool oldNameIsDifferentThanSelectedName = oldIndexIsOutOfBoundsOfNamesListCount || oldPropertyName != optionNames[oldSelectedPropertyIndex];

            if (oldNameIsDifferentThanSelectedName)
            {
                if (optionNames.Contains(oldPropertyName))
                {
                    indexProperty.intValue = optionNames.IndexOf(oldPropertyName);

                    return;
                }

                indexProperty.intValue = 0;
            }
        }
        
        #endregion
    }
}
