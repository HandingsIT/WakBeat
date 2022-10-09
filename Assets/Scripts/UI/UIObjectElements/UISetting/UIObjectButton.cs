using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectButton : MonoBehaviour
{
    // ��ư ����
    public Button btnHome;
    public Button btnStore;
    public Button btnSetting;
    public Button btnTrophy;
    public Button btnPause;
    // ���� ȭ�� ������Ʈ
    public GameObject PanelSetting;
    public GameObject PanelShop;

    // Index Detail : 0 > ���� ȭ�� ��ư ����
    //                        1 > �ٹ� ���� �� �� ���� ��ư ����
    //                        2 > �ΰ��� ȭ�� ��ư ����
    //                        3 > ��� ȭ�� ��ư ����
    //                        9 > ��ü ��ư ����� (ex. ��Ʈ�� ȭ�� ��)
    public void buttonViewController(int index)
    {
        switch (index)
        {
            case 0:
                btnHome.gameObject.SetActive(false);
                btnStore.gameObject.SetActive(false);
                btnSetting.gameObject.SetActive(true);
                btnTrophy.gameObject.SetActive(true);
                btnPause.gameObject.SetActive(false);
                break;
            case 1:
                btnHome.gameObject.SetActive(true);
                btnStore.gameObject.SetActive(true);
                btnSetting.gameObject.SetActive(true);
                btnTrophy.gameObject.SetActive(false);
                btnPause.gameObject.SetActive(false);
                break;
            case 2:
                btnHome.gameObject.SetActive(false);
                btnStore.gameObject.SetActive(false);
                btnSetting.gameObject.SetActive(false);
                btnTrophy.gameObject.SetActive(false);
                btnPause.gameObject.SetActive(true);
                break;
            case 3:
                btnHome.gameObject.SetActive(false);
                btnStore.gameObject.SetActive(false);
                btnSetting.gameObject.SetActive(true);
                btnTrophy.gameObject.SetActive(false);
                btnPause.gameObject.SetActive(false);
                break;
            case 9:
                btnHome.gameObject.SetActive(false);
                btnStore.gameObject.SetActive(false);
                btnSetting.gameObject.SetActive(false);
                btnTrophy.gameObject.SetActive(false);
                btnPause.gameObject.SetActive(false);
                break;
        }
    }

    //  Ȩ ��ư Ŭ�� �� ȭ�� ����
    public void buttonHomeClick()
    {
        // ���� ȭ������ �̵�
        UIManager.Instance.GoPanelMain();
        // BGM ����
        SoundManager.Instance.ForceAudioStop();
    }

    //  ���� ��ư Ŭ�� �� ȭ�� ����
    public void buttonStoreClick()
    {
        // �˾� ����
        PanelShop.SetActive(true);
    }

    // ���� ��ư Ŭ�� �� ȭ�� ����
    public void buttonSettingClick()
    {
        // �˾� ����
        PanelSetting.SetActive(true);
    }

    // Ʈ���� ��ư Ŭ�� �� ȭ�� ����
    public void buttonTrophyClick()
    {
        Debug.Log(">>>>>>>>>>>> Home Trophy Click.");
    }

    // �Ͻ����� ��ư Ŭ�� �� ȭ�� ����
    public void buttonPauseClick()
    {
        Debug.Log(">>>>>>>>>>>> Home Pause Click.");
    }

    void Start()
    {
        // �� ��ư �� Ŭ�� �̺�Ʈ ����
        btnHome.onClick.AddListener(buttonHomeClick);
        btnStore.onClick.AddListener(buttonStoreClick);
        btnSetting.onClick.AddListener(buttonSettingClick);
        btnTrophy.onClick.AddListener(buttonTrophyClick);
        btnPause.onClick.AddListener(buttonPauseClick);
    }

    void Update()
    {
        
    }
}
