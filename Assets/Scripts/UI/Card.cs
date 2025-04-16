using System;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private Image _cardProfile;
    private Image _weaponProfile;

    private TextMeshProUGUI _nameText;
    private TextMeshProUGUI _descText;
    private UpgradeData _upgradeData;

    public Action onClickWeaponCard;
    public Action onClickStatusCard;

    private void Awake()
    {
        _cardProfile = GetComponent<Image>();
        _weaponProfile = transform.Find("Profile").GetComponent<Image>();

        _nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        _descText = transform.Find("Description").GetComponent<TextMeshProUGUI>();

        GetComponent<Button>().onClick.AddListener(() => 
        {
            onClickWeaponCard?.Invoke();
        });

        GetComponent<Button>().onClick.AddListener(() =>
        {
            onClickStatusCard?.Invoke();
        });
    }
    public void SetStatusCardData(int key)
    {
        string path = "Card/StatusCard";

        _upgradeData = DataManager.Instance.GetUpgradeData(key);

        Sprite sprite = Resources.Load<Sprite>(path);

        _cardProfile.sprite = sprite;

        _nameText.text = _upgradeData.Name; 
        _descText.text = $"{_upgradeData.DescPrefix} {(_upgradeData.Value * 100):0.#}% {_upgradeData.DescSuffix}";
    }
    public void SetSkillCardData(int key)
    {
        string path = "Card/SkillCard";

        _upgradeData = DataManager.Instance.GetUpgradeData(key);

        Sprite sprite = Resources.Load<Sprite>(path);

        _cardProfile.sprite = sprite;

        _nameText.text = _upgradeData.Name;
        _descText.text = $"{_upgradeData.DescPrefix} {(_upgradeData.Value * 100):0.#}% {_upgradeData.DescSuffix}";
    }

    public void SetWeaponCardData(int key)
    {
        string path = "Card/WeaponCard";

        _upgradeData = DataManager.Instance.GetUpgradeData(key);

        Sprite sprite = Resources.Load<Sprite>(path);

        _cardProfile.sprite = sprite;

        string weaponPath = "Card/" + _upgradeData.Type;
        sprite = Resources.Load<Sprite>(weaponPath);
        _weaponProfile.sprite = sprite;

        _nameText.text = _upgradeData.Name;
        _descText.text = $"{_upgradeData.DescPrefix} {_upgradeData.DescSuffix}";
    }
}
