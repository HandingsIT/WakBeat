using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening.Plugins.Core.PathCore;
//using UnityEditorInternal;
using static UnityEngine.Rendering.DebugUI;
using UnityEditorInternal;

public class DataManager : MonoBehaviourSingleton<DataManager>
{
    // �۷ι� ������ ����
    static Boolean _FileYn;
    static int _ClearStageCount;

    // ���� ȭ�� ����
    static string[] _InnerOperationKey;
    static string[] _OuterOperationKey;
    static string _KeyDivision;
    static float? _BGMValue;
    static float? _SFXValue;
    // ���� ȭ�� ����
    static int _SkinCount = 4;
    static int _SkillCount = 5;
    static string[] _SkinUnLockYn;
    static string[] _SkinUsingYn;
    static int[] _SkinUnLockCondition;
    static string[] _SkillUnLockYn;
    static string[] _SkillUsingYn;
    static int[] _SkillUnLockCondition;
    static Boolean _ShopCompulsionActive;
    static Boolean _BackgroundProcActive;

    public static string GetUserData()
    {
        JsonUserData userData = new JsonUserData();

        userData.data.fileName = _fileName;
        userData.data.version = GlobalData.Instance.Information.Version;

        userData.data.playTime = GlobalState.Instance.PlayTime.ToString();
        userData.data.dateTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss:tt"));
        userData.data.coin = 0;

        userData.data.ClearStageCount = 0;
        userData.data.statusAlbum_01 = 0;
        userData.data.statusAlbum_02 = 0;
        userData.data.statusAlbum_03 = 0;
        userData.data.statusAlbum_04 = 0;

        // ���� ȭ�� �۷ι� ������ ����
        userData.data.BGMValue = _BGMValue ?? GlobalState.Instance.UserData.data.BGMValue;
        userData.data.SFXValue = _SFXValue ?? GlobalState.Instance.UserData.data.SFXValue;
        userData.data.KeyDivision = _KeyDivision ?? (GlobalState.Instance.UserData.data.KeyDivision ?? "Integration");
        userData.data.InnerOperationKey = _InnerOperationKey ?? GlobalState.Instance.UserData.data.InnerOperationKey;
        userData.data.OuterOperationKey = _OuterOperationKey ?? GlobalState.Instance.UserData.data.OuterOperationKey;

        // ���� ȭ�� �۷ι� ������ ����
        userData.data.SkinCount = _SkinCount;
        userData.data.SkillCount = _SkillCount;
        userData.data.SkinUnLockYn = _SkinUnLockYn ?? GlobalState.Instance.UserData.data.SkinUnLockYn;
        userData.data.SkinUsingYn = _SkinUsingYn ?? GlobalState.Instance.UserData.data.SkinUsingYn;
        userData.data.SkillUnLockYn = _SkillUnLockYn ?? GlobalState.Instance.UserData.data.SkillUnLockYn;
        userData.data.SkillUsingYn = _SkillUsingYn ?? GlobalState.Instance.UserData.data.SkillUsingYn;

        return userData.ToJson();
    }

    static string path;
    static string _fileName = "save";

    public static void SaveUserData()
    {
        // Json ������ ������
        var userData = GetUserData();

        // ���Ͽ� ������ �ۼ��Ͽ� ����
        File.WriteAllText(path + _fileName, userData);
    }

    public void LoadUserData()
    {
        if (File.Exists(path + _fileName))
        {
            // ���Ͽ��� ������ �ҷ���
            var userData = File.ReadAllText(path + _fileName);
            GlobalState.Instance.UserData = JsonUtility.FromJson<JsonUserData>(userData);

            // �۷ι� �� ȣ�� ��� ���Ͽ� ���� �� �����/ȯ���� �������� ����
            SoundManager.Instance.CtrlBGMVolume(GlobalState.Instance.UserData.data.BGMValue);
            SoundManager.Instance.CtrlSFXVolume(GlobalState.Instance.UserData.data.SFXValue);

            // ���� ���� Ȯ��
            _FileYn = true;
            GlobalState.Instance.UserData.data.FileYn = _FileYn;

            Debug.Log($"Load User Data : {userData}");
        } 
        else
        {
            // ���� ���� Ȯ��
            _FileYn = false;
            GlobalState.Instance.UserData.data.FileYn = _FileYn;

            // ���� ���� ��
            SoundManager.Instance.CtrlBGMVolume(0.5f);
            SoundManager.Instance.CtrlSFXVolume(0.5f);
        }
    }

    private void Awake()
    {
        path = Application.dataPath + "/StreamingAssets/";
        LoadUserData();
    }

    // ���� ��ȸ ����
    public static Boolean SetFileYn
    {
        get { return _FileYn; }
        set { _FileYn = value; }
    }

    // Ŭ���� �������� ��
    public static int SetClearStageCount
    {
        get { return _ClearStageCount; }
        set { _ClearStageCount = value; }
    }

    // ���� > Ű ���� ���� �� ����
    public static string SetKeyDivision
    {
        set { _KeyDivision = value; }
    }

    // ���� > ����� ũ�� �� ����
    public static float SetBGMValue
    {
        set { _BGMValue = value; }
    }

    // ���� > ȯ���� ũ�� �� ����
    public static float SetSFXValue
    {
        set { _SFXValue = value; }
    }

    // ���� > Ű ���� > �и� > ���� �̵� �� �迭 ����
    public static string[] SetInnerOperationKey
    {
        set { _InnerOperationKey = value; }
    }

    // ���� > Ű ���� > �и� > �ٱ� �̵� �� �迭 ����
    public static string[] SetOuterOperationKey
    {
        set { _OuterOperationKey = value; }
    }

    // ���� ��Ų ����
    public static int SetSkinCount
    {
        get { return _SkinCount; }
        set { _SkinCount = value;  }
    }

    // ���� ��ų ����
    public static int SetSkillCount
    {
        get { return _SkillCount; }
        set { _SkillCount = value;  }
    }

    // ���� ��ų ��� ����
    public static string[] SetSkillUsingYn
    {
        set { _SkillUsingYn = value; }
    }

    // ���� ��Ų �ر� ����
    public static string[] SetSkinUnLockYn
    {
        set { _SkinUnLockYn = value; }
    }

    // ���� ��Ų ��� ����
    public static string[] SetSkinUsingYn
    {
        set { _SkinUsingYn = value; }
    }

    // ���� ��ų �ر� ���
    public static int[] SetSkinUnLockCondition
    {
        set { _SkinUnLockCondition = value; }
    }

    // ���� ��ų �ر� ����
    public static string[] SetSkillUnLockYn
    {
        set { _SkillUnLockYn = value; }
    }

    // ���� ��ų ��� ����
    public static Boolean SetShopCompulsionActive
    {
        get { return _ShopCompulsionActive; }
        set { _ShopCompulsionActive = value; }
    }

    // ���� ��ų �ر� ���
    public static int[] SetSkillUnLockCondition
    {
        set { _SkillUnLockCondition = value; }
    }

    // ���� ��ų ��� ����
    public static Boolean SetBackgroundProcActive
    {
        get { return _BackgroundProcActive; }
        set { _BackgroundProcActive = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log($"JsonData = {DataManager.Instance.GetUserData()}");
        //SaveUserData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
