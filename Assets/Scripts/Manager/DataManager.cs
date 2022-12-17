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
    static int[] _album1StageProgressLine;
    static int[] _album2StageProgressLine;
    static int[] _album3StageProgressLine;
    static int[] _album4StageProgressLine;

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
        gameData.album1StageProgressLine = _album1StageProgressLine ?? globalUserData.gameData.album1StageProgressLine;
        gameData.album1StageProgressLine = _album2StageProgressLine ?? globalUserData.gameData.album2StageProgressLine;
        gameData.album1StageProgressLine = _album3StageProgressLine ?? globalUserData.gameData.album3StageProgressLine;
        gameData.album1StageProgressLine = _album4StageProgressLine ?? globalUserData.gameData.album4StageProgressLine;

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
            // ���� ���� Ȯ��
            _fileYn = true;

            // ���Ͽ��� ������ �ҷ���
            var jsonUserData = File.ReadAllText(_path + _fileName);
            GlobalState.Instance.UserData = JsonUtility.FromJson<JsonUserData>(jsonUserData);

            Debug.Log($"Load User Data : {jsonUserData}");

            // DataManager�� ���Ͽ��� ������ ������ �ֱ�
            // ���� ������ ����
            dataBGMValue = GlobalState.Instance.UserData.data.settingData.BGMValue;
            dataSFXValue = GlobalState.Instance.UserData.data.settingData.SFXValue;
            dataKeyDivision = GlobalState.Instance.UserData.data.settingData.keyDivision;
            dataInnerOperationKey = GlobalState.Instance.UserData.data.settingData.innerOperationKey;
            dataOuterOperationKey = GlobalState.Instance.UserData.data.settingData.outerOperationKey;

            // ���� ������ ����
            dataSkinUnLockYn = GlobalState.Instance.UserData.data.shopData.skillUnLockYn;
            dataSkinUsingYn = GlobalState.Instance.UserData.data.shopData.skinUsingYn;
            dataSkillUnLockYn = GlobalState.Instance.UserData.data.shopData.skillUnLockYn;
            dataSkillUsingYn = GlobalState.Instance.UserData.data.shopData.skillUsingYn;

            // �ΰ��� ���� ������
            dataClearStageCount = GlobalState.Instance.UserData.data.gameData.clearStageCount;
            dataAlbum1ClearYn = GlobalState.Instance.UserData.data.gameData.album1ClearYn;
            dataAlbum2ClearYn = GlobalState.Instance.UserData.data.gameData.album2ClearYn;
            dataAlbum3ClearYn = GlobalState.Instance.UserData.data.gameData.album3ClearYn;
            dataAlbum4ClearYn = GlobalState.Instance.UserData.data.gameData.album4ClearYn;
            dataAlbum1StageProgressLine = GlobalState.Instance.UserData.data.gameData.album1StageProgressLine;
            dataAlbum2StageProgressLine = GlobalState.Instance.UserData.data.gameData.album2StageProgressLine;
            dataAlbum3StageProgressLine = GlobalState.Instance.UserData.data.gameData.album3StageProgressLine;
            dataAlbum4StageProgressLine = GlobalState.Instance.UserData.data.gameData.album4StageProgressLine;

            // �۷ι� �� ȣ�� ��� ���Ͽ� ���� �� �����/ȯ���� �������� ����
            SoundManager.Instance.CtrlBGMVolume((float)dataBGMValue);
            SoundManager.Instance.CtrlSFXVolume((float)dataSFXValue);
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

            _clearStageCount = 0;
            _album1ClearYn = new string[_album1StageCount];
            _album2ClearYn = new string[_album2StageCount];
            _album3ClearYn = new string[_album3StageCount];
            _album4ClearYn = new string[_album4StageCount];
            _album1StageProgressLine = new int[_album1StageCount];
            _album2StageProgressLine = new int[_album2StageCount];
            _album3StageProgressLine = new int[_album3StageCount];
            _album4StageProgressLine = new int[_album4StageCount];

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

            for (int i = 0; i < _album1StageCount; i++)
            {
                _album1ClearYn[i] = "N";
                _album1StageProgressLine[i] = 0;
            }
            for (int i = 0; i < _album2StageCount; i++)
            {
                _album2ClearYn[i] = "N";
                _album2StageProgressLine[i] = 0;
            }
            for (int i = 0; i < _album3StageCount; i++)
            {
                _album3ClearYn[i] = "N";
                _album3StageProgressLine[i] = 0;
            }
            for (int i = 0; i < _album4StageCount; i++)
            {
                _album4ClearYn[i] = "N";
                _album4StageProgressLine[i] = 0;
            }

            userData.gameData.clearStageCount = 0;
            userData.gameData.album1ClearYn = _album1ClearYn;
            userData.gameData.album2ClearYn = _album2ClearYn;
            userData.gameData.album3ClearYn = _album3ClearYn;
            userData.gameData.album4ClearYn = _album4ClearYn;
            userData.gameData.album1StageProgressLine = _album1StageProgressLine;
            userData.gameData.album2StageProgressLine = _album2StageProgressLine;
            userData.gameData.album3StageProgressLine = _album3StageProgressLine;
            userData.gameData.album4StageProgressLine = _album4StageProgressLine;
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
    public static int[] dataAlbum1StageProgressLine
    {
        get { return _album1StageProgressLine; }
        set { _album1StageProgressLine = value; }
    }

    // �ٹ� 2�� �������� �� �����
    public static int[] dataAlbum2StageProgressLine
    {
        get { return _album2StageProgressLine; }
        set { _album2StageProgressLine = value; }
    }

    // �ٹ� 3�� �������� �� �����
    public static int[] dataAlbum3StageProgressLine
    {
        get { return _album3StageProgressLine; }
        set { _album3StageProgressLine = value; }
    }

    // �ٹ� 4�� �������� �� �����
    public static int[] dataAlbum4StageProgressLine
    {
        get { return _album4StageProgressLine; }
        set { _album4StageProgressLine = value; }
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
