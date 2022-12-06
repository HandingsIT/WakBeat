using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening.Plugins;

public class DataManager : MonoBehaviourSingleton<DataManager>
{
    // �۷ι� ������ ���� (�Һ�)
    // ���� ���� ������
    // ���� ���
    static string _path;
    // ���� ��
    static string _fileName = "save";
    // ���� ����
    static Boolean _fileYn = false;
    // ���� ���� ������
    // ��Ų ����
    static int _skinCount = 4;
    // ��ų ����
    static int _skillCount = 5;
    // ��Ų �ر� ���� �迭
    static int[] _skinUnLockCondition = { 1, 2, 3, 5, 7 };
    // ��ų �ر� ���� �迭
    static int[] _skillUnLockCondition = { 1, 2, 3, 5, 7 };
    // �ٹ� �� �������� ��
    static int _album1StageCount = 2;
    static int _album2StageCount = 4;
    static int _album3StageCount = 5;
    static int _album4StageCount = 4;
    // ���� ���� �ر� ��� ���� > True : ���� �ر� ��� / False : ���� �ر� �̻��
    static Boolean _shopCompulsionActive = true;
    // �ΰ��� ���� ������
    // ��� ���μ��� ���� ���� > True : ���� / False : ������(�˾� â ��)
    static Boolean _backgroundProcActive = true;

    // �۷ι� ������ ���� (����)
    // ���� ���� ������
    // ����� ����
    static float? _BGMValue;
    // ȯ���� ����
    static float? _SFXValue;
    // Ű ���� �� > Integration : ����, Separation : �и�
    static string _keyDivision;
    // Ű ���� > �и� > ���� �̵� Ű �迭
    static string[] _innerOperationKey = new string[4];
    // Ű ���� > �и� > �ٱ� �̵� Ű �迭
    static string[] _outerOperationKey = new string[4];
    // ���� ���� ������
    // ��Ų �ر� ���� > Y : �ر�, N : ���ر�
    static string[] _skinUnLockYn;
    // ��Ų ��� ���� > Y : ���, N : �̻��
    static string[] _skinUsingYn;
    // ��ų �ر� ���� > Y : �ر�, N : ���ر�
    static string[] _skillUnLockYn;
    // ��ų ��� ���� > Y : ���, N : �̻��
    static string[] _skillUsingYn;
    // �ΰ��� ���� ������
    // �������� Ŭ���� ��
    static int? _clearStageCount;
    // �ٹ� �� �������� Ŭ���� ����
    static string[] _album1ClearYn;
    static string[] _album2ClearYn;
    static string[] _album3ClearYn;
    static string[] _album4ClearYn;
    // �ٹ� �� �������� �����
    static int[] _album1ProgressRate;
    static int[] _album2ProgressRate;
    static int[] _album3ProgressRate;
    static int[] _album4ProgressRate;
    // �������� �� Ŭ���� �� ���� �� 
    static int[] _album1DeathCount;
    static int[] _album2DeathCount;
    static int[] _album3DeathCount;
    static int[] _album4DeathCount;
    // �������� �� Ŭ���� �� ��� ������ �迭
    static string[] _ablum1UsingItem;
    static string[] _ablum2UsingItem;
    static string[] _ablum3UsingItem;
    static string[] _ablum4UsingItem;

