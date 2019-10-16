using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int score = 0;
    public int coins = 0; 
    public GameObject levelScreen;
    public PlayerController player;
    public bool die;
    public float startPosition;

    public int lives = 3;

    private void Awake()
    {
        startPosition = -4.03f;
        DontDestroyOnLoad(this.gameObject);

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

}
