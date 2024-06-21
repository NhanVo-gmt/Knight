using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDescUI : MonoBehaviour
{
    [SerializeField] private Image           itemImg;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    public void UpdateUI(ItemData data)
    {
        itemImg.sprite = data.sprite;
        title.SetText(data.itemName);
        description.SetText(data.itemDescription);
    }
}
