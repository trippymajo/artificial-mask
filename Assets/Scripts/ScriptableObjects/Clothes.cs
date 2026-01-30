using UnityEngine;

public enum ClothesType
{
    Rich,
    Medium,
    Poor
}
[CreateAssetMenu(fileName = "Clothes", menuName = "Scriptable Objects/Clothes")]
public class Clothes : ScriptableObject
{
    public Sprite sprite;
    public ClothesType clothesType;
}
