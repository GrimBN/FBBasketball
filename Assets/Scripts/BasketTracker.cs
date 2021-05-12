using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketTracker : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;
    [SerializeField] AudioClip scoreSFX, resetScoreSFX;
    BasketMovement basketMovement;
    AudioSource audioSource;
    //BoxCollider2D basketTriggerCollider;

    int score = 0;
    int highScore = -1;

    void Start()
    {
        //basketTriggerCollider = GetComponentInChildren<BoxCollider2D>(true);
        basketMovement = GetComponent<BasketMovement>();
        audioSource = GetComponent<AudioSource>();
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText)
        {
            scoreText.text = score.ToString();
        }

        if (score > highScore && highScoreText)
        {
            highScore = score;
            highScoreText.text = "High Score : " + highScore.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BallControl ball;
        if (other.gameObject.TryGetComponent(out ball))
        {
            if (!ball.GetHasScored())
            {
                ball.SetHasScoredTrue();
                IncreaseScore();
            }
        }
    }

    public void IncreaseScore()
    {
        score++;        
        UpdateScoreText();
        audioSource.PlayOneShot(scoreSFX, 0.3f);
        if(score == basketMovement.GetMovementStartScore())
        {
            basketMovement.MoveBasket();
        }
        else if(score >= basketMovement.GetMovementStartScore() && score % basketMovement.GetSpeedupScoreInterval() == 0)
        {
            basketMovement.IncrementDistancePerInterval();
        }
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
        audioSource.PlayOneShot(resetScoreSFX);
        basketMovement.ResetBasket();
    }

    public int GetScore()
    {
        return score;
    }
}
