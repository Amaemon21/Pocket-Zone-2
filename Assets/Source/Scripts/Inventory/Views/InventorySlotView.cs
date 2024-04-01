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

        public bool SelectedImage
        {
            get => _selectedImage.sprite;
            set
            {
                if (value == true)
                    _selectedImage.enabled = value;
                else
                    _selectedImage.enabled = value;
            }
        }

        public string Title
        {
            get => _textTitle.text;
            set => _textTitle.text = value;
        }

        public int Amount
        {
            get => Convert.ToInt32(_textAmount.text);
            set => _textAmount.text = value == 0 ? "" : value.ToString();
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
                }
            }

        }
    }
}