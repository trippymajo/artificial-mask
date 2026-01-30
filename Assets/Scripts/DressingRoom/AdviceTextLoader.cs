using UnityEngine;
using UnityEngine.UI;

public class AdviceTextLoader : MonoBehaviour
{
    [System.Serializable]
    public struct LevelAdvice
    {
        public LevelId level;
        [TextArea(2, 8)]
        public string text;
    }

    [SerializeField] private Text adviceText;

    [Header("Advice mapping")]
    [SerializeField] private LevelAdvice[] advices;

    [TextArea(2, 8)]
    [SerializeField] private string fallback = "No advice.";

    private void Start()
    {
        if (adviceText == null)
        {
            Debug.LogError("AdviceTextLoader: adviceText is not assigned.");
            return;
        }

        LevelId level = LevelManager.Instance != null
            ? LevelManager.Instance.SelectedLevel
            : LevelId.MysticCult;

        adviceText.text = GetAdvice(level);
    }

    private string GetAdvice(LevelId level)
    {
        if (advices != null)
        {
            for (int i = 0; i < advices.Length; i++)
            {
                if (advices[i].level == level)
                    return string.IsNullOrEmpty(advices[i].text) ? fallback : advices[i].text;
            }
        }

        return fallback;
    }
}
