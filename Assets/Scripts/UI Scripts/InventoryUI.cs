using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text countText;

    public void UpdateInfo(Sprite itemIcon, int itemCount)
    {
        icon.sprite = itemIcon;
        countText.text = itemCount.ToString();
    }
}
