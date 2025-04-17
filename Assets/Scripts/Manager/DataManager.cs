using UnityEngine;
using System.Collections.Generic;

public struct CharacterData
{
    public int Key;
    public int Level;
    public float AttackPower;
    public float MaxHp;
    public float MoveSpeed;
    public float AttackSpeed;
}

public struct WeaponData
{
    public int Key;
    public int Type;
    public string Name;
    public float PosX;
    public float PosY;
    public float PosZ;
    public float RotX;
    public float RotY;
    public float RotZ;
    public string PrefabPath;
}

public struct MonsterData
{
    // Ã¤¿ì¸é µÊ 
}

public struct SkillData
{
    public int Key;
    public string Name;
    public float AttackRatio;
    public string SkillImagePath;
}

public struct UpgradeData
{
    public int Key;
    public string Name;
    public string Type;
    public float Value;
    public string DescPrefix;
    public string DescSuffix;
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

    private Dictionary<int, WeaponData> _weaponDatas = new Dictionary<int, WeaponData>();
    public Dictionary<int, WeaponData> WeaponDatas
    {
        get { return _weaponDatas; }
    }

    private Dictionary<int, MonsterData> _monsterDatas = new Dictionary<int, MonsterData>();
    public Dictionary<int, MonsterData> MonsterDatas
    {
        get { return _monsterDatas; }
    }

    private Dictionary<int, SkillData> _skillDatas = new Dictionary<int, SkillData>();
    public Dictionary<int, SkillData> SkillDatas
    {
        get { return _skillDatas; }
    }

    private Dictionary<int, UpgradeData> _upgradeDatas = new Dictionary<int, UpgradeData>();
    public Dictionary<int, UpgradeData> UpgradeDatas
    {
        get { return _upgradeDatas; }
    }



    public CharacterData GetCharacterData(int key) { return _characterDatas[key]; }
    public WeaponData GetWeaponData(int key) { return _weaponDatas[key]; }
    public MonsterData GetMonsterData(int key) { return _monsterDatas[key]; }
    public SkillData GetSkillData(int key) { return _skillDatas[key]; }
    public UpgradeData GetUpgradeData(int key) { return _upgradeDatas[key]; }

    public void LoadCharacterData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Tables/CharacterStatusTable");

        string text = textAsset.text;
        
        string[] rowData = text.Split("\r\n");
        
        for (int i = 1; i < rowData.Length; i++)
        {
            if (rowData[i].Length == 0)
                break;
        
            string[] datas = rowData[i].Split(",");
        
            CharacterData data;
            data.Key = int.Parse(datas[0]);
            data.Level = int.Parse(datas[1]);
            data.AttackPower = float.Parse(datas[2]);
            data.MaxHp = float.Parse(datas[3]);
            data.MoveSpeed = float.Parse(datas[4]);
            data.AttackSpeed = float.Parse(datas[5]);
        
            _characterDatas.Add(data.Key, data);
        }
    }
    public void LoadWeaponData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Tables/WeaponTable");

        string text = textAsset.text;

        string[] rowData = text.Split("\r\n");

        for (int i = 1; i < rowData.Length; i++)
        {
            if (rowData[i].Length == 0)
                break;

            string[] datas = rowData[i].Split(",");

            WeaponData data;
            data.Key = int.Parse(datas[0]);
            data.Type = int.Parse(datas[1]);
            data.Name = datas[2];
            data.PosX = float.Parse(datas[3]);
            data.PosY = float.Parse(datas[4]);
            data.PosZ = float.Parse(datas[5]);
            data.RotX = float.Parse(datas[6]);
            data.RotY = float.Parse(datas[7]);
            data.RotZ = float.Parse(datas[8]);
            data.PrefabPath = datas[9];

            _weaponDatas.Add(data.Key, data);
        }
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

    public void LoadSkillData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Tables/SkillTable");

        string text = textAsset.text;
        
        string[] rowData = text.Split("\r\n");
        
        for (int i = 1; i < rowData.Length; i++)
        {
            if (rowData[i].Length == 0)
                break;
        
            string[] datas = rowData[i].Split(",");
        
            SkillData data;
            data.Key = int.Parse(datas[0]);
            data.Name = datas[1];
            data.AttackRatio = float.Parse(datas[2]);
            data.SkillImagePath = datas[3];
        
            _skillDatas.Add(data.Key, data);
        }
    }

    public void LoadUpgradeData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Tables/UpgradeTable");

        string text = textAsset.text;
        
        string[] rowData = text.Split("\r\n");
        
        for (int i = 1; i < rowData.Length; i++)
        {
            if (rowData[i].Length == 0)
                break;
        
            string[] datas = rowData[i].Split(",");
        
            UpgradeData data;
            data.Key = int.Parse(datas[0]);
            data.Name = datas[1];
            data.Type = datas[2];
            data.Value = float.Parse(datas[3]);
            data.DescPrefix = datas[4];
            data.DescSuffix = datas[5];
        
            _upgradeDatas.Add(data.Key, data);
        }
    }
}
