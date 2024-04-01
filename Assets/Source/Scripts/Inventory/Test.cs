using Inventory;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private EntryPoint _entryPoint;
    [SerializeField] private InventorySlotView _cachedInventorySlotView;

    public void Set(InventorySlotView cachedInventorySlotView)
    {
        _cachedInventorySlotView = cachedInventorySlotView;
        _cachedInventorySlotView.SelectedImage = true;
    }

    public void Destroy()
    {
        if (_cachedInventorySlotView.Sprite == null)
        {
            return;
        }

        _entryPoint.InventoryService.RemoveItems(_entryPoint.CachedOwnerId, _cachedInventorySlotView.Title, _cachedInventorySlotView.Amount);
    }
}