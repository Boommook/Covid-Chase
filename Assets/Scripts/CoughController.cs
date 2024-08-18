using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class CoughController : MonoBehaviour
{
    public KeyCode inputKey = KeyCode.Space;


    // keyword means that any prefab passed in must have the Explosion script
    public GameObject coughPrefab;
    
    // the two cough objects will end at the same time so don't need a variable for the first cough duration
    public float timeToInfectNextTile = 1;
    public float latterCoughDuration = 1;

    /*
    private void OnEnable() {

    }
    */

    private void Update() {
        if(Input.GetKeyDown(inputKey)){
            StartCoroutine(PlayerCough());
        }
    }

    private IEnumerator PlayerCough(){
        
        Vector2 position = transform.position;
        Vector2 placePosition = position;
        Vector2 nextPosition = placePosition;

        Vector2 facing = InfectedMovementController.GetFacing();
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        if(facing == Vector2.up){
            placePosition.y = position.y + 1;
            nextPosition.y = placePosition.y + 1;
            
        }
        else if(facing == Vector2.down){
            placePosition.y = position.y - 1;
            nextPosition.y = placePosition.y - 1;
        }
        else if(facing == Vector2.left){
            placePosition.x = position.x - 1;
            nextPosition.x = placePosition.x - 1;
        }
        else if(facing == Vector2.right){
            placePosition.x = position.x + 1;
            nextPosition.x = placePosition.x + 1;
        }


        // make an instance of the cough prefab at the position that is a game object
        GameObject firstCough = Instantiate(coughPrefab, placePosition, Quaternion.identity);
        
        yield return new WaitForSeconds(timeToInfectNextTile);

        GameObject secondCough = Instantiate(coughPrefab, nextPosition, Quaternion.identity);

        yield return new WaitForSeconds(latterCoughDuration);

        Destroy(firstCough);
        Destroy(secondCough);

    }

    /*
    private void OnTriggerExit2D(Collider2D other) {
        // if the collider is a bomb...
        if(other.gameObject.layer == LayerMask.NameToLayer("Bomb")) {
            other.isTrigger = false;
        }
    }
    */
}
