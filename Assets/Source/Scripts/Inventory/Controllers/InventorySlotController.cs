using UnityEngine;

namespace Inventory
{
    public class InventorySlotController
    {
        private readonly InventorySlotView _view;

        public InventorySlotController(IReadOnlyInventorySlot slot, InventorySlotView view)
        {
            _view = view;

            slot.ItemIdChanged += OnSlotItemIdChanged;
            slot.ItemAmountChanged += OnSlotItemAmountChanged;
            slot.ItemSpriteChanged += OnSlotSpriteAmountChanged;

            view.Title = slot.ItemId;
            view.Amount = slot.Amount;
            view.Sprite = slot.Sprite;
        }

        private void OnSlotItemIdChanged(string newItemId)
        {
            _view.Title = newItemId;
        }

        private void OnSlotItemAmountChanged(int newAmount)
        {
            _view.Amount = newAmount;
        }

        private void OnSlotSpriteAmountChanged(Sprite newSprite)
        {
            _view.Sprite = newSprite;
        }
    }
}