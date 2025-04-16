using TMPro;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    private TextMeshPro _playerGUIText;

    private void Awake()
    {
        _playerGUIText = GetComponent<TextMeshPro>();
    }

    public void SetPlayerStatus(CharacterData characterData)
    {
    //    _playerGUIText.text =
    //        $"<color=red>HP: {characterData.MaxHp}</color> {characterData.MaxHp} \n" + 
    //        $"<color=green>ATK: {characterData.Attack}</color>\n" +
    //        $"<color=blue>DEF: {def}</color>";
    }
}
