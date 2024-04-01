using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Image image;

    public Sprite Sprite
    {
        get => image.sprite;
        set => image.sprite = value;    
    }
}
