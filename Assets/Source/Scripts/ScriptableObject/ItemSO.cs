using UnityEngine;

[CreateAssetMenu(menuName = "Item/Item")]
public class ItemSO : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
}