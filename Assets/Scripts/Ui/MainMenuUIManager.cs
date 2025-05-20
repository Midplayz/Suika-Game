using UnityEngine;
using MaskTransitions;

public class MainMenuUIManager : MonoBehaviour
{
    public void LoadMainSence()
    {
        TransitionManager.Instance.LoadLevel("MainGame");
    }
    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}
