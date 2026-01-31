using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectSimple : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Game";

    public void OnClick()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}