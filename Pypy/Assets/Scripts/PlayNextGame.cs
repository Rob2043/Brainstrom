using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayNextGame : MonoBehaviour
{
    public string sceneSelect;
    public void NextGame()
    {
        SceneManager.LoadScene($"Level {sceneSelect}");
    }
}
