using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{

    public void Back(){
        SceneManager.LoadScene("Main Menu");
    }
}
