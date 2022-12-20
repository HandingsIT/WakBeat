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
            settingData.BGMValue = DataManager.dataBGMValue ?? 0.5f;
            settingData.SFXValue = DataManager.dataSFXValue ?? 0.5f;
            settingData.keyDivision = DataManager.dataKeyDivision ?? "Integration";

            // ���� ���� ������
            if (null == DataManager.dataSkinUnLockYn || DataManager.dataSkinUnLockYn.Length <= 0 
                || (Array.IndexOf(DataManager.dataSkinUnLockYn, "N") < 0 && Array.IndexOf(DataManager.dataSkinUnLockYn, "Y") < 0)
                || null == DataManager.dataSkinUsingYn || DataManager.dataSkinUsingYn.Length <= 0 
                || (Array.IndexOf(DataManager.dataSkinUsingYn, "N") < 0 && Array.IndexOf(DataManager.dataSkinUnLockYn, "Y") < 0))
            {
                DataManager.dataSkinUnLockYn = new string[DataManager.dataSkinCount];
                DataManager.dataSkinUsingYn = new string[DataManager.dataSkinCount];

                for (int i = 0; i < DataManager.dataSkinCount; i++)
                {
                    if(i == 0)
                    {
                        DataManager.dataSkinUnLockYn[i] = "Y";
                        DataManager.dataSkinUsingYn[i] = "Y";
                    }
                    else
                    {
                        DataManager.dataSkinUnLockYn[i] = "N";
                        DataManager.dataSkinUsingYn[i] = "N";
                    }
                }
            }
            if(null == DataManager.dataSkillUnLockYn || DataManager.dataSkillUnLockYn.Length <= 0 
                || (Array.IndexOf(DataManager.dataSkillUnLockYn, "N") < 0 && Array.IndexOf(DataManager.dataSkillUnLockYn, "Y") < 0)
                || null == DataManager.dataSkillUsingYn || DataManager.dataSkillUsingYn.Length <= 0 
                || (Array.IndexOf(DataManager.dataSkillUsingYn, "N") < 0 && Array.IndexOf(DataManager.dataSkillUsingYn, "Y") < 0))
            {
                DataManager.dataSkillUnLockYn = new string[DataManager.dataSkillCount];
                DataManager.dataSkillUsingYn = new string[DataManager.dataSkillCount];

                for (int i = 0; i < dataSkillCount; i++)
                {
                    DataManager.dataSkillUnLockYn[i] = "N";
                    DataManager.dataSkillUsingYn[i] = "N";
                }
            }
            shopData.skinUnLockYn = DataManager.dataSkinUnLockYn;
            shopData.skinUsingYn = DataManager.dataSkinUsingYn;
            shopData.skillUnLockYn = DataManager.dataSkillUnLockYn;
            shopData.skillUsingYn = DataManager.dataSkillUsingYn;
        }
        else
        {
            // ���� ���� ������
            settingData.BGMValue = (float)DataManager.dataBGMValue;
            settingData.SFXValue = (float)DataManager.dataSFXValue;
            settingData.keyDivision = DataManager.dataKeyDivision;

            // ���� ���� ������
            shopData.skinUnLockYn = DataManager.dataSkinUnLockYn;
            shopData.skinUsingYn = DataManager.dataSkinUsingYn;
            shopData.skillUnLockYn = DataManager.dataSkillUnLockYn;
            shopData.skillUsingYn = DataManager.dataSkillUsingYn;
        }

        // ���� ���� ������
        settingData.innerOperationKey = DataManager.dataInnerOperationKey;
        settingData.outerOperationKey = DataManager.dataOuterOperationKey;

        // �ΰ��� ���� ������
        gameData.clearStageCount = (int)DataManager.dataClearStageCount;
        gameData.album1ClearYn = DataManager.dataAlbum1ClearYn;
        gameData.album2ClearYn = DataManager.dataAlbum2ClearYn;
        gameData.album3ClearYn = DataManager.dataAlbum3ClearYn;
        gameData.album4ClearYn = DataManager.dataAlbum4ClearYn;
        gameData.album1StageProgressLine = DataManager.dataAlbum1StageProgressLine;
        gameData.album2StageProgressLine = DataManager.dataAlbum2StageProgressLine;
        gameData.album3StageProgressLine = DataManager.dataAlbum3StageProgressLine;
        gameData.album4StageProgressLine = DataManager.dataAlbum4StageProgressLine;

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
        } 
        else
        {
            // ���� ���� Ȯ��
            _fileYn = false;

            // ���� ���� �� �⺻ �� ����
            dataBGMValue = 0.5f;
            dataSFXValue = 0.5f;
            dataKeyDivision = "Integration";
            dataInnerOperationKey = new string[4];
            dataOuterOperationKey = new string[4];

            dataSkinUnLockYn = new string[dataSkinCount];
            dataSkinUsingYn = new string[dataSkinCount];
            dataSkillUnLockYn = new string[dataSkillCount];
            dataSkillUsingYn = new string[dataSkillCount];

            dataClearStageCount = 0;
            dataAlbum1ClearYn = new string[dataAlbum1StageCount];
            dataAlbum2ClearYn = new string[dataAlbum2StageCount];
            dataAlbum3ClearYn = new string[dataAlbum3StageCount];
            dataAlbum4ClearYn = new string[dataAlbum4StageCount];
            dataAlbum1StageProgressLine = new int[dataAlbum1StageCount];
            dataAlbum2StageProgressLine = new int[dataAlbum2StageCount];
            dataAlbum3StageProgressLine = new int[dataAlbum3StageCount];
            dataAlbum4StageProgressLine = new int[dataAlbum4StageCount];

            for (int i = 0; i < dataSkinCount; i++)
            {
                if (i == 0)
                {
                    dataSkinUnLockYn[i] = "Y";
                    dataSkinUsingYn[i] = "Y";
                }
                else
                {
                    dataSkinUnLockYn[i] = "N";
                    dataSkinUsingYn[i] = "N";
                }
            }

            for (int i = 0; i < dataSkillCount; i++)
            {
                dataSkillUnLockYn[i] = "N";
                dataSkillUsingYn[i] = "N";
            }

            userData.settingData.BGMValue = (float)dataBGMValue;
            userData.settingData.SFXValue = (float)dataSFXValue;
            userData.settingData.keyDivision = dataKeyDivision;

            userData.shopData.skinUnLockYn = dataSkinUnLockYn;
            userData.shopData.skinUsingYn = dataSkinUsingYn;
            userData.shopData.skillUnLockYn = dataSkillUnLockYn;
            userData.shopData.skillUsingYn = dataSkillUsingYn;

            for (int i = 0; i < dataAlbum1StageCount; i++)
            {
                dataAlbum1ClearYn[i] = "N";
                dataAlbum1StageProgressLine[i] = 0;
            }
            for (int i = 0; i < dataAlbum2StageCount; i++)
            {
                dataAlbum2ClearYn[i] = "N";
                dataAlbum2StageProgressLine[i] = 0;
            }
            for (int i = 0; i < dataAlbum3StageCount; i++)
            {
                dataAlbum3ClearYn[i] = "N";
                dataAlbum3StageProgressLine[i] = 0;
            }
            for (int i = 0; i < dataAlbum4StageCount; i++)
            {
                dataAlbum4ClearYn[i] = "N";
                dataAlbum4StageProgressLine[i] = 0;
            }

            userData.gameData.clearStageCount = (int)dataClearStageCount;
            userData.gameData.album1ClearYn = dataAlbum1ClearYn;
            userData.gameData.album2ClearYn = dataAlbum2ClearYn;
            userData.gameData.album3ClearYn = dataAlbum3ClearYn;
            userData.gameData.album4ClearYn = dataAlbum4ClearYn;
            userData.gameData.album1StageProgressLine = dataAlbum1StageProgressLine;
            userData.gameData.album2StageProgressLine = dataAlbum2StageProgressLine;
            userData.gameData.album3StageProgressLine = dataAlbum3StageProgressLine;
            userData.gameData.album4StageProgressLine = dataAlbum4StageProgressLine;
        }

        // �۷ι� �� ȣ�� ��� ���Ͽ� ���� �� �����/ȯ���� �������� ����
        SoundManager.Instance.CtrlBGMVolume((float)dataBGMValue);
        SoundManager.Instance.CtrlSFXVolume((float)dataSFXValue);
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
