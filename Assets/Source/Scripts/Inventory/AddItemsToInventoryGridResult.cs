using UnityEngine;

namespace Inventory
{
    public readonly struct AddItemsToInventoryGridResult
    {
        public readonly string InventoryOwnerId;
        public readonly int ItemsToAddAmount;
        public readonly int ItemsAddedAmount;
        public readonly Sprite ItemsSprite;

        public AddItemsToInventoryGridResult(string inventoryOwnerId, int itemsToAddAmount, int itemsAddedAmount, Sprite itemsSprite)
        {
            InventoryOwnerId = inventoryOwnerId;
            ItemsToAddAmount = itemsToAddAmount;
            ItemsAddedAmount = itemsAddedAmount;
            ItemsSprite = itemsSprite;
        }

        public int ItemsNotAddedAmount => ItemsToAddAmount - ItemsAddedAmount;
    }
}