using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public interface IUserData
{
    string ToJson(bool intented);
    void UpdateFromJson(string jsonData);
}

[Serializable]
public class DataBase : IUserData
{
    public virtual string ToJson(bool intented = false)
    {
        return JsonUtility.ToJson(this);
    }

    public virtual void UpdateFromJson(string jsonData)
    {
        JsonUtility.FromJsonOverwrite(jsonData, this);
    }
}

// ------------------------------------------------------------------------
[Serializable]
public enum BallType
{
    BlackBall,
    BlueBall,
    PeridotGreenBall,
    OrangeBall,
}

[Serializable]
public class UserData
{
    // ���� ���� �۷ι� ������
    // ���� ����
    public string version = string.Empty;
    // �� �÷��� Ÿ��
    public string totalPlayTime = string.Empty;
    // ù �÷��� Ÿ��
    public string firstPlayTime = string.Empty;
    // ������ �÷��� Ÿ��
    public string lastPlayTime = string.Empty;

    // ���� ���� �۷ι� ������
    public SettingData settingData = new SettingData();

    // ���� ���� �۷ι� ������
    public ShopData shopData = new ShopData();

    // ���� ���� �۷ι� ������
    public GameData gameData = new GameData();
}

[Serializable]
// ���� ���� �۷ι� ������
public class SettingData : DataBase
{
    // ����� ����
    public float BGMValue = new float();
    // ȯ���� ����
    public float SFXValue = new float();
    // Ű ���� ���� > Integration : ����, Separation : �и�
    public string keyDivision = string.Empty;
    // Ű ���� ���� > �и� > ���� �̵� Ű �迭
    public string[] innerOperationKey = new string[4];
    // Ű ���� ���� > �и� > �ٱ� �̵� Ű �迭
    public string[] outerOperationKey = new string[4];
}

[Serializable]
// ���� ���� �۷ι� ������
public class ShopData : DataBase
{
    // ��Ų �ر� ���� �迭
    public string[] skinUnLockYn = new string[DataManager.dataSkinCount];
    // ��Ų ��� ���� �迭
    public string[] skinUsingYn = new string[DataManager.dataSkinCount];
    // ��ų �ر� ���� �迭
    public string[] skillUnLockYn = new string[DataManager.dataSkillCount];
    // ��ų ��� ���� �迭
    public string[] skillUsingYn = new string[DataManager.dataSkillCount];
}

[Serializable]
// ���� ���� �۷ι� ������
public class GameData : DataBase
{
    // �������� Ŭ���� ��
    public int clearStageCount = new int();
    // �ٹ� �� �������� Ŭ���� ���� �迭
    public string[] album1ClearYn = new string[DataManager.dataAlbum1StageCount];
    public string[] album2ClearYn = new string[DataManager.dataAlbum2StageCount];
    public string[] album3ClearYn = new string[DataManager.dataAlbum3StageCount];
    public string[] album4ClearYn = new string[DataManager.dataAlbum4StageCount];
    // �ٹ� �� �������� ����� �迭
    public int[] album1ProgressRate = new int[DataManager.dataAlbum1StageCount];
    public int[] album2ProgressRate = new int[DataManager.dataAlbum2StageCount];
    public int[] album3ProgressRate = new int[DataManager.dataAlbum3StageCount];
    public int[] album4ProgressRate = new int[DataManager.dataAlbum4StageCount];
    // �ٹ� �� �������� Ŭ���� �� ���� �� �迭
    public int[] album1DeathCount = new int[DataManager.dataAlbum1StageCount];
    public int[] album2DeathCount = new int[DataManager.dataAlbum2StageCount];
    public int[] album3DeathCount = new int[DataManager.dataAlbum3StageCount];
    public int[] album4DeathCount = new int[DataManager.dataAlbum4StageCount];
    // �ٹ� �� �������� Ŭ���� �� ��� ������ ��� �迭
    public string[] ablum1UsingItem = new string[DataManager.dataAlbum1StageCount];
    public string[] ablum2UsingItem = new string[DataManager.dataAlbum2StageCount];
    public string[] ablum3UsingItem = new string[DataManager.dataAlbum3StageCount];
    public string[] ablum4UsingItem = new string[DataManager.dataAlbum4StageCount];
}

[Serializable]
public class JsonUserData : DataBase
{
    public UserData data = new UserData();
}

