using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene()
    {
        IDCreator.ResetIDs();
        IDHolder.ClearIDHoldersList();
        Command.CommandQueue.Clear();
        Command.CommandExecutionComplete();
        string buttonClicked = EventSystem.current.currentSelectedGameObject.name;
        switch (buttonClicked)
        {
            case "PlayerVsAI":
                GameManager.IsHeadlessMode = false;
                SceneManager.LoadScene("Scene_PlayervsAI");
                break;
            case "AIvsAI":
                GameManager.IsHeadlessMode = true;
                SceneManager.LoadScene("Scene_AIvsAI");
                break;
            case "Quit":
                Application.Quit();
                break;
            case "Menu":
                SceneManager.LoadScene("Scene_Menu");
                break;
            default:
                SceneManager.LoadScene("Scene_Menu");
                break;
        }

    }
}
