using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string sceneToLoad = "MainLevel"; // <-- 請確認這個名稱正確

    public void StartTheGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
