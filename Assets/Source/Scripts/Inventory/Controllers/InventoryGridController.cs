using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Inventory
{
    public class InventoryGridController
    {
        private readonly List<InventorySlotController> _slotController = new();

        public InventoryGridController(IReadOnlyInventoryGrid inventory, InventoryView view)
        {
            var size = inventory.Size;
            var slots = inventory.GetSlots();
            var linelenght = size.y;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    int index = i * size.y + j;
                    var slotView = view.GetSlotView(index);
                    var slot = slots[i, j];
                    _slotController.Add(new InventorySlotController(slot, slotView));
                }
            }

            view.OwnerId = inventory.OwnerId;
        }
    }
}