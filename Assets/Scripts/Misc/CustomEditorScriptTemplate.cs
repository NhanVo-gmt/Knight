using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public partial class CustomEditorScriptTemplate
{
    private static string PATH_TO_OUTPUT_SCRIPT_FILE = "/Scripts/BehaviourTree/Node/ActionNode/";
    private static string FILE_NAME = "";
    private static string FILE_EXTENSION = "Editor.txt";
    
    private static List<string> PUBLIC_VARIABLES = new List<string>();
    private static List<string> VARIABLE_TYPES = new List<string>(){"bool", "int", "string", "float", "Vector2", "Vector3"};
    
    [MenuItem("Knight/Custom Editor/Generate Node Template")]
    public static void GenerateNodeCustomEditorScript()
    {
        if (Selection.activeObject != null)
        {
            string scriptTxt = Selection.activeObject.ToString();
            ReadClass(scriptTxt);
            
            StringBuilder result = new StringBuilder();
            AddClassHeader(result);
            AddClassBody(result);
            AddClassFooter(result);
            
            string scriptPath = Application.dataPath + PATH_TO_OUTPUT_SCRIPT_FILE + FILE_NAME + FILE_EXTENSION;
            File.WriteAllText(scriptPath, result.ToString());
        }
        else Debug.LogError("Please select a script");
    }

    private static void ReadClass(string scriptTxt)
    {
        string currentTxt = "";
        bool isPublic = false;
        bool isClass = false;
        bool isVariable = false;
        
        for (int i = 0; i < scriptTxt.Length; i++)
        {
            if (scriptTxt[i] == ' ' || scriptTxt[i] == '\n' || scriptTxt[i] == ';')
            {
                // Get class name
                if (isClass)
                {
                    isClass = false;
                    FILE_NAME = currentTxt;
                }
                // Get class variable
                else if (isVariable)
                {
                    isPublic = false;
                    isVariable = false;
                    PUBLIC_VARIABLES.Add(currentTxt);
                }

                if (currentTxt == "class")
                {
                    isClass = true;
                }
                else if (currentTxt == "public")
                {
                    isPublic = true;
                }
                else if (VARIABLE_TYPES.Contains(currentTxt) && isPublic)
                {
                    isVariable = true;
                }

                currentTxt = "";
            }
            else
            {
                currentTxt += scriptTxt[i];
            }
        }
    }

    private static void AddClassHeader(StringBuilder result)
    {
        result.Append(@"using UnityEditor;
using UnityEngine;
public class ");
        result.Append(FILE_NAME);
        result.Append(@"Editor : Editor
{");
    }

    private static void AddClassBody(StringBuilder result)
    {
        foreach (string variable in PUBLIC_VARIABLES)
        {
            result.Append(@"
    private SerializedProperty ");
            string variablePropertyName = variable + "Property;";
            Debug.Log(variablePropertyName);
            result.Append(variablePropertyName);
        }
    }

    private static void AddClassFooter(StringBuilder result)
    {
        result.Append(@"
}");
    }
}
