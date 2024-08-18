using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{

    public GameObject uninfectedPlayer;
    public GameObject infectedPlayer;

    private float score;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI player1WinText;
    public TextMeshProUGUI player2WinText;
    public TextMeshProUGUI tieGameText;

    public GameObject backToMenuButton;

    public Text currentScore;
    [SerializeField] Text previousScore;

    private bool alive;

    // USED FOR SPAWNING BUSHES:
    public GameObject smallBushPrefab;
    public GameObject bigBushLeftPrefab;
    public GameObject bigBushRightPrefab;
    public Tilemap road;
    public GameObject stage;
    private List<Vector2> smallBushes = new List<Vector2>();
    

    private void Awake() {
        Debug.Log("Awake");
        string newText = StaticData.valueToKeep;
        previousScore.text = newText;
        alive = true;
        
        SpawnSmallBushes();
        SpawnBigBushes();
    }
    
    private void Update() {
        if (alive){
            score += Time.deltaTime;
            currentScore.text = Mathf.FloorToInt(score).ToString("D4");
        }
    }

    private void SpawnSmallBushes(){
        for(float y = 7f; y >= -6; y -= 0.5f){
            for(float x = -14.5f; x <= 13.5; x += 0.5f){
                // if the random number from 0 to 125 is 1...
                if(UnityEngine.Random.Range(0, 125) == 1){
                    // make a new Vector2 with the coords
                    Vector2 placeVector = new Vector2(x, y);
                    // if the coords and the coords half a tile left and half a tile right do not overlap a collider...
                    if(Physics2D.OverlapPointAll(placeVector).Length == 0 && 
                        Physics2D.OverlapPointAll(new Vector2(x + 1, y)).Length == 0 &&
                        Physics2D.OverlapPointAll(new Vector2(x - 1, y)).Length == 0){
                            // if the coords do not overlap the road
                        if(!road.HasTile(new Vector3Int((int)placeVector.x, (int)placeVector.y, 0)) 
                        && !road.HasTile(new Vector3Int((int)placeVector.x + 1, (int)placeVector.y, 0))
                        && !road.HasTile(new Vector3Int((int)placeVector.x - 1, (int)placeVector.y, 0))){
                            // make a bool for if the coords overlap an existing bush
                            bool overlapBush = false;
                            // for each existing bush coordinate...
                            foreach(Vector2 pos in smallBushes){
                                // if the coords or the coords half a tile up or half a tile down overlap an existing bush...
                                if(placeVector == pos
                                    || placeVector.y + 0.5 == pos.y || placeVector.y - 0.5 == pos.y){
                                    // set the bool to true
                                    overlapBush = true;
                                }
                            }
                            // if the coords do not overlap any existing bushes...
                            if(!overlapBush){
                                // create the bush at the coords and make it a child of the stage gameobject
                                (Instantiate (smallBushPrefab, placeVector, Quaternion.identity) as GameObject).transform.parent = stage.transform;
                                // add the coords of the new bush to the existing bush list
                                smallBushes.Add(placeVector);
                                // increment the x value by 0.5
                                x += 0.5f;
                            }
                        }
                    }
                }
            }
        }
    }

    private void SpawnBigBushes(){
        for(float y = 7f; y >= -6; y -= 0.5f){
            for(float x = -14.5f; x <= 13.5; x += 0.5f){
                if(UnityEngine.Random.Range(0, 125) == 1){
                    Vector2 placeVector1 = new Vector2(x, y);
                    Vector2 placeVector2 = new Vector2(x + 0.5f, y);
                    if(Physics2D.OverlapPointAll(placeVector1).Length == 0 && Physics2D.OverlapPointAll(placeVector2).Length == 0){
                        if(!road.HasTile(new Vector3Int((int)placeVector1.x, (int)placeVector1.y, 0)) && 
                        !road.HasTile(new Vector3Int((int)placeVector2.x, (int)placeVector2.y, 0)) &&
                        !road.HasTile(new Vector3Int((int)placeVector2.x + 1, (int)placeVector2.y, 0)) &&
                        !road.HasTile(new Vector3Int((int)placeVector1.x - 1, (int)placeVector2.y, 0))){
                            bool overlapBush = false;
                            foreach(Vector2 pos in smallBushes){
                                if(placeVector1 == pos || placeVector2 == pos
                                    || placeVector1.y + 1 == pos.y || placeVector1.y - 1 == pos.y
                                    || placeVector2.y + 1 == pos.y || placeVector2.y - 1 == pos.y){
                                    overlapBush = true;
                                }
                            }
                            if(!overlapBush){
                                (Instantiate (bigBushLeftPrefab, placeVector1, Quaternion.identity) as GameObject).transform.parent = stage.transform;
                                (Instantiate (bigBushRightPrefab, placeVector2, Quaternion.identity) as GameObject).transform.parent = stage.transform;
                                x += 1f;
                            }
                        }
                    }
                }
            }
        }
    }

    /*
    public GameObject GetInfectedPlayer(){
        return infectedPlayer;
    }
    */

    /*
    public void Next(){
        if(round == 1){
            SecondPlayerDead();
        }
        else{
            // GAME OVER
        }
    }
    */

    public void SecondPlayerDead(){
        Debug.Log("End");
        alive = false;
        gameOverText.enabled = true;
        if(int.Parse(previousScore.text) > int.Parse(currentScore.text)){
            player1WinText.enabled = true;
        }
        else if(int.Parse(previousScore.text) < int.Parse(currentScore.text)) {
            player2WinText.enabled = true;
        }
        else{
            tieGameText.enabled = true;
        }
        backToMenuButton.SetActive(true);
    }

    /*
    private void UpdatePrevScore(){
        float prevScore = PlayerPrefs.GetFloat("prevscore", 0);

        if (score > prevScore)
        {
            prevScore = score;
            PlayerPrefs.SetFloat("prevscore", prevScore);
        }

        previousScoreText.text = Mathf.FloorToInt(prevScore).ToString("D5");
    }
    */
    
}
