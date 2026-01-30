using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private LevelId level;
    [SerializeField] private string gameSceneName = "Game";

    public void OnClick()
    {
        if (LevelManager.Instance == null)
        {
            Debug.LogError("GameState is missing in the first scene.");
            return;
        }

        LevelManager.Instance.SetSelectedLevel(level);
        SceneManager.LoadScene(gameSceneName);
    }
}