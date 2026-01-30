using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AppearanceCycler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer target;

    [Header("Options (Items)")]
    [SerializeField] private ClothingItemDef[] options;

    [Header("Behavior")]
    [SerializeField] private bool hideWhenNone = true;

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

        var item = options[index];

        // pick needed sprite
        target.sprite = item != null ? item.sprite : null;

        // Make it visible
        target.color = Color.white;

        // Handle null object
        if (hideWhenNone)
            target.enabled = (item != null && item.sprite != null);
        else
            target.enabled = true;
    }
}