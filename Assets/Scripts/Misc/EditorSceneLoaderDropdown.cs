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
        [MenuItem("Scenes/Festival/FestivalScene")]
        public static void LoadFestivalScene() { OpenScene("Assets/Scenes/Festival/FestivalScene.unity"); }
        [MenuItem("Scenes/ForbiddenLand/ForbiddenLandScene")]
        public static void LoadForbiddenLandScene() { OpenScene("Assets/Scenes/ForbiddenLand/ForbiddenLandScene.unity"); }
        [MenuItem("Scenes/ForbiddenLand/ForbiddenLandScene1")]
        public static void LoadForbiddenLandScene1() { OpenScene("Assets/Scenes/ForbiddenLand/ForbiddenLandScene1.unity"); }
        [MenuItem("Scenes/ForbiddenLand/ForbiddenLandScene2")]
        public static void LoadForbiddenLandScene2() { OpenScene("Assets/Scenes/ForbiddenLand/ForbiddenLandScene2.unity"); }
        [MenuItem("Scenes/Forest/ForestScene")]
        public static void LoadForestScene() { OpenScene("Assets/Scenes/Forest/ForestScene.unity"); }
        [MenuItem("Scenes/Forest/ForestScene1")]
        public static void LoadForestScene1() { OpenScene("Assets/Scenes/Forest/ForestScene1.unity"); }
        [MenuItem("Scenes/Forest/ForestScene2")]
        public static void LoadForestScene2() { OpenScene("Assets/Scenes/Forest/ForestScene2.unity"); }
        [MenuItem("Scenes/Forest/ForestScene3")]
        public static void LoadForestScene3() { OpenScene("Assets/Scenes/Forest/ForestScene3.unity"); }
#endif
    }
