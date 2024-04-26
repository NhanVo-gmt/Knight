using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class MovingPlatform : MonoBehaviour
{
    [Header("Check points")]
    public List<Vector2> localCheckpoints;

    private List<Vector2> worldCheckPoints;

    [SerializeField] private float speed = 10f;
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private int nextIndex = 0;
    [SerializeField] private int targetIndex = 0;
    
    private Vector2 destination;
    private Vector2 direction;

    [Header("Switch")] 
    [SerializeField] private Switch moveSwitch;

    private Rigidbody2D rb;
    private Vector2 velocity;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        worldCheckPoints = localCheckpoints.Select(x => x + (Vector2)transform.position).ToList();
        if (moveSwitch) moveSwitch.OnTrigger += OnSwitchTrigger;
    }
    
    private void Start()
    {
        destination = transform.position;
        nextIndex = currentIndex;
    }

    private void OnSwitchTrigger()
    {
        if (targetIndex == 0) targetIndex = worldCheckPoints.Count - 1;
        else targetIndex = 0;
    }

    void SetNextTarget()
    {
        if (nextIndex > targetIndex)
        {
            nextIndex--;
        }
        else if (nextIndex < targetIndex)
        {
            nextIndex++;
        }
        else return;
        
        destination = worldCheckPoints[nextIndex];
        direction = (destination - (Vector2)transform.position).normalized;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if (currentIndex == targetIndex) return;
        if (Vector2.Distance(transform.position, destination) < 0.3f)
        {
            direction = Vector2.zero;
            currentIndex = nextIndex;
            SetNextTarget();
        }
    }

    private void FixedUpdate()
    {
        velocity = direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + velocity);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            PlayerParent.Instance.SetMovingPlatform(this);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            PlayerParent.Instance.UnsetMovingPlatform();
        }
    }

    #region Get Methods

    public Vector2 GetVelocity()
    {
        return velocity;
    }

    #endregion


    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < localCheckpoints.Count; i++)
        {
            CustomGizmos.DrawString(i.ToString(), localCheckpoints[i] + (Vector2)transform.position, Color.blue);
            Gizmos.DrawSphere(localCheckpoints[i] + (Vector2)transform.position, 0.1f);
        }
    }
}
