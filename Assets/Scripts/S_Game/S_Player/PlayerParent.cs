using System;
using UnityEngine;

public class PlayerParent : SingletonObject<PlayerParent>
{
    // moving platform, elevators
    private MovingPlatform platform;
    private MovingPlatform[] platforms;

    protected override void Awake()
    {
        base.Awake();
    }


    private void OnEnable()
    {
        SceneLoader.Instance.OnSceneLoadingCompleted += SceneLoader_OnSceneLoadingCompleted;
        FindAllMovingPlatformsInScene();
    }

    private void OnDisable()
    {
        if (QuitUtils.isQuitting) return;
        
        SceneLoader.Instance.OnSceneLoadingCompleted -= SceneLoader_OnSceneLoadingCompleted;
    }

    private void SceneLoader_OnSceneLoadingCompleted(object sender, EventArgs e)
    {
        FindAllMovingPlatformsInScene();
    }

    void FindAllMovingPlatformsInScene()
    {
        platforms = FindObjectsOfType<MovingPlatform>();
        foreach (MovingPlatform movingPlatform in platforms)
        {
            movingPlatform.OnPlayerOnPlatform += SetMovingPlatform;
        }
    }


    Vector2 GetPlatformVelocity()
    {
        if (!platform) return Vector2.zero;

        return platform.GetVelocity();
    }

    public void SetMovingPlatform(MovingPlatform platform)
    {
        this.platform = platform;
    }

    private void FixedUpdate()
    {
        if (Player.Instance == null) return; 
            
        Player.Instance.SetPlatformVelocity(GetPlatformVelocity());
    }
}
