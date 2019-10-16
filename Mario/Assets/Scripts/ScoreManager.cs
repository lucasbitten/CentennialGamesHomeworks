using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager scoreManager;


    [SerializeField]
    TextMeshProUGUI scoreText;
    int score;

    [SerializeField]
    TextMeshProUGUI coinsText;
    int coin;
    [SerializeField]
    TextMeshProUGUI timeText;
    public float time = 403;

    [SerializeField]
    TextMeshProUGUI points;
    [SerializeField]
    Canvas canvas;
    public int lives;
    Camera cam;
    [SerializeField]
    TextMeshProUGUI livesText;
    private void Start() {
        cam = FindObjectOfType<Camera>();

        if(scoreManager == null)
        {
            scoreManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        coin = GameManager.instance.coins;
        score = GameManager.instance.score;
        lives = GameManager.instance.lives;
        livesText.text = "x" + lives;
    }


    private void Update()
    {
        

        time -= Time.deltaTime;
        if (time <= 400){
            timeText.text = Mathf.RoundToInt(time).ToString();

        }else
        {
            timeText.text = "400";

        }
    }
    public void AddScore(int value, Vector3 pos)
    {
        pos = cam.WorldToScreenPoint(pos + new Vector3(0, 0.8f, 0));
        
        TextMeshProUGUI text = Instantiate(points, pos , Quaternion.identity, canvas.transform);
        text.text = value.ToString();
        score += value;
        GameManager.instance.score = score;
        scoreText.text = score.ToString("D6");
    }
    public void AddCoin()
    {
        coin++;
        GameManager.instance.coins = coin;
        coinsText.text = "x" + coin.ToString("D2");
    }


}
