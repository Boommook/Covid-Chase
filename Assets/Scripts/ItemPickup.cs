using System.Collections;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // enum can make an enumerated list of names
    public enum ItemType{
        Mask, Vaccine, Yardstick
    }
    
    public ItemType type;

    private void OnItemPickup(GameObject player){
        switch(type){
            case ItemType.Mask:
                player.GetComponent<UninfectedItems>().AddMask();
                Destroy(gameObject);
                break;
            case ItemType.Vaccine:
                player.GetComponent<UninfectedItems>().AddVaccine();
                Destroy(gameObject);
                break;
            case ItemType.Yardstick:
                Debug.Log("1");
                player.GetComponent<UninfectedItems>().AddYardsticks();
                Destroy(gameObject);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Uninfected")){
            OnItemPickup(other.gameObject);
        }
    }
    
}
