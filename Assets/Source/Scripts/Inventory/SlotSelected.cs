using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotSelected : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RemoveItem _removeItem;
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private InventorySlotView _inventorySlotView;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_inventorySlotView.IsEmpty)
        {
            _inventoryView.AllUnSelected();
            _inventorySlotView.Selected();
            _removeItem.Set(_inventorySlotView);
        }
    }
}