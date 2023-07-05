using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeComponent
{
    public GameObject gameObject {get; private set;}
    public Transform transform {get; private set;}
    public Rigidbody2D rb {get; private set;}
    public Collider2D col {get; private set;}

    public GameObject player {get; private set;}

    public static BehaviourTreeComponent CreateTreeComponentFromGameObject(GameObject gameObject)
    {
        BehaviourTreeComponent component = new BehaviourTreeComponent();
        component.gameObject = gameObject;
        component.transform = gameObject.transform;
        component.rb = gameObject.GetComponent<Rigidbody2D>();
        component.col = gameObject.GetComponent<Collider2D>();

        return component;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }
}
