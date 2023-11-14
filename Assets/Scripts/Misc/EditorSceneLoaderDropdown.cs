using UnityEditor;
public partial class EditorSceneLoader 
    {
#if UNITY_EDITOR
        [MenuItem("Scenes/CaveScene")]
        public static void LoadCaveScene() { OpenScene("Assets/Scenes/CaveScene.unity"); }
        [MenuItem("Scenes/FarmScene")]
        public static void LoadFarmScene() { OpenScene("Assets/Scenes/FarmScene.unity"); }
        [MenuItem("Scenes/ForestScene")]
        public static void LoadForestScene() { OpenScene("Assets/Scenes/ForestScene.unity"); }
        [MenuItem("Scenes/MenuScene")]
        public static void LoadMenuScene() { OpenScene("Assets/Scenes/MenuScene.unity"); }
        [MenuItem("Scenes/TestScene")]
        public static void LoadTestScene() { OpenScene("Assets/Scenes/TestScene.unity"); }
#endif
    }
