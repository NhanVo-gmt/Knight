using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeComponent
{
    public GameObject gameObject {get; private set;}
    public Transform transform {get; private set;}
    public Animator anim { get; private set; }

    public Core core {get; private set;}
    public GameObject player {get; private set;}

    public static BehaviourTreeComponent CreateTreeComponentFromGameObject(GameObject gameObject, GameObject player)
    {
        BehaviourTreeComponent component = new BehaviourTreeComponent();
        component.gameObject = gameObject;
        component.transform = gameObject.transform;
        component.core = gameObject.GetComponentInChildren<Core>();
        component.player = player;

        return component;
    }
}
