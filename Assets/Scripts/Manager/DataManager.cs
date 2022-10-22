using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEditorInternal;

public class DataManager : MonoBehaviourSingleton<DataManager>
{
    static string[] _InnerOperationKey;
    static string[] _OuterOperationKey;
    static string _KeyDivision;
    static float _BGMValue = 1f;
    static float _SFXValue = 1f;

    public static string GetUserData()
    {
        JsonUserData userData = new JsonUserData();

        userData.data.fileName = _fileName;
        userData.data.version = GlobalData.Instance.Information.Version;

        userData.data.playTime = GlobalState.Instance.PlayTime.ToString();
        userData.data.dateTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss:tt"));
        userData.data.coin = 0;

        userData.data.clearStageCount = 0;
        userData.data.statusAlbum_01 = 0;
        userData.data.statusAlbum_02 = 0;
        userData.data.statusAlbum_03 = 0;
        userData.data.statusAlbum_04 = 0;

        // ���� ȭ�� �۷ι� ������ ����
        userData.data.BGMValue = _BGMValue;
        userData.data.SFXValue = _SFXValue;
        userData.data.KeyDivision = (null == _KeyDivision || "".Equals(_KeyDivision)) ? "Integration" : _KeyDivision;
        userData.data.InnerOperationKey = _InnerOperationKey;
        userData.data.OuterOperationKey = _OuterOperationKey;

        return userData.ToJson();
    }


    static string path;
    static string _fileName = "save";

    public static void SaveUserData()
    {
        var userData = GetUserData();

        File.WriteAllText(path + _fileName, userData);
    }

    public void LoadUserData()
    {
        if (File.ReadAllText(path + _fileName) != null)
        {
            var userData = File.ReadAllText(path + _fileName);
            GlobalState.Instance.UserData = JsonUtility.FromJson<JsonUserData>(userData);

            // �۷ι� �� ȣ�� ��� ���Ͽ� ���� �� �����/ȯ���� �������� ����
            SoundManager.Instance.CtrlBGMVolume(GlobalState.Instance.UserData.data.BGMValue);
            SoundManager.Instance.CtrlSFXVolume(GlobalState.Instance.UserData.data.SFXValue);

            Debug.Log($"Load User Data : {userData}");
        }
    }

    private void Awake()
    {
        path = Application.dataPath + "/StreamingAssets/";
        LoadUserData();
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
