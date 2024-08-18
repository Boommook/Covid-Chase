using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickCharacter : MonoBehaviour
{

    public void Cole(){
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("PlayerOne", "Cole");  
        SceneManager.LoadScene("GamePhase1");
    }

    public void Cristina(){
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("PlayerOne", "Cristina"); 
        SceneManager.LoadScene("GamePhase1");
    }
}