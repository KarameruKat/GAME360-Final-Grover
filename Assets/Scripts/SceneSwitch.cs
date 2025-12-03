using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {

    public string sceneToLoad;
    public float timeRemaining;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            timeRemaining = 0;
            SceneManager.LoadSceneAsync(sceneToLoad);
        }
    }
    public float GetTimeRemaining() => timeRemaining;
}
