using System;
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
    private Vector2 platformVelocity = Vector2.zero;
    
    public bool isAddingForce { get; private set; }
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
        isAddingForce = true;
        StartCoroutine(AddForceCoroutine(direction, amount));
    }

    IEnumerator AddForceCoroutine(Vector2 direction, float amount)
    {
        Vector2 forceDirection = direction + platformVelocity;
        rb.AddForce(forceDirection * amount, ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(0.1f);

        isAddingForce = false;
    }

    public void SetPlatformVelocity(Vector2 velocity)
    {
        platformVelocity = velocity;
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

    public void SetPosition(Vector2 newPos)
    {
        rb.position = newPos;
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

    public Vector2 GetDirectionLeftRightFromPlayer(Vector2 playerPos)
    {
        return playerPos.x < transform.position.x ? Vector2.left : Vector2.right;
    }

    public float GetDirectionMagnitude()
    {
        return faceDirection == Vector2.left ? -1 : 1;
    }

    public Vector2 GetWorldPosFromRelativePos(Vector2 relativePos)
    {
        return new Vector2(transform.position.x - relativePos.x * GetDirectionMagnitude(), transform.position.y + relativePos.y);
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
        
        #region Conserve Momentum
        
        // Wont slow player when they are moving in the same direction 
        if (data.moveData.doConserveMomentum && Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) &&
            Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f)
        {
            // Prevent any deceleration from happening
            accelRate = 0f;
        }
        
        #endregion

        float speedDiff = targetSpeed - rb.velocity.x;
        
        float movement = speedDiff * accelRate;
        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    #region Dash
    
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

    #endregion

    public void MovePosition(Vector2 velocity)
    {
        rb.MovePosition(rb.position + velocity);
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

    private void FixedUpdate()
    {
        if (isDashing || isAddingForce) return;
        
        if (platformVelocity != Vector2.zero)
        {
            MovePosition(platformVelocity);
        }
    }
}
