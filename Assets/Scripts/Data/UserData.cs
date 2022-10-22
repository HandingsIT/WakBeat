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
    public string fileName;
    public string version;
    public string playTime;
    public string dateTime;
    public int coin;
    //public string currentBall;
    //public string[] InnerOperationKey;
    //public string[] OuterOperationKey;
    public int clearStageCount;
    public int statusAlbum_01;
    public int statusAlbum_02;
    public int statusAlbum_03;
    public int statusAlbum_04;

    // ���� ���� �۷ι� ������
    // ����� ũ��
    public float BGMValue;
    // ȿ���� ũ��
    public float SFXValue;
    // Ű ���� ���� > Integration : ����, Separation : �и�
    public string KeyDivision;
    // Ű ���� ���� > �и� > ���� �̵� 
    public string[] InnerOperationKey = new string[4];
    // Ű ���� ���� > �и� > �ٱ� �̵�
    public string[] OuterOperationKey = new string[4];

    // ���� ���� �۷ι� ������
    // ��Ų ����
    public int SkinCount;
    // ��ų ����
    public int SkillCount;

    // ��Ų ���� ������
    // ���� �� ���� > Black, Blue, Green, Orange
    public string currentBall;
}

[Serializable]
public class JsonUserData : DataBase
{
    public UserData data = new UserData();
}

