using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public LevelId SelectedLevel { get; private set; } = LevelId.MysticCult;

    public int LastScore { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSelectedLevel(LevelId level)
    {
        SelectedLevel = level;
    }

    public void SetSelectedLevel(int levelId)
    {
        SelectedLevel = (LevelId)levelId;
    }

    public void SetScore(int score)
    {
        LastScore = score;
    }
}
