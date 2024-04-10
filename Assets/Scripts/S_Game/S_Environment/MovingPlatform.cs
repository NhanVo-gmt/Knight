using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class MovingPlatform : MonoBehaviour
{
    public List<Vector2> _checkpoints;

    private List<Vector2> checkPoints;
    private List<Vector2> reverseCheckPoint;

    private float speed = 10f;
    
    private int currentIndex = 0;
    private int targetIndex = 0;
    private Vector3 target;
    private Vector3 direction;
    
    private void Awake()
    {
        checkPoints = _checkpoints;
        reverseCheckPoint = _checkpoints.Reverse<Vector2>();
    }

    public void SetTarget(int targetIndex)
    {
        this.targetIndex = targetIndex;
    }

    public void SetNextTarget()
    {
        if (currentIndex > targetIndex)
        {
            currentIndex--;
        }
        else if (currentIndex < targetIndex)
        {
            currentIndex++;
        }

        target = transform.position + (Vector3)_checkpoints[currentIndex];
        direction = (target - transform.position).normalized;
    }

    public void Move()
    {
        if (currentIndex == targetIndex) return;
        
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            SetNextTarget();
        }

        transform.position += direction * speed * Time.deltaTime;
    }

    private void Update()
    {
        Move();
    }


    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < _checkpoints.Count; i++)
        {
            CustomGizmos.DrawString(i.ToString(), _checkpoints[i] + (Vector2)transform.position, Color.blue);
            Gizmos.DrawSphere(_checkpoints[i] + (Vector2)transform.position, 0.1f);
        }
    }
}
