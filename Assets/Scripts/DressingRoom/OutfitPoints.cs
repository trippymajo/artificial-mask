using UnityEngine;
using UnityEngine.SceneManagement;

public class OutfitPoints : MonoBehaviour
{
    [Header("Fallback if GameState is missing")]
    [SerializeField] private LevelId fallbackLevel = LevelId.MysticCult;

    [Header("Character slots")]
    [SerializeField] private AppearanceCycler top;
    [SerializeField] private AppearanceCycler clothes;
    [SerializeField] private AppearanceCycler boots;

    [Header("Scoring")]
    [SerializeField] private int pointsPerMatch = 1;

    public void CalculateScoreAndLoadScene()
    {
        LevelId currentLevel =
            LevelManager.Instance != null ? LevelManager.Instance.SelectedLevel : fallbackLevel;

        int score = 0;

        // Counting stuff
        score += ScoreItem(top != null ? top.CurrentItem : null, currentLevel);
        score += ScoreItem(clothes != null ? clothes.CurrentItem : null, currentLevel);
        score += ScoreItem(boots != null ? boots.CurrentItem : null, currentLevel);

        // Lets do debug lol
        Debug.Log("Score: " + score);

        if (LevelManager.Instance != null) LevelManager.Instance.SetScore(score);

        string nextSceneName = GetResultSceneName(currentLevel);
        SceneManager.LoadScene(nextSceneName);
    }

    // Help func to count stuff
    private int ScoreItem(ClothingItemDef item, LevelId currentLevel)
    {
        if (item == null) return 0;
        if (!item.givesPoints) return 0;
        if (item.levelTag != currentLevel) return 0;

        return pointsPerMatch;
    }

    // Shit code btw. Don't like idea of many scenes
    private string GetResultSceneName(LevelId level)
    {
        return level switch
        {
            // Daniel, Do this!!!
            LevelId.MysticCult => "Results_Mystic",
            LevelId.NeonLights => "Results_Neon",
            LevelId.TheTest => "Results_Satanic",
            _ => "Results"
        };
    }
}
