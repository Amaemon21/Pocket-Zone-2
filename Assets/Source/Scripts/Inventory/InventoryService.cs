using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryService
    {
        private readonly Dictionary<string, InventoryGrid> _inventoriesMap = new();

        private DataItemsSO _dataItemsSO;

        public InventoryGrid RegisterInventory(InventoryGridData inventoryData)
        {
            InventoryGrid inventory = new InventoryGrid(inventoryData);
            _inventoriesMap[inventory.OwnerId] = inventory;
            return inventory;
        }

        public AddItemsToInventoryGridResult AddItemsToInventory(string ownerId, string itemId, Sprite sprite, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId]; 
            return inventory.AddItems(itemId, sprite, amount);
        }

        public AddItemsToInventoryGridResult AddItemsToInventory(string ownerId, Vector2Int slotCords, string itemId, Sprite sprite, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.AddItems(itemId, sprite, amount);
        }

        public RemoveItemsToInventoryGridResult RemoveItems(string ownerId, string itemId, int currentAmount, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.RemoveItems(itemId, currentAmount, amount);
        }

        public RemoveItemsToInventoryGridResult RemoveItems(string ownerId, Vector2Int slotCords, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.RemoveItems(itemId, amount);
        }

        public bool Has(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.Has(itemId, amount);
        }

        public IReadOnlyInventoryGrid GetInventory(string ownerId)
        {
            return _inventoriesMap[ownerId];
        }
    }
}