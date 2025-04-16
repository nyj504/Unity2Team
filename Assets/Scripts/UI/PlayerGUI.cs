using TMPro;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    private TextMeshProUGUI _playerGUIText;

    private void Awake()
    {
        _playerGUIText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            UIManager.Instance.OpenChoiceUI();
        }

        if (GameManager.Instance.isGameStart)
            SetStatus();
    }

    public void SetStatus()
    {
        CharacterData guiData = GameManager.PlayerInstance.GetPlayerData;
        _playerGUIText.text =
             $"<color=red>HP:</color> {guiData.MaxHp} / {guiData.MaxHp}\n" +
             $"<color=yellow>ATK:</color> {guiData.AttackPower}\n" +
             $"<color=green>DEX:</color> {guiData.MoveSpeed}";
    }
}
