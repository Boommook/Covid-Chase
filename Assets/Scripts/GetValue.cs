using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetValue : MonoBehaviour
{
    [SerializeField] Text myText;

    public void LoadSceneAndKeepValue(){
        string dataToKeep = myText.text;
        StaticData.valueToKeep = dataToKeep;
        SceneManager.LoadScene("GamePhase2");
    }
}
