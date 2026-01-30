using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AppearanceCycler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer target;

    [Header("Options (Colors)")]
    // Just for now withouth sprites
    [SerializeField] private Color[] options;

    private int index;

    private void Reset()
    {
        target = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (target == null)
        {
            target = GetComponent<SpriteRenderer>();
        }

        Apply();
    }

    public void Next()
    {
        if (options == null || options.Length == 0) return;

        // Avoid getting out of bounds
        index = (index + 1) % options.Length;

        Apply();
    }

    public void Prev()
    {
        if (options == null || options.Length == 0) return;

        // Avoid getting out of bounds
        index = (index - 1 + options.Length) % options.Length;

        Apply();
    }

    private void Apply()
    {
        if (target == null) return;
        // In future need to check sprite existence
        if (options == null || options.Length == 0) return;

        var c = options[index];
        target.color = c;
    }
}