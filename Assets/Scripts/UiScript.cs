using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiScript : MonoBehaviour {

    public static UiScript Instance { get; private set; }
   
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("WalkableScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Bye Bye!");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
