using System.Collections;
using TMPro;
using UnityEngine;

public class UninfectedItems : MonoBehaviour
{
    public SpriteRenderer inventoryMask;
    public SpriteRenderer inventoryVaccine;

    public SpriteRenderer inventoryYardsticks;

    public TextMeshProUGUI textNumMasks;
    public TextMeshProUGUI textNumVaccines;
    public TextMeshProUGUI textNumYardsticks;

    private int numMasks = 1;
    private int numVaccines = 1;
    private int numYardsticks = 2;

    private static bool immune = false;
    public static bool GetImmunity(){
        return immune;
    }

    public KeyCode maskInput = KeyCode.Q;
    public KeyCode vaccineInput = KeyCode.E;
    public KeyCode yardstickInput = KeyCode.X;

    private void Awake() {
        immune = false;
    }

    private void Update(){
        InventoryDisplayer();
        
        if(Input.GetKeyDown(maskInput) && numMasks > 0){
            numMasks--;
            StartCoroutine(UseMaskItem());
        }

        if(Input.GetKeyDown(vaccineInput) && numVaccines > 0){
            numVaccines--;
            StartCoroutine(UseVaccineItem());
        }

        if(Input.GetKeyDown(yardstickInput) && numYardsticks >= 2){
            numYardsticks -= 2;
            StartCoroutine(UseYardsticksItem());
        }
    }

    private void InventoryDisplayer(){
        textNumMasks.text = numMasks.ToString();
        textNumVaccines.text = numVaccines.ToString();
        textNumYardsticks.text = numYardsticks.ToString();
        if(numMasks > 0){
            inventoryMask.enabled = true;
        }
        else if(numMasks == 0){
            inventoryMask.enabled = false;
        }
        if(numVaccines > 0){
            inventoryVaccine.enabled = true;
        }
        else if(numVaccines == 0){
            inventoryVaccine.enabled = false;
        }
        if(numYardsticks > 0){
            inventoryYardsticks.enabled = true;
        }
        else if(numYardsticks == 0){
            inventoryYardsticks.enabled = false;
        }
    }
    
    private IEnumerator UseMaskItem(){
        Debug.Log("Mask Used");
        immune = true;

        // MASK DURATION IS 5 SECONDS...MAY WANT TO CHANGE
        yield return new WaitForSeconds(5);

        immune = false;
    }

    private IEnumerator UseVaccineItem(){
        Debug.Log("Vaccine Used");

        // check if infected player is within range
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 3f);
        foreach(Collider2D col in collider){
            if(col.gameObject.CompareTag("Infected")){
                GameObject infectedPlayer = col.gameObject;
                infectedPlayer.GetComponent<InfectedMovementController>().speed--;
                infectedPlayer.GetComponent<CoughController>().enabled = false;
                
                yield return new WaitForSeconds(5);

                infectedPlayer.GetComponent<CoughController>().enabled = true;

                yield return new WaitForSeconds(2.5f);

                infectedPlayer.GetComponent<InfectedMovementController>().speed++;
            }
        }
    }

    private IEnumerator UseYardsticksItem(){
        Debug.Log("Yardstick Used");
        gameObject.GetComponent<CircleCollider2D>().enabled = true;

        // MASK DURATION IS 5 SECONDS...MAY WANT TO CHANGE
        yield return new WaitForSeconds(5);

        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    public void AddMask(){
        numMasks++;
    }

    public void AddVaccine(){
        numVaccines++;
    }

    public void AddYardsticks(){
        numYardsticks++;
    }
}
