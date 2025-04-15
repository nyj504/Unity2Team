using UnityEngine;
using System.Collections.Generic;

public struct CharacterData
{
   // Ã¤¿ì¸é µÊ 
}

public struct MonsterData
{
    // Ã¤¿ì¸é µÊ 
}

public struct WeaponData
{
    // Ã¤¿ì¸é µÊ 
}
public class DataManager
{
    private static DataManager _instance;

    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataManager();
            }

            return _instance;
        }
    }

    private Dictionary<int, CharacterData> _characterDatas = new Dictionary<int, CharacterData>();
    public Dictionary<int, CharacterData> CharacterDatas
    {
        get { return _characterDatas; }
    }

    private Dictionary<int, MonsterData> _monsterDatas = new Dictionary<int, MonsterData>();
    public Dictionary<int, MonsterData> MonsterDatas
    {
        get { return _monsterDatas; }
    }

    private Dictionary<int, WeaponData> _weaponDatas = new Dictionary<int, WeaponData>();
    public Dictionary<int, WeaponData> WeaponDatas
    {
        get { return _weaponDatas; }
    }


    public CharacterData GetCharacterData(int key) { return _characterDatas[key]; }
    public MonsterData GetMonsterData(int key) { return _monsterDatas[key]; }
    public WeaponData GetWeaponData(int key) { return _weaponDatas[key]; }

    public void LoadCharacterData()
    {
        //TextAsset textAsset = Resources.Load<TextAsset>("Tables/CatTable");

        //string text = textAsset.text;
        //
        //string[] rowData = text.Split("\r\n");
        //
        //for (int i = 1; i < rowData.Length; i++)
        //{
        //    if (rowData[i].Length == 0)
        //        break;
        //
        //    string[] datas = rowData[i].Split(",");
        //
        //    CharacterData data;
        //    data.Key = int.Parse(datas[0]);
        //    data.Name = datas[1];
        //    data.Attack = float.Parse(datas[2]);
        //    data.MaxHp = float.Parse(datas[3]);
        //    data.Speed = float.Parse(datas[4]);
        //    data.Range = float.Parse(datas[5]);
        //
        //    _characterDatas.Add(data.Key, data);
        //}
    }

    public void LoadMonsterData()
    {
        //TextAsset textAsset = Resources.Load<TextAsset>("Tables/CatTable");

        //string text = textAsset.text;
        //
        //string[] rowData = text.Split("\r\n");
        //
        //for (int i = 1; i < rowData.Length; i++)
        //{
        //    if (rowData[i].Length == 0)
        //        break;
        //
        //    string[] datas = rowData[i].Split(",");
        //
        //    CharacterData data;
        //    data.Key = int.Parse(datas[0]);
        //    data.Name = datas[1];
        //    data.Attack = float.Parse(datas[2]);
        //    data.MaxHp = float.Parse(datas[3]);
        //    data.Speed = float.Parse(datas[4]);
        //    data.Range = float.Parse(datas[5]);
        //
        //    _characterDatas.Add(data.Key, data);
        //}
    }

    public void LoadWeaponData()
    {
        //TextAsset textAsset = Resources.Load<TextAsset>("Tables/CatTable");

        //string text = textAsset.text;
        //
        //string[] rowData = text.Split("\r\n");
        //
        //for (int i = 1; i < rowData.Length; i++)
        //{
        //    if (rowData[i].Length == 0)
        //        break;
        //
        //    string[] datas = rowData[i].Split(",");
        //
        //    CharacterData data;
        //    data.Key = int.Parse(datas[0]);
        //    data.Name = datas[1];
        //    data.Attack = float.Parse(datas[2]);
        //    data.MaxHp = float.Parse(datas[3]);
        //    data.Speed = float.Parse(datas[4]);
        //    data.Range = float.Parse(datas[5]);
        //
        //    _characterDatas.Add(data.Key, data);
        //}
    }
}
