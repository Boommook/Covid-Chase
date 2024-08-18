using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void BackToMenuClicked(){
        SceneManager.LoadScene("Main Menu");
    }
}
