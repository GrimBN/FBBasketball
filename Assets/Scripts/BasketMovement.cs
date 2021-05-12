using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketMovement : MonoBehaviour
{    
    [SerializeField] float timeInterval = 0.1f, baseDistancePerInterval = 0.1f, dPIIncrease = 0.05f, maxPos = 2f;
    [SerializeField] int startBasketMovementScore = 10, basketSpeedupScoreInterval = 10;
    float distancePerInterval;
    BasketTracker basketTracker;

    Coroutine moveBasketCoroutine;

    private void Start()
    {
        basketTracker = GetComponent<BasketTracker>();        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            StartCoroutine(MoveBasketCoroutine());
        }
    }

    public IEnumerator MoveBasketCoroutine()
    {
        distancePerInterval = baseDistancePerInterval;
        while (basketTracker.GetScore() >= startBasketMovementScore)
        {
            while (transform.position.x < maxPos && basketTracker.GetScore() >= startBasketMovementScore)
            {
                transform.Translate(distancePerInterval, 0f, 0f);
                yield return timeInterval;
            }

            while (transform.position.x > -maxPos && basketTracker.GetScore() >= startBasketMovementScore)
            {
                transform.Translate(-distancePerInterval, 0f, 0f);
                yield return timeInterval;
            }
        }
    }

    public void MoveBasket()
    {
        moveBasketCoroutine = StartCoroutine(MoveBasketCoroutine());
    }

    public void ResetBasket()
    {
        if (moveBasketCoroutine != null)
        {
            StopCoroutine(moveBasketCoroutine);
        }
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
    }

    public void IncrementDistancePerInterval()
    {
        distancePerInterval += dPIIncrease;
    }

    public int GetMovementStartScore()
    {
        return startBasketMovementScore;
    }

    public int GetSpeedupScoreInterval()
    {
        return basketSpeedupScoreInterval;
    }
   
}
