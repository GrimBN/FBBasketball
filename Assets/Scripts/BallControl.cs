using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class BallControl : MonoBehaviour
{        
    Rigidbody2D ballRigidbody2D;
    CircleCollider2D ballCollider2D;
    Animator ballAnimator;
    Touch touch;
    [SerializeField] float minFingerSpeed = 0.1f, minYMoveDistance = 0.2f, verticalForce = 5f, torqueMultiplier = 0.5f, horizontalForceMultiplier = 1.5f;                           
    [SerializeField] AudioClip collisionSFX;
    [Range(0,1)][SerializeField] float volume = 0.4f;

    Vector2 initialTouchPos = Vector2.negativeInfinity, newTouchPos = Vector2.negativeInfinity;
    bool hasScored = false;

    void Start()
    {
        Input.simulateMouseWithTouches = false;
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        ballCollider2D = GetComponent<CircleCollider2D>();
        ballAnimator = GetComponent<Animator>();
    }

    /*private void OnMouseDown()
    {        
        LaunchBall(testingHorizontalForce);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(collisionSFX, Camera.main.transform.position, volume);
    }

    void Update()
    {
        ProcessTouchInput();        
    }

    private void ProcessTouchInput()
    {        
        if(Input.touchCount > 0 && !ballAnimator.GetBool("Launched"))
        {            
            touch = Input.GetTouch(0);            

            if(touch.phase == TouchPhase.Began && ballCollider2D.OverlapPoint(Camera.main.ScreenToWorldPoint(touch.position)))
            {                
                initialTouchPos = touch.position;
                newTouchPos = initialTouchPos;                
            }
            
            else if(touch.phase == TouchPhase.Ended && ballCollider2D.OverlapPoint(Camera.main.ScreenToWorldPoint(initialTouchPos)))
            {                
                newTouchPos = touch.position;
                float fingerSpeed = touch.deltaPosition.magnitude / touch.deltaTime;                
                if (fingerSpeed > minFingerSpeed && (Camera.main.ScreenToWorldPoint(newTouchPos).y - Camera.main.ScreenToWorldPoint(initialTouchPos).y) > minYMoveDistance)
                {
                    float xDifference = Camera.main.ScreenToWorldPoint(newTouchPos).x - Camera.main.ScreenToWorldPoint(initialTouchPos).x;
                    float horizontalForce = xDifference;

                    LaunchBall(horizontalForce * horizontalForceMultiplier);
                }
            }
        }
    }

    private void LaunchBall(float horizontalForce)
    {
        ballAnimator.SetBool("Launched", true);
        ballRigidbody2D.constraints = RigidbodyConstraints2D.None;
        ballRigidbody2D.AddForce(new Vector2(horizontalForce, verticalForce),ForceMode2D.Impulse);
        ballRigidbody2D.AddTorque(-horizontalForce * torqueMultiplier, ForceMode2D.Impulse);
        
    }

    public void SetBallColliderEnabled(bool value)
    {
        ballCollider2D.enabled = value;
    }

    public void SetHasScoredTrue()
    {
        hasScored = true;
    }

    public bool GetHasScored()
    {
        return hasScored;
    }

    private void ChangeBallPos(Vector2 screenPos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        ballRigidbody2D.position = new Vector2(worldPos.x,ballRigidbody2D.position.y);
    }
}
