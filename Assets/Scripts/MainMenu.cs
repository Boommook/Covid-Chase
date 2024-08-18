using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame(){
        SceneManager.LoadScene("Pick Character");
    }

    public void Instructions(){
        SceneManager.LoadScene("Instructions");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
