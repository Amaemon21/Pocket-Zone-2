using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class EntryPoint : MonoBehaviour
    {
        private const string Owner = "Inventory";

        [SerializeField] private ScreenView _screenView;

        private ScreenController _screenController;

        public string CachedOwnerId { get; private set; }
        public InventoryService InventoryService { get; private set; }

        private void Awake()
        {
            InventoryService = new InventoryService();

            var inventoryData = CreateTestInventory(Owner);
            InventoryService.RegisterInventory(inventoryData);

            _screenController = new ScreenController(InventoryService, _screenView);
            _screenController.OpenInventory(Owner);
            CachedOwnerId = Owner;
        }

        private InventoryGridData CreateTestInventory(string ownerId)
        {
            var size = new Vector2Int(4, 3);
            var createdInventorySlots = new List<InventorySlotData>();
            var length = size.x * size.y;

            for (var i = 0; i < length; i++)
            {
                createdInventorySlots.Add(new InventorySlotData());
            }

            var createdInventoryData = new InventoryGridData
            {
                OwnerId = ownerId,
                Size = size,
                Slots = createdInventorySlots
            };

            return createdInventoryData;
        }
    }
}