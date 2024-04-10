using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : SingletonObject<PlayerParent>
{
    // moving platform, elevators
    private Transform parentTransform;

    private Transform playerStartParent; // null in build, Test in dev

    private void Start()
    {
        playerStartParent = Player.Instance.transform.parent;
    }

    public void SetTransform(Transform newTransform)
    {
        parentTransform = newTransform;
        Player.Instance.transform.parent = transform;
    }

    public void RemoveTransform()
    {
        parentTransform = null;
        Player.Instance.transform.parent = playerStartParent;
    }

    private void Update()
    {
        if (parentTransform == null) return;
        
        Move();
    }

    void Move()
    {
        transform.position = parentTransform.position;
    }
}
