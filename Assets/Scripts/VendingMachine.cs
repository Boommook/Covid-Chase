using System.Collections;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{

    public LayerMask vendingMachineLayerMask;

    public BoxCollider2D machineCollider;

    public SpriteRenderer machineNotReady;
    public SpriteRenderer machineReady;
    public SpriteRenderer machineDispensing;
    private SpriteRenderer machineState;

    public KeyCode inputForUse = KeyCode.LeftShift;

    public float chanceOfReady = 0.05f;

    public GameObject[] spawnableItems;

    private void Awake(){
        machineState = machineNotReady;
    }

    private void Update() {

        if(machineState == machineNotReady){
            if(Random.Range(0f, 10f) < chanceOfReady){
                machineNotReady.enabled = false;
                machineReady.enabled = true;
                machineState = machineReady;
            }
        }

        if(Input.GetKeyDown(inputForUse)){

            Vector2 placePosition = UninfectedMovementController.GetPosition();
            Vector2 facing = UninfectedMovementController.GetFacing();

            placePosition.x = Mathf.Round(2 * placePosition.x) / 2;
            placePosition.y = Mathf.Round(2 * placePosition.y) / 2;

            if(facing == Vector2.up){
                // this is because the machine collider and player collider are sized oddly, so it would overshoot if it was just 1
                placePosition.y += 0.5f;
            }
            else if(facing == Vector2.down){
                placePosition.y--;
            }
            else if(facing == Vector2.left){
                placePosition.x--;
            }
            else if(facing == Vector2.right){
                placePosition.x++;
            }

            if(machineCollider.OverlapPoint(placePosition) && machineState == machineReady){
                StartCoroutine(Dispense());
            }
        }
    }
    
    private IEnumerator Dispense(){
        machineReady.enabled = false;
        machineDispensing.enabled = true;

        yield return new WaitForSeconds(1);

        // DISPENSE ITEM
        if(spawnableItems.Length > 0){
            int randIndex = Random.Range(0, spawnableItems.Length);
            if(transform.CompareTag("DownPointingMachine")){
                Vector2 spawnPos = transform.position;
                spawnPos.y--;
                Instantiate(spawnableItems[randIndex], spawnPos, Quaternion.identity);
            }
            else if(transform.CompareTag("LeftPointingMachine")){
                Vector2 spawnPos = transform.position;
                spawnPos.x--;
                Instantiate(spawnableItems[randIndex], spawnPos, Quaternion.identity);
            }
            else if(transform.CompareTag("RightPointingMachine")){
                Vector2 spawnPos = transform.position;
                spawnPos.x++;
                Instantiate(spawnableItems[randIndex], spawnPos, Quaternion.identity);
            }
        }

        machineDispensing.enabled = false;
        machineNotReady.enabled = true;
        machineState = machineNotReady;
    }
    
}