    public static string GetUserData()
    {
        JsonUserData userData = new JsonUserData();

        // ���� ���� ���� ������
        userData.data.version = GlobalData.Instance.Information.Version;
        userData.data.totalPlayTime = (userData.data.totalPlayTime + GlobalState.Instance.PlayTime).ToString();
        userData.data.firstPlayTime = _fileYn ? GlobalState.Instance.UserData.data.firstPlayTime : DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss:tt"));
        userData.data.lastPlayTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss:tt"));

        // ���� Ȥ�� ������ ���� �� �۷ι� ������ ó��
        if (!_fileYn)
        {
            // ���� ���� ������
            userData.data.settingData.BGMValue = _BGMValue ?? 0.5f;
            userData.data.settingData.SFXValue = _SFXValue ?? 0.5f;
            userData.data.settingData.keyDivision = _keyDivision ?? "Integration";

            // ���� ���� ������
            if (null == _skinUnLockYn || _skinUnLockYn.Length <= 0 || (Array.IndexOf(_skinUnLockYn, "N") < 0 && Array.IndexOf(_skinUnLockYn, "Y") < 0)
                || null == _skinUsingYn || _skinUsingYn.Length <= 0 || (Array.IndexOf(_skinUsingYn, "N") < 0 && Array.IndexOf(_skinUnLockYn, "Y") < 0))
            {
                _skinUnLockYn = new string[_skinCount];
                _skinUsingYn = new string[_skinCount];

                for (int i = 0; i < _skinCount; i++)
                {
                    if(i == 0)
                    {
                        _skinUnLockYn[i] = "Y";
                        _skinUsingYn[i] = "Y";
                    }
                    else
                    {
                        _skinUnLockYn[i] = "N";
                        _skinUsingYn[i] = "N";
                    }
                }
            }
            if(null == _skillUnLockYn || _skillUnLockYn.Length <= 0 || (Array.IndexOf(_skillUnLockYn, "N") < 0 && Array.IndexOf(_skillUnLockYn, "Y") < 0)
                || null == _skillUsingYn || _skillUsingYn.Length <= 0 || (Array.IndexOf(_skillUsingYn, "N") < 0 && Array.IndexOf(_skillUsingYn, "Y") < 0))
            {
                _skillUnLockYn = new string[_skillCount];
                _skillUsingYn = new string[_skillCount];

                for (int i = 0; i < _skillCount; i++)
                {
                    _skillUnLockYn[i] = "N";
                    _skillUsingYn[i] = "N";
                }
            }
            userData.data.shopData.skinUnLockYn = _skinUnLockYn;
            userData.data.shopData.skinUsingYn = _skinUsingYn;
            userData.data.shopData.skillUnLockYn = _skillUnLockYn;
            userData.data.shopData.skillUsingYn = _skillUsingYn;
        }
        else
        {
            // ���� ���� ������
            userData.data.settingData.BGMValue = _BGMValue ?? GlobalState.Instance.UserData.data.settingData.BGMValue;
            userData.data.settingData.SFXValue = _SFXValue ?? GlobalState.Instance.UserData.data.settingData.SFXValue;
            userData.data.settingData.keyDivision = _keyDivision ?? GlobalState.Instance.UserData.data.settingData.keyDivision;

            // ���� ���� ������
            userData.data.shopData.skinUnLockYn = _skinUnLockYn ?? GlobalState.Instance.UserData.data.shopData.skinUnLockYn;
            userData.data.shopData.skinUsingYn = _skinUsingYn ?? GlobalState.Instance.UserData.data.shopData.skinUsingYn;
            userData.data.shopData.skillUnLockYn = _skillUnLockYn ?? GlobalState.Instance.UserData.data.shopData.skillUnLockYn;
            userData.data.shopData.skillUsingYn = _skillUsingYn ?? GlobalState.Instance.UserData.data.shopData.skillUsingYn;
        }

        // ���� ���� ������
        userData.data.settingData.innerOperationKey = _innerOperationKey ?? GlobalState.Instance.UserData.data.settingData.innerOperationKey;
        userData.data.settingData.outerOperationKey = _outerOperationKey ?? GlobalState.Instance.UserData.data.settingData.outerOperationKey;

        // �ΰ��� ���� ������
        userData.data.gameData.clearStageCount = _clearStageCount ?? GlobalState.Instance.UserData.data.gameData.clearStageCount;
        userData.data.gameData.album1ClearYn = _album1ClearYn ?? GlobalState.Instance.UserData.data.gameData.album1ClearYn;
        userData.data.gameData.album2ClearYn = _album2ClearYn ?? GlobalState.Instance.UserData.data.gameData.album2ClearYn;
        userData.data.gameData.album3ClearYn = _album3ClearYn ?? GlobalState.Instance.UserData.data.gameData.album3ClearYn;
        userData.data.gameData.album4ClearYn = _album4ClearYn ?? GlobalState.Instance.UserData.data.gameData.album4ClearYn;
        userData.data.gameData.album1ProgressRate = _album1ProgressRate ?? GlobalState.Instance.UserData.data.gameData.album1ProgressRate;
        userData.data.gameData.album2ProgressRate = _album2ProgressRate ?? GlobalState.Instance.UserData.data.gameData.album2ProgressRate;
        userData.data.gameData.album3ProgressRate = _album3ProgressRate ?? GlobalState.Instance.UserData.data.gameData.album3ProgressRate;
        userData.data.gameData.album4ProgressRate = _album4ProgressRate ?? GlobalState.Instance.UserData.data.gameData.album4ProgressRate;
        userData.data.gameData.album1DeathCount = _album1DeathCount ?? GlobalState.Instance.UserData.data.gameData.album1DeathCount;
        userData.data.gameData.album2DeathCount = _album2DeathCount ?? GlobalState.Instance.UserData.data.gameData.album2DeathCount;
        userData.data.gameData.album3DeathCount = _album3DeathCount ?? GlobalState.Instance.UserData.data.gameData.album3DeathCount;
        userData.data.gameData.album4DeathCount = _album4DeathCount ?? GlobalState.Instance.UserData.data.gameData.album4DeathCount;
        userData.data.gameData.ablum1UsingItem = _ablum1UsingItem ?? GlobalState.Instance.UserData.data.gameData.ablum1UsingItem;
        userData.data.gameData.ablum2UsingItem = _ablum2UsingItem ?? GlobalState.Instance.UserData.data.gameData.ablum2UsingItem;
        userData.data.gameData.ablum3UsingItem = _ablum3UsingItem ?? GlobalState.Instance.UserData.data.gameData.ablum3UsingItem;
        userData.data.gameData.ablum4UsingItem = _ablum4UsingItem ?? GlobalState.Instance.UserData.data.gameData.ablum4UsingItem;

        return userData.ToJson();
    }

