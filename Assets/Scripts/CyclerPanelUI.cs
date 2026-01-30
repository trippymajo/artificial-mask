using UnityEngine;
using UnityEngine.UI;

public class CyclerPanelUI : MonoBehaviour
{
    [SerializeField] private Button leftButt;
    [SerializeField] private Button rightButt;

    [SerializeField] private AppearanceCycler targetCycler;

    private void Awake()
    {
        if (leftButt != null) leftButt.onClick.AddListener(OnPrev);
        if (rightButt != null) rightButt.onClick.AddListener(OnNext);
    }

    private void OnDestroy()
    {
        if (leftButt != null) leftButt.onClick.RemoveListener(OnPrev);
        if (rightButt != null) rightButt.onClick.RemoveListener(OnNext);
    }

    private void OnPrev()
    {
        if (targetCycler != null) targetCycler.Prev();
    }

    private void OnNext()
    {
        if (targetCycler != null) targetCycler.Next();
    }
}