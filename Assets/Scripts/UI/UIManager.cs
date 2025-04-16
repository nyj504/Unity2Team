using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;

public class UIManager : MonoBehaviour
{
    private bool _isFirstChoice = true;
    private int _selectedWeaponID;
    private int _waveIndex = 0;
  
    private GameObject _choicePanel;
    private GameObject _guiPanel;
    private GameObject _playerGUI;
    private TextMeshProUGUI _waveCount;

    private static UIManager _instance;
    public static UIManager Instance
        { get { return _instance; } }

    private List<Card> _cards = new List<Card>();

    private void Awake()
    {
        _instance = this;
        _choicePanel = transform.Find("ChoicePanel").gameObject;
        _guiPanel = transform.Find("PlayerGUI").gameObject;
        _waveCount = transform.Find("WaveCount").GetComponent<TextMeshProUGUI>();

        GetComponentInChildren<PlayerGUI>(_playerGUI);
        GetComponentsInChildren<Card>(_cards);
        _choicePanel.SetActive(false);
    }

    private void Start()
    {
    }

    private void Update()
    {
        ShowWaveCount();
    }
    public void OpenChoiceUI()
    {
        _choicePanel.SetActive(true);
      
        if (_isFirstChoice)
        {
            SetWeaponCard();
            _isFirstChoice = false;
        }
        else
        {
            SetStatusCard();
        }
    }

    public void ShowWaveCount()
    {
        _waveCount.text = $"<color=red>Wave:</color> {_waveIndex}";
    }

    private void SetStatusCard()
    {
        HashSet<int> usedKeys = new HashSet<int>();

        for (int i = 0; i < _cards.Count; i++)
        {
            int randKey;
            do
            {
                randKey = UnityEngine.Random.Range(1001, 1009); 
            }
            while (usedKeys.Contains(randKey));
           
            usedKeys.Add(randKey);
            _cards[i].SetStatusCardData(randKey);

            _cards[i].onClickStatusCard = () =>
            {
                GameManager.PlayerInstance.EnhancePlayerStatus(randKey);

                _choicePanel.SetActive(false);
                _waveIndex++;
            };
        }
    }

    private void SetSkillCard()
    {
        foreach (Card card in _cards)
        {
            card.SetSkillCardData(501);
        }
    }
    private void SetWeaponCard()
    {
        int[] weaponID = { 501, 502, 503 };

        for (int i = 0; i < _cards.Count; i++)
        {
            int id = weaponID[i];
            _cards[i].SetWeaponCardData(id);

            _cards[i].onClickWeaponCard = () =>
            {
                _selectedWeaponID = id;
          
                GameManager.Instance.SetPlayerWeapon(id);

                _choicePanel.SetActive(false);
                _waveIndex++;
            };
        }
    }
}