    public static void SaveUserData()
    {
        // Json ������ ������
        var userData = GetUserData();

        // ���Ͽ� ������ �ۼ��Ͽ� ����
        File.WriteAllText(_path + _fileName, userData);
    }

    public void LoadUserData()
    {
        if (File.Exists(_path + _fileName))
        {
            // ���Ͽ��� ������ �ҷ���
            var userData = File.ReadAllText(_path + _fileName);
            GlobalState.Instance.UserData = JsonUtility.FromJson<JsonUserData>(userData);

            // �۷ι� �� ȣ�� ��� ���Ͽ� ���� �� �����/ȯ���� �������� ����
            SoundManager.Instance.CtrlBGMVolume(GlobalState.Instance.UserData.data.settingData.BGMValue);
            SoundManager.Instance.CtrlSFXVolume(GlobalState.Instance.UserData.data.settingData.SFXValue);

            // ���� ���� Ȯ��
            _fileYn = true;
            Debug.Log($"Load User Data : {userData}");
        } 
        else
        {
            // ���� ���� Ȯ��
            _fileYn = false;

            // ���� ���� ��
            SoundManager.Instance.CtrlBGMVolume(0.5f);
            SoundManager.Instance.CtrlSFXVolume(0.5f);
        }
    }

    private void Awake()
    {
        _path = Application.dataPath + "/StreamingAssets/";
        LoadUserData();
    }

    // ���� ���� ����
    public static Boolean dataFileYn
    {
        get { return _fileYn; }
        set { _fileYn = value; }
    }

    // ���� ���� �ر� ��� ����
    public static Boolean dataShopCompulsionActive
    {
        get { return _shopCompulsionActive; }
        set { _shopCompulsionActive = value; }
    }

    // ��� ���μ��� ���� ���� ����
    public static Boolean dataBackgroundProcActive
    {
        get { return _backgroundProcActive; }
        set { _backgroundProcActive = value; }
    }

    // Ŭ���� �������� ��
    public static int? dataClearStageCount
    {
        get { return _clearStageCount; }
        set { _clearStageCount = value; }
    }

    // ���� > Ű ���� ���� �� ����
    public static string dataKeyDivision
    {
        get { return _keyDivision; }
        set { _keyDivision = value; }
    }

    // ���� > ����� ũ�� �� ����
    public static float? dataBGMValue
    {
        get { return _BGMValue; }
        set { _BGMValue = value; }
    }

    // ���� > ȯ���� ũ�� �� ����
    public static float? dataSFXValue
    {
        get { return _SFXValue; }
        set { _SFXValue = value; }
    }

    // ���� > Ű ���� > �и� > ���� �̵� �� �迭 ����
    public static string[] dataInnerOperationKey
    {
        get { return _innerOperationKey; }
        set { _innerOperationKey = value; }
    }

    // ���� > Ű ���� > �и� > �ٱ� �̵� �� �迭 ����
    public static string[] dataOuterOperationKey
    {
        get { return _outerOperationKey; }
        set { _outerOperationKey = value; }
    }

    // ���� ��Ų ����
    public static int dataSkinCount
    {
        get { return _skinCount; }
        set { _skinCount = value;  }
    }

    // ���� ��ų ����
    public static int dataSkillCount
    {
        get { return _skillCount; }
        set { _skillCount = value;  }
    }

    // ���� ��ų ��� ����
    public static string[] dataSkillUsingYn
    {
        get { return _skillUsingYn; }
        set { _skillUsingYn = value; }
    }

    // ���� ��Ų �ر� ����
    public static string[] dataSkinUnLockYn
    {
        get { return _skinUnLockYn; }
        set { _skinUnLockYn = value; }
    }

    // ���� ��Ų ��� ����
    public static string[] dataSkinUsingYn
    {
        get { return _skinUsingYn; }
        set { _skinUsingYn = value; }
    }

