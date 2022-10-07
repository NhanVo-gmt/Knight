using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D rb {get; private set;} 
    public Vector2 direction {get; private set;}

    float gravityScale;
    
    protected override void Awake() 
    {
        base.Awake();
        
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Start()
    {
        InitializeDirection();

        gravityScale = rb.gravityScale;
    }

    #region Velocity

    public void SetVelocity(Vector2 velocity) 
    {
        ChangeDirection(velocity.x);

        rb.velocity = velocity;
    }

    public void SetVelocityX(float xVelocity)
    {
        ChangeDirection(xVelocity);
        
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    public void SetVelocityY(float yVelocity) 
    {
        rb.velocity = new Vector2(rb.velocity.x, yVelocity);
    }

    public void SetVelocityZero() 
    {
        rb.velocity = Vector2.zero;
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    public void SetGravityZero()
    {
        rb.gravityScale = 0;
    }

    public void SetGravityNormal()
    {
        rb.gravityScale = gravityScale;
    }


    #endregion
    

    #region Direction

    private void InitializeDirection()
    {
        direction = Vector2.left;
    }

    public void ChangeDirection(float xInput)
    {
        if (xInput < 0)
        {
            if (direction == Vector2.right) Flip();
            
            direction = Vector2.left;
            
        }
        else if (xInput > 0)
        {
            if (direction == Vector2.left) Flip();
            
            direction = Vector2.right;
        }
    }

    void Flip() 
    {
        rb.transform.Rotate(0, 180, 0);
    }

    #endregion
}
