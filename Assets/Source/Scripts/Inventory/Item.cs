using Inventory;
using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField] public string ItemId { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}