using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIManager _instance;
    public UIManager Instance
        { get { return _instance; } }

    private List<Card> _cards = new List<Card>();

    private void Awake()
    {
        _instance = this;

        GetComponentsInChildren<Card>(_cards);
    }

    private void Start()
    {
        SetWeaponCard();
    }

    private void SetStatusCard()
    {
        foreach (Card card in _cards)
        {
            card.SetStatusCardData(1001);
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
            _cards[i].SetWeaponCardData(weaponID[i]);
        }
    }
}
