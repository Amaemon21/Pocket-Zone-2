using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private Image _selectedImage;
        [SerializeField] private TMP_Text _textTitle;
        [SerializeField] private TMP_Text _textAmount;
        [SerializeField] private Image _icon;

        public bool IsEmpty { get; private set; } = true;

        public string Title
        {
            get => _textTitle.text;
            set => _textTitle.text = value;
        }

        public int Amount
        {
            get => Convert.ToInt32(_textAmount.text);
            set
            {
                if (value == 0)
                {
                    _textAmount.text = "";
                    IsEmpty = true;
                }
                else
                {
                    _textAmount.text = value.ToString();
                    IsEmpty = false; 
                }
            }
        }

        public Sprite Sprite
        {
            get => _icon.sprite;
            set
            {
                if (value == null)
                {
                    _icon.enabled = false;
                }
                else
                {
                    _icon.enabled = true;
                    _icon.sprite = value;
                    IsEmpty = false;
                }
            }

        }

        public void Selected()
        {   
            _selectedImage.enabled = true;
        }

        public void UnSelected()
        {
            _selectedImage.enabled = false;
        }
    }
}