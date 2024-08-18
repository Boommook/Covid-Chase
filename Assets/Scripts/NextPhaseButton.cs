
using UnityEngine;

public class nextPhaseButton : MonoBehaviour
{

    public GameManager gameManager;
    public void Clicked()
    {
        gameManager.LoadSceneAndKeepValue();
    }
}
