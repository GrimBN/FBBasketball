using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BallControl : MonoBehaviour
{
    Rigidbody2D ballRigidbody2D;
    CircleCollider2D ballCollider2D;
    Touch touch;
    [SerializeField] float minFingerSpeed = 0.1f, minYMoveDistance = 0.2f, verticalForce = 5f;
    //[SerializeField] Text text,text2;

    Vector2 initialTouchPos = Vector2.negativeInfinity, newTouchPos = Vector2.negativeInfinity;

    void Start()
    {
        Input.simulateMouseWithTouches = false;
        ballRigidbody2D = GetComponent<Rigidbody2D>();
        ballCollider2D = GetComponent<CircleCollider2D>();        
    }

    /*private void OnMouseDown()
    {        
        LaunchBall();
    }*/

    void Update()
    {
        ProcessTouchInput();        
    }

    private void ProcessTouchInput()
    {        
        if(Input.touchCount > 0)
        {            
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)// && ballCollider2D.OverlapPoint(Camera.main.ScreenToWorldPoint(touch.position)))
            {
                //text.text = "started";
                initialTouchPos = touch.position;
                newTouchPos = initialTouchPos;
                //ChangeBallPos(newTouchPos);
            }

            if(touch.phase == TouchPhase.Moved)// && ballCollider2D.OverlapPoint(initialTouchPos))
            {
                //text.text = "Moving";
                newTouchPos = touch.position;
                //ChangeBallPos(newTouchPos);
            }  
            
            if(touch.phase == TouchPhase.Ended)// && ballCollider2D.OverlapPoint(initialTouchPos))
            {                
                newTouchPos = touch.position;
                float fingerSpeed = touch.deltaPosition.magnitude / touch.deltaTime;
                //text.text = fingerSpeed.ToString();
                //text2.text = (newTouchPos.y - initialTouchPos.y).ToString();
                if (fingerSpeed > minFingerSpeed && Mathf.Abs(newTouchPos.y - initialTouchPos.y) > minYMoveDistance)
                {
                    LaunchBall();
                }
            }
        }
    }

    private void LaunchBall()
    {
        ballRigidbody2D.constraints = RigidbodyConstraints2D.None;
        ballRigidbody2D.AddForce(new Vector2(0f, verticalForce),ForceMode2D.Impulse);
    }

    private void ChangeBallPos(Vector2 screenPos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        ballRigidbody2D.position = new Vector2(worldPos.x,ballRigidbody2D.position.y);
    }
}
