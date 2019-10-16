using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    Vector3 startPosition = new Vector3(0, -7,0);

    [SerializeField]
    ScoreManager scoreManager;
    Rigidbody2D myRigdbody;

    [SerializeField]
    Text winner;

    bool gameEnded = false;
    bool gameStarted = false;
    private void Start() {
        myRigdbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){

            if(gameEnded){
                RestartGame();
            } else if (!gameStarted)
            {
                StartGame();

            }


        }
    }

    void StartGame(){
        gameStarted = true;
        transform.position = startPosition;
        myRigdbody.velocity = new Vector3 (speed, speed, 0);

    }


    void RestartGame(){
        gameEnded = false;
        gameStarted = false;
        winner.gameObject.SetActive(false);
        scoreManager.score1 = 0;
        scoreManager.score1Text.text = "" + scoreManager.score1;
        scoreManager.score2 = 0;
        scoreManager.score2Text.text = "" + scoreManager.score2;
        myRigdbody.velocity = new Vector3 (0, 0, 0);
        transform.position = startPosition;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Goal1")
        {
            scoreManager.score1++;
            scoreManager.score1Text.text = "" + scoreManager.score1;

            if (scoreManager.score1 == 11)
            {
                winner.text = "Player 1 Wins!";
                winner.gameObject.SetActive(true);
                gameEnded = true;
            }else
            {
                StartGame();              
            }


        }

        if (other.name == "Goal2")
        {
            scoreManager.score2++;
            scoreManager.score2Text.text = "" + scoreManager.score2;

            if (scoreManager.score1 == 11)
            {
                winner.text = "Player 2 Wins!";
                winner.gameObject.SetActive(true);

                gameEnded = true;
            } else{
                StartGame();

            }

        }

    }

}
