using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemoveItem : MonoBehaviour
{
    [SerializeField] private EntryPoint _entryPoint;
    [SerializeField] private InventoryView _inventoryView;
    [SerializeField] private Slider _sliderDeliValue;
    [SerializeField] private TMP_Text _textValue;

    private InventorySlotView _cachedInventorySlotView;
    private int _delitValue;

    private void OnEnable()
    {
        _sliderDeliValue.value = 1;
        _textValue.text = 1.ToString();
    }

    public void OnValueChanged()
    {
        _delitValue = (int)_sliderDeliValue.value;
        _textValue.text = _delitValue.ToString();
    }

    public void Set(InventorySlotView cachedInventorySlotView)
    {
        _cachedInventorySlotView = cachedInventorySlotView;
        _sliderDeliValue.maxValue = _cachedInventorySlotView.Amount;
    }

    public void Destroy()
    {
        if (_cachedInventorySlotView == null)
            return;

        if (_cachedInventorySlotView.Title != "")
            _entryPoint.InventoryService.RemoveItems(_entryPoint.CachedOwnerId, _cachedInventorySlotView.Title, _cachedInventorySlotView.Amount, _delitValue);

        _cachedInventorySlotView = null;

        _inventoryView.AllUnSelected();
    }
}