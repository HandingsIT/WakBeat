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
        var globalUserData = GlobalState.Instance.UserData.data;

        JsonUserData userData = new JsonUserData();

        var settingData = userData.data.settingData;
        var shopData = userData.data.shopData;
        var gameData = userData.data.gameData;

        // ���� ���� ���� ������
        userData.data.version = GlobalData.Instance.Information.Version;
        userData.data.totalPlayTime = (userData.data.totalPlayTime + GlobalState.Instance.PlayTime).ToString();
        userData.data.firstPlayTime = _fileYn ? globalUserData.firstPlayTime : DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss:tt"));
        userData.data.lastPlayTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss:tt"));

        // ���� Ȥ�� ������ ���� �� �۷ι� ������ ó��
        if (!_fileYn)
        {
            // ���� ���� ������
            settingData.BGMValue = _BGMValue ?? 0.5f;
            settingData.SFXValue = _SFXValue ?? 0.5f;
            settingData.keyDivision = _keyDivision ?? "Integration";

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
            shopData.skinUnLockYn = _skinUnLockYn;
            shopData.skinUsingYn = _skinUsingYn;
            shopData.skillUnLockYn = _skillUnLockYn;
            shopData.skillUsingYn = _skillUsingYn;
        }
        else
        {
            // ���� ���� ������
            settingData.BGMValue = _BGMValue ?? globalUserData.settingData.BGMValue;
            settingData.SFXValue = _SFXValue ?? globalUserData.settingData.SFXValue;
            settingData.keyDivision = _keyDivision ?? globalUserData.settingData.keyDivision;

            // ���� ���� ������
            shopData.skinUnLockYn = _skinUnLockYn ?? globalUserData.shopData.skinUnLockYn;
            shopData.skinUsingYn = _skinUsingYn ?? globalUserData.shopData.skinUsingYn;
            shopData.skillUnLockYn = _skillUnLockYn ?? globalUserData.shopData.skillUnLockYn;
            shopData.skillUsingYn = _skillUsingYn ?? globalUserData.shopData.skillUsingYn;
        }

        // ���� ���� ������
        settingData.innerOperationKey = _innerOperationKey ?? globalUserData.settingData.innerOperationKey;
        settingData.outerOperationKey = _outerOperationKey ?? globalUserData.settingData.outerOperationKey;

        // �ΰ��� ���� ������
        gameData.clearStageCount = _clearStageCount ?? globalUserData.gameData.clearStageCount;
        gameData.album1ClearYn = _album1ClearYn ?? globalUserData.gameData.album1ClearYn;
        gameData.album2ClearYn = _album2ClearYn ?? globalUserData.gameData.album2ClearYn;
        gameData.album3ClearYn = _album3ClearYn ?? globalUserData.gameData.album3ClearYn;
        gameData.album4ClearYn = _album4ClearYn ?? globalUserData.gameData.album4ClearYn;
        gameData.album1ProgressRate = _album1ProgressRate ?? globalUserData.gameData.album1ProgressRate;
        gameData.album2ProgressRate = _album2ProgressRate ?? globalUserData.gameData.album2ProgressRate;
        gameData.album3ProgressRate = _album3ProgressRate ?? globalUserData.gameData.album3ProgressRate;
        gameData.album4ProgressRate = _album4ProgressRate ?? globalUserData.gameData.album4ProgressRate;
        gameData.album1DeathCount = _album1DeathCount ?? globalUserData.gameData.album1DeathCount;
        gameData.album2DeathCount = _album2DeathCount ?? globalUserData.gameData.album2DeathCount;
        gameData.album3DeathCount = _album3DeathCount ?? globalUserData.gameData.album3DeathCount;
        gameData.album4DeathCount = _album4DeathCount ?? globalUserData.gameData.album4DeathCount;
        gameData.ablum1UsingItem = _ablum1UsingItem ?? globalUserData.gameData.ablum1UsingItem;
        gameData.ablum2UsingItem = _ablum2UsingItem ?? globalUserData.gameData.ablum2UsingItem;
        gameData.ablum3UsingItem = _ablum3UsingItem ?? globalUserData.gameData.ablum3UsingItem;
        gameData.ablum4UsingItem = _ablum4UsingItem ?? globalUserData.gameData.ablum4UsingItem;

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
        var userData = GlobalState.Instance.UserData.data;

        if (File.Exists(_path + _fileName))
        {
            // ���Ͽ��� ������ �ҷ���
            var jsonUserData = File.ReadAllText(_path + _fileName);
            GlobalState.Instance.UserData = JsonUtility.FromJson<JsonUserData>(jsonUserData);

            // �۷ι� �� ȣ�� ��� ���Ͽ� ���� �� �����/ȯ���� �������� ����
            SoundManager.Instance.CtrlBGMVolume(userData.settingData.BGMValue);
            SoundManager.Instance.CtrlSFXVolume(userData.settingData.SFXValue);

            // ���� ���� Ȯ��
            _fileYn = true;
            Debug.Log($"Load User Data : {jsonUserData}");
        } 
        else
        {
            // ���� ���� Ȯ��
            _fileYn = false;

            // ���� ���� �� �⺻ �� ����
            SoundManager.Instance.CtrlBGMVolume(0.5f);
            SoundManager.Instance.CtrlSFXVolume(0.5f);

            _skinUnLockYn = new string[_skinCount];
            _skinUsingYn = new string[_skinCount];
            _skillUnLockYn = new string[_skillCount];
            _skillUsingYn = new string[_skillCount];

            for (int i = 0; i < _skinCount; i++)
            {
                if (i == 0)
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

            for (int i = 0; i < _skillCount; i++)
            {
                _skillUnLockYn[i] = "N";
                _skillUsingYn[i] = "N";
            }

            userData.settingData.BGMValue = 0.5f;
            userData.settingData.SFXValue = 0.5f;
            userData.settingData.keyDivision = "Integration";

            userData.shopData.skinUnLockYn = _skinUnLockYn;
            userData.shopData.skinUsingYn = _skinUsingYn;
            userData.shopData.skillUnLockYn = _skillUnLockYn;
            userData.shopData.skillUsingYn = _skillUsingYn;
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
