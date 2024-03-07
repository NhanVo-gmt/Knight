using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public enum LockedType
    {
        Key,
        Switch,
    }

    [SerializeField] private LockedType lockedType = LockedType.Switch;
    
    [SerializeField] private Switch doorSwitch;

    private float duration = 2f;
    private Vector2 startPos;
    private Vector2 endPos;

    private void Awake()
    {
        startPos = transform.position;
        endPos = startPos + new Vector2(0, -3);
    }

    private void OnEnable()
    {
        doorSwitch.OnTrigger += OpenDoor;
    }

    private void OnDisable()
    {
        doorSwitch.OnTrigger -= OpenDoor;
    }

    private void OpenDoor()
    {
        StartCoroutine(ToggleDoorCoroutine(startPos, endPos));
    }

    private void CloseDoor()
    {
        
    }

    IEnumerator ToggleDoorCoroutine(Vector2 startPos, Vector2 endPos)
    {
        float time = Time.time;
        while (time + duration >= Time.time)
        {
            transform.position = Vector2.Lerp(startPos, endPos, (Time.time - time) / duration);
            yield return null;
        }

        transform.position = endPos;
    }
}
