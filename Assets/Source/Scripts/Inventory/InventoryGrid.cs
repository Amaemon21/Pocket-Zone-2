using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Inventory
{
    public class InventoryGrid : IReadOnlyInventoryGrid
    {
        private readonly InventoryGridData _data;
        private readonly Dictionary<Vector2Int, InventorySlot> _slotsMap = new();

        public event Action<string, int> ItemsAdded;
        public event Action<string, int> ItemsRemoved;
        public event Action<Vector2Int> SizeChanged;

        public InventoryGrid(InventoryGridData data)
        {
            _data = data;

            Vector2Int size = data.Size;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    int index = i * size.y + j;
                    InventorySlotData slotData = data.Slots[index];
                    InventorySlot slot = new InventorySlot(slotData);
                    Vector2Int position = new Vector2Int(i, j);

                    _slotsMap[position] = slot;
                }
            }
        }

        public string OwnerId => _data.OwnerId;

        public Vector2Int Size
        {
            get => _data.Size;
            set
            {
                if (_data.Size != value)
                {
                    _data.Size = value;
                    SizeChanged!.Invoke(value);
                }
            }
        }

        public AddItemsToInventoryGridResult AddItems(string itemId, Sprite sprite, int amount = 1)
        {
            int remainigAmount = amount;
            int itemsAddedToSlotsWithSameItemsAmount = AddToSlotsWithSameItems(itemId, remainigAmount, sprite, out remainigAmount);

            if (remainigAmount <= 0)
            {
                return new AddItemsToInventoryGridResult(OwnerId, amount, itemsAddedToSlotsWithSameItemsAmount, sprite);
            }

            var itemsAddedToAvailableSlotsAmount = AddFirstAvailableSlots(itemId, remainigAmount, sprite, out remainigAmount);
            var totalAddedItemsAmount = itemsAddedToSlotsWithSameItemsAmount + itemsAddedToAvailableSlotsAmount;

            return new AddItemsToInventoryGridResult(OwnerId, amount, totalAddedItemsAmount, sprite);
        }

        public AddItemsToInventoryGridResult AddItems(Vector2Int slotCoords, string itemId, Sprite sprite, int amount = 1)
        {
            InventorySlot slot = _slotsMap[slotCoords];
            int newValue = slot.Amount + amount;
            int itemsAddedAmount = 0;

            if (slot.IsEmpty) slot.ItemId = itemId;

            int itemSlotCapacity = GetItemSlotCapacity(itemId);

            if (newValue > itemSlotCapacity)
            {
                int remainingItems = newValue - itemSlotCapacity;
                int itemsToAddAmount = itemSlotCapacity - slot.Amount;
                itemsAddedAmount += itemsToAddAmount;
                slot.Amount = itemSlotCapacity;
                slot.Sprite = sprite;

                AddItemsToInventoryGridResult result = AddItems(itemId, sprite, remainingItems);
                itemsAddedAmount += result.ItemsAddedAmount;
            }
            else
            {
                itemsAddedAmount = amount;
                slot.Amount = newValue;
                slot.Sprite = sprite;
            }

            return new AddItemsToInventoryGridResult(OwnerId, amount, itemsAddedAmount, sprite);
        }

        public RemoveItemsToInventoryGridResult RemoveItems(string itemId, int currentAmount, int removeAmount = 1)
        {
            if (!Has(itemId, removeAmount)) return new RemoveItemsToInventoryGridResult(OwnerId, removeAmount, false);

            int amountToRemove = removeAmount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    Vector2Int slotCoords = new Vector2Int(i, j);
                    InventorySlot slot = _slotsMap[slotCoords];

                    if (slot.Amount != currentAmount) continue;

                    if (slot.ItemId != itemId) continue;

                    if (amountToRemove > slot.Amount)
                    {
                        amountToRemove -= slot.Amount;

                        if (slot.Amount <= 0) slot.Sprite = null;

                        RemoveItems(slotCoords, itemId, slot.Amount);
                    }
                    else
                    {
                        if (slot.Amount <= 0) slot.Sprite = null;
    
                        RemoveItems(slotCoords, itemId, amountToRemove);

                        return new RemoveItemsToInventoryGridResult(OwnerId, removeAmount, true);
                    }
                }
            }

            throw new Exception("Something went wrong, couldn't remove some items");
        }

        public RemoveItemsToInventoryGridResult RemoveItems(Vector2Int slotCoords, string itemId, int amount = 1)
        {
            InventorySlot slot = _slotsMap[slotCoords];

            if (slot.IsEmpty || slot.ItemId != itemId || slot.Amount < amount)
            {
                return new RemoveItemsToInventoryGridResult(OwnerId, amount, false);
            }

            slot.Amount -= amount;

            if (slot.Amount <= 0)
            {
                slot.Sprite = null;
            }

            if (slot.Amount == 0)
            {
                slot.ItemId = null;
            }

            return new RemoveItemsToInventoryGridResult(OwnerId, amount, true);
        }

        public int GetAmount(string itemId)
        {
            var amount = 0;
            var slots = _data.Slots;

            foreach (var slot in slots)
            {
                if (slot.ItemId == itemId)
                {
                    amount += slot.Amount;
                }
            }

            return amount;
        }

        public bool Has(string itemId, int amount)
        {
            var amountExist = GetAmount(itemId);

            return amountExist >= amount;
        }

        public void SwitchSlots(Vector2Int slotCoordsA, Vector2Int slotCoordsB)
        {
            var slotA = _slotsMap[slotCoordsA];
            var slotB = _slotsMap[slotCoordsB];
            
            var tempSlotItemId = slotA.ItemId;
            var tempSlotItemAmount = slotA.Amount;

            slotA.ItemId = slotB.ItemId;
            slotA.Amount = slotB.Amount;
            slotB.ItemId = tempSlotItemId;
            slotB.Amount = tempSlotItemAmount;
        }

        public IReadOnlyInventorySlot[,] GetSlots()
        {
            var array = new IReadOnlyInventorySlot[Size.x, Size.y];

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var position = new Vector2Int(i, j);
                    array[i, j] = _slotsMap[position];
                }
            }

            return array;
        }

        private int GetItemSlotCapacity(string itemId)
        {
            return 99;
        }

        private int AddToSlotsWithSameItems(string itemId, int amount, Sprite sprite, out int remainingAmount)
        {
            int itemsAddedAmount = 0;
            remainingAmount = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    Vector2Int coords = new Vector2Int(i, j);
                    InventorySlot slot = _slotsMap[coords];

                    if (slot.IsEmpty)
                    {
                        continue;
                    }

                    int slotItemCapacity = GetItemSlotCapacity(slot.ItemId);    

                    if (slot.Amount >= slotItemCapacity)
                    {
                        continue;
                    }

                    if (slot.ItemId != itemId)
                    {
                        continue;
                    }

                    var newValue = slot.Amount + remainingAmount;

                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        int itemsToAddAmount = slotItemCapacity - slot.Amount;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;
                        slot.Sprite = sprite;

                        if (remainingAmount == 0)
                        {
                            return itemsAddedAmount;
                        }
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        slot.Sprite = sprite;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }

            return itemsAddedAmount;
        }

        private int AddFirstAvailableSlots(string itemId, int amount, Sprite sprite, out int remainingAmount)
        {
            int itemsAddedAmount = 0;
            remainingAmount = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _slotsMap[coords];

                    if (!slot.IsEmpty)
                    {
                        continue;
                    }

                    slot.ItemId = itemId;
                    int newValue = remainingAmount;
                    int slotItemCapacity = GetItemSlotCapacity(slot.ItemId);

                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        int itemsToAddAmount = slotItemCapacity;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;
                        slot.Sprite = sprite;
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        slot.Sprite = sprite;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }

            return itemsAddedAmount;
        }
    }
}