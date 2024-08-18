using UnityEngine;

public class InfectedMovementController : MonoBehaviour
{
    // public so the get is public and the set can be private
    // want to be accessible from other classes, but not changable
    public new Rigidbody2D rigidbody {
        get;
        private set;
    }

    private Vector2 direction = Vector2.down;
    private static Vector2 facing = Vector2.down;
    public static Vector2 GetFacing(){
        return facing;
    }

    public float speed = 6;

    public KeyCode inputUp = KeyCode.UpArrow;
    public KeyCode inputDown = KeyCode.DownArrow;
    public KeyCode inputLeft = KeyCode.LeftArrow;
    public KeyCode inputRight = KeyCode.RightArrow;

    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    // automatically called by Unity when script is called
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
                }

                spriteRendererUp.idleSprite = spriteRendererUp.char1IdleSprite;
                spriteRendererDown.idleSprite = spriteRendererDown.char1IdleSprite;
                spriteRendererLeft.idleSprite = spriteRendererLeft.char1IdleSprite;
                spriteRendererRight.idleSprite = spriteRendererRight.char1IdleSprite;
            }
            else{
                Debug.Log("Cristina chosen");
            }
        }

        else{
            Debug.Log("None chosen");
        }
        
        activeSpriteRenderer = spriteRendererDown;
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
        Vector2 position = rigidbody.position;
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
}
