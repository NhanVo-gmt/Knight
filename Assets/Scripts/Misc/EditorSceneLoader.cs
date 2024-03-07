#if UNITY_EDITOR

using System;
using System.Collections.Generic;
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
    private const string PATH_TO_OUTPUT_ENUM_SCRIPT_FILE = "/Scripts/Misc/Game Misc/SceneLoaderEnum.cs";
    private const string ASSETS_SCENE_PATH = "Assets/Scenes/";

    [MenuItem("Knight/Scene/Editor/Generate Scene Load Menu Code")]
    public static void GenerateSceneLoadMenuCode()
    {
        StringBuilder result = new StringBuilder();
        string basePath = Application.dataPath + PATH_TO_SCENE_FOLDER;
        AddClassHeader();
        AddCodeForDirectory(new DirectoryInfo(basePath));
        AddClassFooter();

        string scriptPath = Application.dataPath + PATH_TO_OUTPUT_SCRIPT_FILE;
        File.WriteAllText(scriptPath, result.ToString());
        
        // GenerateCodeForEnumScriptFile(); //todo remove due to error, update manually

        void AddCodeForDirectory(DirectoryInfo directoryInfo)
        {
            FileInfo[] fileInfoList = directoryInfo.GetFiles();
            for (int i = 0; i < fileInfoList.Length; i++)
            {
                FileInfo fileInfo = fileInfoList[i];
                if (fileInfo.Extension == ".unity")
                {
                    AddCodeForFile(fileInfo);
                }
            }

            DirectoryInfo[] subDirectories = directoryInfo.GetDirectories();
            for (int i = 0; i < subDirectories.Length; i++)
            {
                AddCodeForDirectory(subDirectories[i]);
            }
            
            void AddCodeForFile(FileInfo fileInfo)
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
        
        void AddClassHeader()
        {
            result.Append(@"using UnityEditor;
public partial class EditorSceneLoader 
    {
");
            result.Append(@"#if UNITY_EDITOR
");
        }
        
        
        void AddClassFooter()
        {
            result.Append(@"#endif
    }
");
        }
    }

    private static void OpenScene(string scenePath)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        }
    }
    
    private static void GenerateCodeForEnumScriptFile()
    {
        StringBuilder result = new StringBuilder();
        HashSet<string> regions = new HashSet<string>();
        regions.Add("None");
        string basePath = Application.dataPath + PATH_TO_SCENE_FOLDER;
        
        AddClassHeader();
        AddCodeForSceneEnum(new DirectoryInfo(basePath));
        AddCodeForRegionEnum();
        AddClassFooter();

        string scriptPath = Application.dataPath + PATH_TO_OUTPUT_ENUM_SCRIPT_FILE;
        File.WriteAllText(scriptPath, result.ToString());


        void AddCodeForSceneEnum(DirectoryInfo directoryInfo)
        {
            FileInfo[] fileInfoList = directoryInfo.GetFiles();
            for (int i = 0; i < fileInfoList.Length; i++)
            {
                FileInfo fileInfo = fileInfoList[i];
                if (fileInfo.Extension == ".unity")
                {
                    AddCodeForFile(fileInfo);
                }
            }

            DirectoryInfo[] subDirectories = directoryInfo.GetDirectories();
            for (int i = 0; i < subDirectories.Length; i++)
            {
                AddCodeForSceneEnum(subDirectories[i]);
            }

            void AddCodeForFile(FileInfo fileInfo)
            {
                string subPath = fileInfo.FullName.Replace(basePath, "");
                string region = Path.GetFileName(Path.GetDirectoryName(subPath));
                Debug.Log($"Region Name: {region}");
                regions.Add(region);
                
                Debug.Log($"Scene Name: {fileInfo.Name}");

                result.Append("        ").Append(fileInfo.Name.Replace(".unity", "")).Append(",")
                    .Append(Environment.NewLine);
            }
        }

        void AddCodeForRegionEnum()
        {
            result.Append(@"    }
");
            result.Append(@"    public enum Region {
");
            foreach (string region in regions)
            {
                if (region == "Scenes") continue;
                
                result.Append("        ").Append(region).Append(",")
                    .Append(Environment.NewLine);
            }
        }
                
        void AddClassHeader()
        {
            result.Append(@"using System;
public partial class SceneLoader : SingletonObject<SceneLoader>
{
");
            result.Append(@"    public enum Scene {
");
        }

        void AddClassFooter()
        {
            result.Append(@"    }
");
            result.Append(@"}
");
        }
    }
    
}

#endif
