using UnityEditor;
public partial class EditorSceneLoader 
    {
#if UNITY_EDITOR
        [MenuItem("Scenes/CaveScene")]
        public static void LoadCaveScene() { OpenScene("Assets/Scenes/CaveScene.unity"); }
        [MenuItem("Scenes/MenuScene")]
        public static void LoadMenuScene() { OpenScene("Assets/Scenes/MenuScene.unity"); }
        [MenuItem("Scenes/TestScene")]
        public static void LoadTestScene() { OpenScene("Assets/Scenes/TestScene.unity"); }
        [MenuItem("Scenes/Farm/FarmScene")]
        public static void LoadFarmScene() { OpenScene("Assets/Scenes/Farm/FarmScene.unity"); }
        [MenuItem("Scenes/Forest/ForestScene")]
        public static void LoadForestScene() { OpenScene("Assets/Scenes/Forest/ForestScene.unity"); }
        [MenuItem("Scenes/Forest/ForestScene1")]
        public static void LoadForestScene1() { OpenScene("Assets/Scenes/Forest/ForestScene1.unity"); }
        [MenuItem("Scenes/Forest/ForestScene2")]
        public static void LoadForestScene2() { OpenScene("Assets/Scenes/Forest/ForestScene2.unity"); }
#endif
    }
