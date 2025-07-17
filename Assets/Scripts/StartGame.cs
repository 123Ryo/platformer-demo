using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string sceneToLoad = "MainLevel"; // 遊戲主場景

    public void StartTheGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
