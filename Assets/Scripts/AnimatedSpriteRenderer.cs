using System;
using System.Data.Common;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite idleSprite;
    public Sprite[] animationSprites;

    private string character;

    public Sprite char1IdleSprite;
    public Sprite[] char1AnimationSprites;
    
    // public so it is customizable in Unity for each object
    public float animationTime = 0.25f;
    private int animationFrame;

    public bool loop = true;
    public bool idle = true;

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // when the behavior is enabled, also enable the sprite renderer
    private void OnEnable() {
        spriteRenderer.enabled = true;
    }

    private void OnDisable() {
        spriteRenderer.enabled = false;
    }

    // when the sprite render starts, Start() is called on the first frame the sprite in enabled
    private void Start(){
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
        if(PlayerPrefs.HasKey("PlayerOne")){
            Debug.Log("AnimScript Cole picked");
            if(PlayerPrefs.GetString("PlayerOne").Equals("Cole")){
                character = "Cole";
                spriteRenderer.sprite = char1IdleSprite;
                Debug.Log("Start");
            }
            else{
                character = "Cristina";
                spriteRenderer.sprite = idleSprite;
            }
        }
    }

    // advance to the next frame of the animation
    private void NextFrame(){
        animationFrame++;

        if(loop && animationFrame >= animationSprites.Length){
            animationFrame = 0;
        }

        if(idle){
            if(character.Equals("Cole")){
                spriteRenderer.sprite = char1IdleSprite;
                Debug.Log("NextFrame idle");
            }
            else{
                spriteRenderer.sprite = idleSprite;
            }
        }
        else if(animationFrame >= 0 && animationFrame < animationSprites.Length){
            spriteRenderer.sprite = animationSprites[animationFrame];
            Debug.Log("NextFrame else if");
        }
    }
}
