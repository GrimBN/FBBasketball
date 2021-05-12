using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketMovement : MonoBehaviour
{
    //TODO: stop coroutine stuff
    [SerializeField] float timeInterval = 0.1f, baseDistancePerInterval = 0.1f, dPIIncrease = 0.05f, maxPos = 2f;
    [SerializeField] int startBasketMovementScore = 10, basketSpeedupScoreInterval = 10;
    float distancePerInterval;
    BasketTracker basketTracker;

    private void Start()
    {
        basketTracker = GetComponent<BasketTracker>();        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            StartCoroutine(MoveBasket());
        }
    }

    public IEnumerator MoveBasket()
    {
        distancePerInterval = baseDistancePerInterval;
        while (basketTracker.GetScore() >= startBasketMovementScore)
        {
            while (transform.position.x < maxPos)
            {
                transform.Translate(distancePerInterval, 0f, 0f);
                yield return timeInterval;
            }

            while (transform.position.x > -maxPos)
            {
                transform.Translate(-distancePerInterval, 0f, 0f);
                yield return timeInterval;
            }
        }
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
