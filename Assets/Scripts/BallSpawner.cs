using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BallSpawner : MonoBehaviour
{
    BoxCollider2D ballDespawnCollider;
    [SerializeField] GameObject ballPrefab;
    BasketTracker basketTracker;

    void Start()
    {
        ballDespawnCollider = GetComponent<BoxCollider2D>();
        basketTracker = FindObjectOfType<BasketTracker>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BallControl ball;
        if (other.gameObject.TryGetComponent<BallControl>(out ball))
        {
            Destroy(other.gameObject);
            Instantiate(ballPrefab, new Vector3( Random.Range(-2f, 2f), -4f, 0f),Quaternion.identity);
            if (!ball.GetHasScored())
            {
                basketTracker.ResetScore();
            }
        }
    }

}
