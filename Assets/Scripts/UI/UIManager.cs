using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private bool _isFirstChoice = true;
    private int _selectedWeaponID;
    private GameObject _choicePanel;

    private static UIManager _instance;
    public static UIManager Instance
        { get { return _instance; } }

    private List<Card> _cards = new List<Card>();

    private void Awake()
    {
        _instance = this;
        _choicePanel = transform.Find("ChoicePanel").gameObject;

        GetComponentsInChildren<Card>(_cards);
        _choicePanel.SetActive(false);
    }

    private void Start()
    {
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
          
                GameManager.Instance.SetPlayerWeapon();

                _choicePanel.SetActive(false);
            };
        }
    }
}
