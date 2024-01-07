using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D rb {get; private set;} 
    public Vector2 faceDirection {get; private set;}

    private PlayerData data;

    bool canSetVelocity = true;
    float gravityScale;
    
    public bool isDashing { get; private set; }
    public bool isDashAttacking { get; private set; }
    
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

    public void InitializeData(PlayerData data)
    {
        this.data = data;
    }

    #region Velocity

    public void SetVelocity(Vector2 velocity, bool needToChangeFaceDirection = true) 
    {
        if (needToChangeFaceDirection)
        {
            ChangeDirection(velocity.x);
        }

        if (!canSetVelocity) return;

        rb.velocity = velocity;
    }

    public void SetVelocityX(float xVelocity, bool needToChangeFaceDirection = true)
    {
        if (needToChangeFaceDirection)
        {
            ChangeDirection(xVelocity);
        }

        if (!canSetVelocity) return;
        
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    public void SetVelocityY(float yVelocity) 
    {
        if (!canSetVelocity) return;
        
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

    public void AddForce(Vector2 direction, float amount)
    {
        rb.AddForce(direction * amount, ForceMode2D.Impulse);
    }

    #endregion

    #region Gravity

    public void SetGravityZero()
    {
        rb.gravityScale = 0;
    }

    public void SetGravityNormal()
    {
        rb.gravityScale = gravityScale;
    }

    public void SetGravityScale(float scale)
    {
        rb.gravityScale = scale;
    }


    #endregion

    #region Position

    public Vector2 GetPosition()
    {
        return rb.position;
    }

    #endregion

    #region Direction

    private void InitializeDirection()
    {
        faceDirection = Vector2.left;
    }

    public void ChangeDirection(float xInput)
    {
        if (xInput < 0)
        {
            if (faceDirection == Vector2.right) Flip();
            
            faceDirection = Vector2.left;
            
        }
        else if (xInput > 0)
        {
            if (faceDirection == Vector2.left) Flip();
            
            faceDirection = Vector2.right;
        }
    }

    void Flip() 
    {
        rb.transform.Rotate(0, 180, 0);
    }

    #endregion

    #region Movement Methods

    public void Move(float xInput, float lerpAmount)
    {
        ChangeDirection(xInput);
        
        float targetSpeed = xInput * data.moveData.moveMaxSpeed;
        targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, lerpAmount);
        
        #region Calculate Accelerate

        float accelRate;
        
        // Gets an acceleration value based on if we are accelerating (includes turning) or deccelerating (stops)
        accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? data.moveData.moveAccelAmount : data.moveData.moveDeccelAmount;

        #endregion
        
        // #region Conserve Momentum
        //
        // // Wont slow player when they are moving in the same direction 
        // if (data.moveData.doConserveMomentum && Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) &&
        //     Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f)
        // {
        //     // Prevent any deceleration from happening
        //     accelRate = 0f;
        // }
        //
        // #endregion

        float speedDiff = targetSpeed - rb.velocity.x;
        
        float movement = speedDiff * accelRate;
        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    public void Dash()
    {
        isDashing = true;
        StartCoroutine(StartDash());
    }

    IEnumerator StartDash()
    {
        float startTime = Time.time;
        isDashAttacking = true;
        SetGravityScale(0);

        while (Time.time - startTime <= data.dashData.dashAttackTime)
        {
            rb.velocity = faceDirection.normalized * data.dashData.dashSpeed;
            yield return null;
        }

        isDashAttacking = false;

        yield return EndDashCoroutine();
    }

    public void CancelDash()
    {
        StopAllCoroutines();
        isDashing = false;
        isDashAttacking = false;
    }

    IEnumerator EndDashCoroutine()
    {
        SetGravityScale(data.jumpData.gravityScale);
        rb.velocity = data.dashData.dashEndSpeed * faceDirection.normalized;

        float startTime = Time.time;
        while (Time.time - startTime <= data.dashData.dashEndTime)
        {
            yield return null;
        }
        
        isDashing = false;
    }


    public void MoveToPos(Vector2 endPos, float lerpTime)
    {
        SetVelocityZero();
        SetGravityScale(0f);
        StartCoroutine(MoveToPosCoroutine(endPos, lerpTime));
    }
    
    IEnumerator MoveToPosCoroutine(Vector2 endPos, float lerpTime)
    {
        Vector2 startPos = GetPosition();
        float startTime = 0f;
        
        while (startTime <= lerpTime)
        {
            Vector2 newPos = Vector2.Lerp(startPos, endPos, startTime / lerpTime);
            startTime += Time.deltaTime;
            rb.position = newPos;
            yield return null;
        }

        rb.position = endPos;
    }

    #endregion
}
