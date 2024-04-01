using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Data Items")]
public class DataItemsSO : ScriptableObject
{
    [field: SerializeField] public List<ItemSO> Items = new();
}   