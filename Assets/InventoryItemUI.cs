using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI idText;

    public void SetData(Sprite sprite, string id)
{
    if (icon == null)
        Debug.LogError("InventoryItemUI → 'icon' NON assegnato!");
    if (idText == null)
        Debug.LogError("InventoryItemUI → 'idText' NON assegnato!");

    icon.sprite = sprite;
    idText.text = id;
}
}