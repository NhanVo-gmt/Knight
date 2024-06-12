using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueHolder))]
public class NPC : MonoBehaviour
{
    private DialogueHolder dialogueHolder;
    private NPCCore        core;

    private void Awake()
    {
        core = GetComponentInChildren<NPCCore>();
    }
    
}
