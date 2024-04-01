using TMPro;
using UnityEngine;

namespace Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private InventorySlotView[] _slots;
        [SerializeField] private TMP_Text _textOwner;

        public string OwnerId
        {
            get => _textOwner.text;
            set => _textOwner.text = value;
        }

        public InventorySlotView GetSlotView(int index)
        {
            return _slots[index];
        }

        public void AllUnSelected()
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i].UnSelected();
            }
        }
    }
}