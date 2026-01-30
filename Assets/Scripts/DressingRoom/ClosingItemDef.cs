using UnityEngine;

[CreateAssetMenu(menuName = "Game/Clothing Item")]
public class ClothingItemDef : ScriptableObject
{
    public LevelId levelTag;
    public Sprite sprite;
    public bool givesPoints = true;
}