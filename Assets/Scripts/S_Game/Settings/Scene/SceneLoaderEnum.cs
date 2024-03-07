using System;
public static class SceneLoaderEnum
{
    public enum Scene {
        CaveScene = 400,
        MenuScene = 00,
        TestScene = 01,
        FarmScene = 100,
        ForbiddenLand = 300,
        ForbiddenLand1 = 301,
        ForestScene = 200,
        ForestScene1 = 201,
        ForestScene2 = 202,
        ForestScene3 = 203,
    }
    public enum Region {
        None,
        Farm,
        ForbiddenLand,
        Forest,
    }
}
