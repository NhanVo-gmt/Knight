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
            UpdateIndexOnDialogueGroupUpdate(dialogueGroupNames, oldDialogueGroup, oldSelectedDialogueGroupIndex);
            
            selectedDialogueGroupIndexProperty.intValue = DSInspectorUtility.DrawPopup("Dialogue Groups", selectedDialogueGroupIndexProperty, dialogueGroupNames.ToArray());

            string selectedDialogueGroupName = dialogueGroupNames[selectedDialogueIndexProperty.intValue];
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

        private void UpdateIndexOnDialogueGroupUpdate(List<string> dialogueGroupNames, DSDialogueGroupSO oldDialogueGroup,
            int oldSelectedDialogueGroupIndex)
        {
            if (oldDialogueGroup == null)
            {
                selectedDialogueGroupIndexProperty.intValue = 0;
            }
            else
            {
                if (oldSelectedDialogueGroupIndex > dialogueGroupNames.Count - 1 || oldDialogueGroup.GroupName !=
                    dialogueGroupNames[oldSelectedDialogueGroupIndex])
                {
                    selectedDialogueGroupIndexProperty.intValue =
                        dialogueGroupNames.IndexOf(oldDialogueGroup.GroupName);
                }
                else
                {
                    selectedDialogueIndexProperty.intValue = 0;
                }
            }
        }

        #endregion
    }
}
