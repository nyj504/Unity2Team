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
        SetSkillCard();
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
            card.SetWeaponCardData(501);
        }
    }
}
