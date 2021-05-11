using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BallSpawner : MonoBehaviour
{
    BoxCollider2D ballDespawnCollider;
    [SerializeField] GameObject ballPrefab;

    void Start()
    {
        ballDespawnCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {        
        if(other.gameObject.GetComponent<BallControl>())
        {
            Destroy(other.gameObject);
            Instantiate(ballPrefab, new Vector3( Random.Range(-2f, 2f), -4f, 0f),Quaternion.identity);
        }
    }

}