    // ���� ��ų �ر� ���
    public static int[] dataSkinUnLockCondition
    {
        get { return _skinUnLockCondition; }
        set { _skinUnLockCondition = value; }
    }

    // ���� ��ų �ر� ����
    public static string[]dataSkillUnLockYn
    {
        get { return _skillUnLockYn; }
        set { _skillUnLockYn = value; }
    }

    // ���� ��ų �ر� ���
    public static int[] dataSkillUnLockCondition
    {
        get { return _skillUnLockCondition; }
        set { _skillUnLockCondition = value; }
    }

    // �ٹ� 1 �������� ����
    public static int dataAlbum1StageCount
    {
        get { return _album1StageCount; }
        set { _album1StageCount = value; }
    }

    // �ٹ� 2 �������� ����
    public static int dataAlbum2StageCount
    {
        get { return _album2StageCount; }
        set { _album2StageCount = value; }
    }

    // �ٹ� 3 �������� ����
    public static int dataAlbum3StageCount
    {
        get { return _album3StageCount; }
        set { _album3StageCount = value; }
    }

    // �ٹ� 4 �������� ����
    public static int dataAlbum4StageCount
    {
        get { return _album4StageCount; }
        set { _album4StageCount = value; }
    }

    // �ٹ� 1�� �������� �� Ŭ���� ����
    public static string[] dataAlbum1ClearYn
    {
        get { return _album1ClearYn; }
        set { _album1ClearYn = value; }
    }

    // �ٹ� 2�� �������� �� Ŭ���� ����
    public static string[] dataAlbum2ClearYn
    {
        get { return _album2ClearYn; }
        set { _album2ClearYn = value; }
    }

    // �ٹ� 3�� �������� �� Ŭ���� ����
    public static string[] dataAlbum3ClearYn
    {
        get { return _album3ClearYn; }
        set { _album3ClearYn = value; }
    }

    // �ٹ� 4�� �������� �� Ŭ���� ����
    public static string[] dataAlbum4ClearYn
    {
        get { return _album4ClearYn; }
        set { _album4ClearYn = value; }
    }

    // �ٹ� 1�� �������� �� �����
    public static int[] dataAlbum1ProgressRate
    {
        get { return _album1ProgressRate; }
        set { _album1ProgressRate = value; }
    }

    // �ٹ� 2�� �������� �� �����
    public static int[] dataAlbum2ProgressRate
    {
        get { return _album2ProgressRate; }
        set { _album2ProgressRate = value; }
    }

    // �ٹ� 3�� �������� �� �����
    public static int[] dataAlbum3ProgressRate
    {
        get { return _album3ProgressRate; }
        set { _album3ProgressRate = value; }
    }

    // �ٹ� 4�� �������� �� �����
    public static int[] dataAlbum4ProgressRate
    {
        get { return _album4ProgressRate; }
        set { _album4ProgressRate = value; }
    }

    // �������� 1�� Ŭ���� �� ���� ī��Ʈ
    public static int[] dataAlbum1DeathCount
    {
        get { return _album1DeathCount; }
        set { _album1DeathCount = value; }
    }

    // �������� 2�� Ŭ���� �� ���� ī��Ʈ
    public static int[] dataAlbum2DeathCount
    {
        get { return _album2DeathCount; }
        set { _album2DeathCount = value; }
    }

    // �������� 3�� Ŭ���� �� ���� ī��Ʈ
    public static int[] dataAlbum3DeathCount
    {
        get { return _album3DeathCount; }
        set { _album3DeathCount = value; }
    }

    // �������� 4�� Ŭ���� �� ���� ī��Ʈ
    public static int[] dataAlbum4DeathCount
    {
        get { return _album4DeathCount; }
        set { _album4DeathCount = value; }
    }

    // �������� 1�� Ŭ���� �� ��� ������ �迭
    public static string[] dataAblum1UsingItem
    {
        get { return _ablum1UsingItem; }
        set { _ablum1UsingItem = value; }
    }

    // �������� 2�� Ŭ���� �� ��� ������ �迭
    public static string[] dataAblum2UsingItem
    {
        get { return _ablum2UsingItem; }
        set { _ablum2UsingItem = value; }
    }

    // �������� 3�� Ŭ���� �� ��� ������ �迭
    public static string[] dataAblum3UsingItem
    {
        get { return _ablum3UsingItem; }
        set { _ablum3UsingItem = value; }
    }

    // �������� 4�� Ŭ���� �� ��� ������ �迭
    public static string[] dataAblum4UsingItem
    {
        get { return _ablum4UsingItem; }
        set { _ablum4UsingItem = value; }
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
