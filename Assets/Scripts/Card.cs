using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private Image _profile;
    private TextMeshProUGUI _titleText;
    private TextMeshProUGUI _descText;
    private UpgradeData _upgradeData;

    private void Awake()
    {
        _profile = GetComponent<Image>();

        _titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        _descText = transform.Find("Description").GetComponent<TextMeshProUGUI>();
    }
    public void SetStatusCardData(int key)
    {
        string path = "Card/StatusCard";

        _upgradeData = DataManager.Instance.GetUpgradeData(key);

        Sprite sprite = Resources.Load<Sprite>(path);

        _profile.sprite = sprite;

        _titleText.text = _upgradeData.Name; 
        _descText.text = $"{_upgradeData.DescPrefix} {(_upgradeData.Value * 100):0.#}% {_upgradeData.DescSuffix}";
    }

    public void SetWeaponCardData(int key)
    {
        string path = "Card/SkillCard";

        _upgradeData = DataManager.Instance.GetUpgradeData(key);

        Sprite sprite = Resources.Load<Sprite>(path);

        _profile.sprite = sprite;

        _titleText.text = _upgradeData.Name;
        _descText.text = $"{_upgradeData.DescPrefix} {_upgradeData.DescSuffix}";
    }
}
