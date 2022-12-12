using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectPause : MonoBehaviour
{
    public Button ButtonBack;
    public Button ButtonRestart;
    public Button ButtonHome;

    [SerializeField] private GameObject _PopupMusicInfo;
    [SerializeField] private UIElementPopUp UIElementPopUp;

    private Boolean _isDestroy = false;

    // �˾�â ȣ�� �� UI ����
    private float _duration = 0.15f;

    // ��ư �̺�Ʈ ����
    public void SetButtonEvent()
    {
        ButtonBack.onClick.AddListener(() => { SetButtonClickEvent("Back"); });
        ButtonRestart.onClick.AddListener(() => { SetButtonClickEvent("Restart"); });
        ButtonHome.onClick.AddListener(() => { SetButtonClickEvent("Home"); });
    }

    // ��ư �̺�Ʈ ó��
    public void SetButtonClickEvent(string Division)
    {
        if (Division.Equals("Back")) 
        { 
            // �ڷ� ���� > ���� �̾� �ϱ� > 3�� �ʿ� ����
        }
        else if (Division.Equals("Restart"))
        {
            // ����� �ϱ� > ���� �ʱ�ȭ
            // **** �ʱ�ȭ ���μ��� �ۼ�

            // Music Info Restart
            Destroy(GameObject.Find(_PopupMusicInfo.name + "(Clone)"));

            // **** ���� ���� : ���� Ŭ�� �� Destroy�� ���� �ȵ�
            //_isDestroy = true;
            //UIElementPopUp.SetPopUpMusicInfo();
        }
        else if (Division.Equals("Home"))
        {
            // Ȩ���� �̵� (���� ���� ȭ������ �̵�) > �� ���൵ ���� �۷ι� ������ ����
            // **** Ȩ���� �̵� �� �۷ι� ������ ���� ���μ��� �ۼ�

            // Music Info Destroy
            Destroy(GameObject.Find(_PopupMusicInfo.name + "(Clone)"));
        }

        // ��ư �̺�Ʈ Unlock
        DataManager.dataBackgroundProcActive = true;

        // â �ݱ� ��ư �̺�Ʈ
        UIElementSetting.Instance.ButtonClickControll("Pause", "Close");

        // ���� �Ͻ� ���� ���μ��� > ����
        Stage.Instance.OnClickPause();
    }

    // �˾� â ȣ�� �� UI ��� ����
    private void OnEnable()
    {
        /*
        // 2022.12.12 > Stage�� OnClickPause ���� �� Animation �������� ���� Dotween �۵� ����
        // ������ 0���� ����
        this.transform.localScale = Vector3.zero;
        // 1���� Ŀ���鼭 �ð��� 0.2��, ��ȯ �� ť�� ���·� ����
        this.transform.DOScale(Vector3.one, _duration).SetEase(Ease.InCubic);
        */
    }

    void Start()
    {
        // ��ư �̺�Ʈ ����
        SetButtonEvent();
    }

    /*
    public void OnDestroy()
    {
        Debug.Log(">>>>>>>>>>>>>> OnDestroy : " + _isDestroy);
        if(_isDestroy)
        {
            UIElementPopUp.SetPopUpMusicInfo();
        }
    }
    */
}
