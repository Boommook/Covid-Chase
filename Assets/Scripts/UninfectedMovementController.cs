using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UninfectedMovementController : MonoBehaviour
{
    // public so the get is public and the set can be private
    // want to be accessible from other classes, but not changable
    public new Rigidbody2D rigidbody {
        get;
        private set;
    }

    private Vector2 direction = Vector2.down;
    private static Vector2 facing = Vector2.down;
    private static Vector2 position;
    
    public static Vector2 GetFacing(){
        return facing;
    }
    public static Vector2 GetPosition(){
        return position;
    }

    public float speed = 4.5f;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;
    

    /*
    private AnimatedSpriteRenderer spriteRendererUp;
    private AnimatedSpriteRenderer spriteRendererDown;
    private AnimatedSpriteRenderer spriteRendererLeft;
    private AnimatedSpriteRenderer spriteRendererRight;
    private AnimatedSpriteRenderer spriteRendererDeath;
    */

    /*
    public AnimatedSpriteRenderer coleSpriteRendererUp;
    public AnimatedSpriteRenderer coleSpriteRendererDown;
    public AnimatedSpriteRenderer coleSpriteRendererLeft;
    public AnimatedSpriteRenderer coleSpriteRendererRight;
    public AnimatedSpriteRenderer coleSpriteRendererDeath;
    */
    
    /*
    public AnimatedSpriteRenderer[] animatedSpritesToUse;
    public AnimatedSpriteRenderer[] coleAnimatedSprites;
    public AnimatedSpriteRenderer[] bomberAnimatedSprites;
    */


    private AnimatedSpriteRenderer activeSpriteRenderer;


    // automatically called by Unity when script is called regardless of whether script is enabled
    private void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
        if(PlayerPrefs.HasKey("PlayerOne")){
            if(PlayerPrefs.GetString("PlayerOne").Equals("Cole")){
                Debug.Log("Cole picked");
                for(int i = 0; i < 4; i++){
                    spriteRendererUp.animationSprites[i] = spriteRendererUp.char1AnimationSprites[i];
                    spriteRendererDown.animationSprites[i] = spriteRendererDown.char1AnimationSprites[i];
                    spriteRendererLeft.animationSprites[i] = spriteRendererLeft.char1AnimationSprites[i];
                    spriteRendererRight.animationSprites[i] = spriteRendererRight.char1AnimationSprites[i];
                    spriteRendererDeath.animationSprites[i] = spriteRendererDeath.char1AnimationSprites[i];
                }
                spriteRendererDeath.animationSprites[4] = spriteRendererDeath.char1AnimationSprites[4];
                spriteRendererDeath.animationSprites[5] = spriteRendererDeath.char1AnimationSprites[5];

                spriteRendererUp.idleSprite = spriteRendererUp.char1IdleSprite;
                spriteRendererDown.idleSprite = spriteRendererDown.char1IdleSprite;
                spriteRendererLeft.idleSprite = spriteRendererLeft.char1IdleSprite;
                spriteRendererRight.idleSprite = spriteRendererRight.char1IdleSprite;
                spriteRendererDeath.idleSprite = spriteRendererDeath.char1IdleSprite;
            }
            else{
                Debug.Log("Cristina chosen");
            }
        }

        else{
            Debug.Log("None chosen");
        }
        
        activeSpriteRenderer = spriteRendererDown;
        /*
        if(PlayerPrefs.HasKey("PlayerOne")){
            if(PlayerPrefs.GetString("PlayerOne").Equals("Cole")){
                Debug.Log("Cole");
                for(int i = 0; i < 5; i++){
                    animatedSpritesToUse[i] = coleAnimatedSprites[i];
                }
                bomberAnimatedSprites[1].GetComponent<SpriteRenderer>().enabled = false;
                bomberAnimatedSprites[1].GetComponent<AnimatedSpriteRenderer>().enabled = false;
            }
        }
        else{
            Debug.Log("No input");
            for( int i = 0; i < 5; i++){
                animatedSpritesToUse[i] = bomberAnimatedSprites[i];
            }
        }

        spriteRendererUp = animatedSpritesToUse[0];
        spriteRendererDown = animatedSpritesToUse[1];
        spriteRendererLeft = animatedSpritesToUse[2];
        spriteRendererRight = animatedSpritesToUse[3];
        spriteRendererDeath = animatedSpritesToUse[4];
        
        activeSpriteRenderer = spriteRendererDown;
        */
        
    }

    // called every frame that the game is running
    private void Update() {
        if(Input.GetKey(inputUp)){
            facing = Vector2.up;
            SetDirection(Vector2.up, spriteRendererUp);
        }
        else if(Input.GetKey(inputDown)){
            facing = Vector2.down;
            SetDirection(Vector2.down, spriteRendererDown);
        }
        else if (Input.GetKey(inputLeft)){
            facing = Vector2.left;
            SetDirection(Vector2.left, spriteRendererLeft);
        }
        else if( Input.GetKey(inputRight)){
            facing = Vector2.right;
            SetDirection(Vector2.right, spriteRendererRight);
        }
        // not moving
        else{
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
    }

    // called every physics update, runs on a fixed time interval
    private void FixedUpdate() {
        position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    // set the direction
    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer){
        direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Cough") && !UninfectedItems.GetImmunity()){
           DeathSequence();
        }
    }

    private void DeathSequence(){
        // disable the movement controls
        enabled = false;

        spriteRendererUp.enabled=false;
        spriteRendererDown.enabled=false;
        spriteRendererLeft.enabled=false;
        spriteRendererRight.enabled=false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded(){
        gameObject.SetActive(false);
        if(SceneManager.GetActiveScene().name == "GamePhase1"){
            FindAnyObjectByType<GameManager>().FirstPlayerDead();
        }
        else if(SceneManager.GetActiveScene().name == "GamePhase2"){
            FindAnyObjectByType<GameManager2>().SecondPlayerDead();
        }
    }
}
