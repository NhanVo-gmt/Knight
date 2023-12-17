using System;
public partial class SceneLoader : SingletonObject<SceneLoader>
{
    public enum Scene {
        CaveScene,
        MenuScene,
        TestScene,
        FarmScene,
        ForestScene,
        ForestScene1,
        ForestScene2,
        ForestScene3,
    }
    public enum Region {
        None,
        Farm,
        Forest,
    }
}
