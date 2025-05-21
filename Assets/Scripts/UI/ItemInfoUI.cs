using TMPro;
using UnityEngine;

public class ItemInfoUI : MonoBehaviour
{
    public GameObject itemInfoUIPanel;
    public TextMeshProUGUI  itemInfoUIText;

    public void Show(string _info)
    {
        itemInfoUIPanel.SetActive(true);
        itemInfoUIText.text = _info;
    }

    public void Hide()
    {
        itemInfoUIPanel.SetActive(false);
    }
}
