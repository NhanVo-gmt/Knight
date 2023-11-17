#if UNITY_EDITOR

using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public partial class EditorSceneLoader
{
    private const string PATH_TO_SCENE_FOLDER = "/Scenes/";
    private const string PATH_TO_OUTPUT_SCRIPT_FILE = "/Scripts/Misc/EditorSceneLoaderDropdown.cs";
    private const string ASSETS_SCENE_PATH = "Assets/Scenes/";

    [MenuItem("Knight/Scene/Generate Scene Load Menu Code")]
    public static void GenerateSceneLoadMenuCode()
    {
        StringBuilder result = new StringBuilder();
        string basePath = Application.dataPath + PATH_TO_SCENE_FOLDER;
        AddClassHeader(result);
        AddCodeForDirectory(new DirectoryInfo(basePath), result);
        AddClassFooter(result);

        string scriptPath = Application.dataPath + PATH_TO_OUTPUT_SCRIPT_FILE;
        File.WriteAllText(scriptPath, result.ToString());

        void AddCodeForDirectory(DirectoryInfo directoryInfo, StringBuilder result)
        {
            FileInfo[] fileInfoList = directoryInfo.GetFiles();
            for (int i = 0; i < fileInfoList.Length; i++)
            {
                FileInfo fileInfo = fileInfoList[i];
                if (fileInfo.Extension == ".unity")
                {
                    AddCodeForFile(fileInfo, result);
                }
            }

            DirectoryInfo[] subDirectories = directoryInfo.GetDirectories();
            for (int i = 0; i < subDirectories.Length; i++)
            {
                AddCodeForDirectory(subDirectories[i], result);
            }
        }

        void AddCodeForFile(FileInfo fileInfo, StringBuilder result)
        {
            Debug.Log($"File info fullname: {fileInfo.FullName} {fileInfo.Name}");
            string subPath = fileInfo.FullName.Replace("\\", "/").Replace(basePath, "");
            
            Debug.Log($"Sub path: {subPath}");
            string assetPath = ASSETS_SCENE_PATH + subPath;

            string functionName = fileInfo.Name.Replace(".unity", "").Replace(" ", "").Replace("-", "");

            result.Append("        [MenuItem(\"Scenes/").Append(subPath.Replace(".unity", "")).Append("\")]")
                .Append(Environment.NewLine);
            result.Append("        public static void Load").Append(functionName).Append("() { OpenScene(\"")
                .Append(assetPath).Append("\"); }").Append(Environment.NewLine);
        }
    }

    private static void AddClassHeader(StringBuilder result)
    {
        result.Append(@"using UnityEditor;
public partial class EditorSceneLoader 
    {
");
        result.Append(@"#if UNITY_EDITOR
");
    }
    

    private static void AddClassFooter(StringBuilder result)
    {
        result.Append(@"#endif
    }
");
    }

    private static void OpenScene(string scenePath)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        }
    }
}

#endif
