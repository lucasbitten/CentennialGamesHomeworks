using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    float horizontalMovement;

    [SerializeField]
    float speed = 0;
    [SerializeField]
    float maxSpeed = 8;

    [SerializeField]
    float acceleration = 10;
    [SerializeField]
    float deceleration = 10;

    [SerializeField]
    float runningSpeed = 8;
    [SerializeField]
    float walkSpeed = 5;

    [SerializeField]
	private bool grounded;

	[SerializeField]
	private LayerMask whatIsGround;  
	[SerializeField]
	private Transform groundCheck;

	[SerializeField]
	private float groundCheckRadius;

    [SerializeField]
    float fallMultiplier = 2.5f;
    [SerializeField]
    float lowJumpMultiplier = 2f;
    [SerializeField]
    float jumpVelocity;
    public bool jumping;

    [SerializeField]
    Animator[] animators;

    Animator animator;
    float scale;

    [SerializeField]
    GameObject small;
    [SerializeField]
    GameObject big;

    public bool isBig;
    [SerializeField]
    GameObject flower;
    public bool hasFlower;
    BoxCollider2D col;

    [SerializeField]
    Transform top;

    [SerializeField]
    FlowerPower flowerPower;

    public bool hasStar;

    float timer = 15;

    AudioManager audioManager;

    public float coolDown = 5;
    public bool hitted;
    public bool died;
    bool levelComplete;
    bool levelFinished;
    [SerializeField]
    GameObject flag;
    [SerializeField]
    GameObject levelScreen;
    [SerializeField]
    GameObject gameOverScreen;
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        scale = transform.localScale.x;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        maxSpeed = walkSpeed;
        animator = animators[0];
        died = true;
        transform.position = new Vector3 (-4.03f, transform.position.y, transform.position.z);

        if(GameManager.instance.die){
            transform.position = new Vector3 (GameManager.instance.startPosition, transform.position.y, transform.position.z);
            GameManager.instance.lives--;
        }
        StartCoroutine(LoadLevel());

    }



    IEnumerator LoadLevel()
    {
        
        yield return new WaitForSeconds(3);
        levelScreen.SetActive(false);
        audioManager.Play("Music");

        died = false;
    }

    void Update()
    {
    

        if(!levelFinished){
            animator.SetBool("LevelCompleto", levelComplete);
            animator.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
            animator.SetBool("Jumping", jumping);
            animator.SetBool("HasStar", hasStar);
        } else
        {
            animator.SetBool("LevelCompleto", false);
            animator.SetFloat("Speed", 2);
            animator.SetBool("Jumping", false);
            animator.SetBool("HasStar", false);
        }


        if(animator == animators[0]){
            animator.SetBool("Hitted", hitted);
            animator.SetBool("Dead",died);
        }
        if (!died && !levelComplete){

            if(ScoreManager.scoreManager.time <= 0){
                Die();
            }

            grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

            if(Input.GetKey(KeyCode.A) && speed > 0 && myRigidbody.velocity.x < 0.1f){
                speed = 0;
            } else if (Input.GetKey(KeyCode.A) && speed > -maxSpeed){
                speed = speed - acceleration*Time.deltaTime;
                transform.localScale = new Vector3 (-scale, transform.localScale.y, transform.localScale.z);

            }else if(Input.GetKey(KeyCode.D) && speed < 0 && myRigidbody.velocity.x > -0.1f){
                speed = 0;
            } 
            else if (Input.GetKey(KeyCode.D) && speed < maxSpeed){
                speed = speed + acceleration*Time.deltaTime;
                transform.localScale = new Vector3 (scale, transform.localScale.y, transform.localScale.z);

            }else
            {
                if (speed > deceleration*Time.deltaTime){
                    speed = speed - deceleration*Time.deltaTime;
                } else if (speed < -deceleration*Time.deltaTime){
                    speed = speed + deceleration*Time.deltaTime;
                }else
                {
                    speed = 0;
                }
            }


            if(Input.GetKeyDown(KeyCode.LeftShift) && hasFlower){
                FlowerPower flowerPwr = Instantiate(flowerPower, transform.position, Quaternion.identity);
                if(transform.localScale.x < 0){
                    flowerPwr.forceX = - flowerPower.forceX;
                }


                audioManager.Play("FlowerPower");
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                maxSpeed = runningSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                maxSpeed = walkSpeed;
            }

            if (grounded){
                jumping = false;

            }

            if (Input.GetKeyDown (KeyCode.Space) && grounded) {
                myRigidbody.velocity = (Vector2.up * jumpVelocity);
                audioManager.Play("Jump");
            }

            if(myRigidbody.velocity.y > 0.15f)
            {
                jumping = true;

            }

            if (myRigidbody.velocity.y < 0){
                myRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1)*Time.deltaTime;
            } else if (myRigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space)){
                myRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1)*Time.deltaTime;

            }

            if (hasStar)
            {
                timer -= Time.deltaTime;

                if(timer <= 0)
                {
                    hasStar = false;
                }
            }


            if (hitted){
                coolDown -=Time.deltaTime;

                if(coolDown <= 0){
                    hitted = false;
                    coolDown = 5;
                    Physics2D.IgnoreLayerCollision(10,14, false);

                }
            }


        }
        
    }


    private void FixedUpdate() {

        if (!died && !levelComplete){
            myRigidbody.velocity = new Vector2( speed, myRigidbody.velocity.y);

        }

        if(levelComplete){
            flag.GetComponent<Rigidbody2D>().gravityScale = 1;
            if (flag.transform.localPosition.y  <= -4){
                flag.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                flag.GetComponent<Rigidbody2D>().gravityScale = 0;
                
            }
        }

    }



    public void Die(){
        died = true;
        jumping = false;
        for (int i = 0; i < GetComponentsInChildren<Collider2D>().Length ; i++)
        {
            GetComponentsInChildren<Collider2D>()[i].enabled = false;
        }
        myRigidbody.velocity = new Vector2(0, 8);
        myRigidbody.gravityScale = 2;
        audioManager.Stop("Music");

        if (GameManager.instance.lives > 1){
            audioManager.Play("Death");
            StartCoroutine(RestartLevel());

        } else
        {
            audioManager.Play("GameOver");
            StartCoroutine(RestartGame());

        }

    }

    IEnumerator RestartGame()
    {
        
        yield return new WaitForSeconds(4f);
        gameOverScreen.SetActive(true);
        levelScreen.SetActive(true);
        Destroy(GameManager.instance.gameObject);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    IEnumerator RestartLevel()
    {
        
        yield return new WaitForSeconds(3f);
        GameManager.instance.die= true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }


    public void GetBigger(){
        isBig = true;
        animator = animators[1];
        big.SetActive(true);
        small.SetActive(false);
        flower.SetActive(false);
        col.offset = new Vector2 (0,0.5f);
        col.size = new Vector2 (col.size.x, 2);
        top.localPosition = new Vector3(0,1,0);
    }

    public void GetFlower(){
        animator = animators[2];
        isBig = true;
        hasFlower = true;
        big.SetActive(false);
        small.SetActive(false);
        flower.SetActive(true);
        col.offset = new Vector2 (0,0.5f);
        col.size = new Vector2 (col.size.x, 2);
        top.localPosition = new Vector3(0,1,0);
    }

    public void TakeDamage(){

        if (!isBig && !hasFlower){
            Die();
        } else
        {
            audioManager.Play("Hit");
            Physics2D.IgnoreLayerCollision(10,14, true);
            hitted = true;
            animator = animators[0];
            isBig = false;
            hasFlower = false;
            big.SetActive(false);
            small.SetActive(true);
            flower.SetActive(false);
            col.offset = new Vector2 (0,0);
            col.size = new Vector2 (col.size.x, 1);
            top.localPosition = new Vector3(0,0,0);       
        }



    }


    public void GetStar()
    {
        timer = 15;
        hasStar = true;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Mushroom"){
            ScoreManager.scoreManager.AddScore(1000, transform.position);
            audioManager.Play("PickPowerUp");
            GetBigger();
            Destroy(other.transform.parent.gameObject);
        } 

       if (other.gameObject.tag == "Flower"){
            ScoreManager.scoreManager.AddScore(1000, transform.position);
            audioManager.Play("PickPowerUp");
            GetFlower();
           Destroy(other.transform.parent.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CompleteLevel"){
            ScoreManager.scoreManager.AddScore(5000,transform.position);
            audioManager.Stop("Music");
            audioManager.Play("LevelComplete");
            levelComplete = true;
            myRigidbody.velocity = Vector2.zero;
            other.GetComponent<BoxCollider2D>().enabled = false;
        }

        if(other.tag == "CheckPoint"){
            GameManager.instance.startPosition = other.transform.position.x;
        }

        if(other.tag == "FinishLevel1"){
            StartCoroutine(LevelCompleto());
            levelFinished = true;
        }
        if(other.tag == "FinishLevel2"){
            myRigidbody.velocity = Vector2.zero;
            small.SetActive(false);
            big.SetActive(false);
            flower.SetActive(false);
            StartCoroutine(ResetGame());
        }

    }


    IEnumerator ResetGame()
    {
        
        yield return new WaitForSeconds(3);
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene(0);    
    }

    IEnumerator LevelCompleto()
    {
        
        yield return new WaitForSeconds(0.5f);
        myRigidbody.velocity = new Vector2(2,0);
        animator.SetFloat("Speed", 2);
    }





}
