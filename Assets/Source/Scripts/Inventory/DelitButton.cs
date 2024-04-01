using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

public class DelitButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Test Test;

    [SerializeField] private InventorySlotView _inventorySlotView;

    public void OnPointerClick(PointerEventData eventData)
    {
        Test.Set(_inventorySlotView);
    }
}