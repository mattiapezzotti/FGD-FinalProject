using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI idText;

    public void SetData(Sprite sprite, string id)
    {
        icon.sprite = sprite;
        idText.text = id;
    }
}