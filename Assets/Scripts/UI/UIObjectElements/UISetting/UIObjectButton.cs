using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectButton : MonoBehaviour
{
    public Button ButtonHome;
    public Button ButtonSetting;
    public Button ButtonShop;
    public Button ButtonTrophy;
    public Button ButtonPause;
    // ��ư ���� --> Global Data���� �����ͼ� ���� ������� ����
    //const int SFX_Home = 1;
    //const int SFX_Setting = 4;

    // Index Detail : 0 > ���� ȭ�� ��ư ����
    //                        1 > �ٹ� ���� �� �� ���� ��ư ����
    //                        2 > �ΰ��� ȭ�� ��ư ����
    //                        3 > ��� ȭ�� ��ư ����
    //                        9 > ��ü ��ư ����� (ex. ��Ʈ�� ȭ�� ��)
    public void ButtonViewController(int index)
    {
        switch (index)
        {
            case 0:
                ButtonHome.gameObject.SetActive(false);
                ButtonSetting.gameObject.SetActive(true);
                ButtonShop.gameObject.SetActive(false);
                ButtonTrophy.gameObject.SetActive(false);
                ButtonPause.gameObject.SetActive(false);
                break;
            case 1:
                ButtonHome.gameObject.SetActive(false);
                ButtonSetting.gameObject.SetActive(true);
                ButtonShop.gameObject.SetActive(true);
                ButtonTrophy.gameObject.SetActive(false);
                ButtonPause.gameObject.SetActive(false);
                break;
            case 2:
                ButtonHome.gameObject.SetActive(true);
                ButtonSetting.gameObject.SetActive(true);
                ButtonShop.gameObject.SetActive(true);
                ButtonTrophy.gameObject.SetActive(false);
                ButtonPause.gameObject.SetActive(false);
                break;
            case 3:
                ButtonHome.gameObject.SetActive(false);
                ButtonSetting.gameObject.SetActive(false);
                ButtonShop.gameObject.SetActive(false);
                ButtonTrophy.gameObject.SetActive(false);
                ButtonPause.gameObject.SetActive(true);
                break;
            case 4:
                ButtonHome.gameObject.SetActive(false);
                ButtonSetting.gameObject.SetActive(true);
                ButtonShop.gameObject.SetActive(false);
                ButtonTrophy.gameObject.SetActive(false);
                ButtonPause.gameObject.SetActive(false);
                break;
            case 9:
                ButtonHome.gameObject.SetActive(false);
                ButtonSetting.gameObject.SetActive(false);
                ButtonShop.gameObject.SetActive(false);
                ButtonTrophy.gameObject.SetActive(false);
                ButtonPause.gameObject.SetActive(false);
                break;
        }
    }

    // �� ��ư �� �̺�Ʈ ����
    public void SetButtonEvent()
    {
        ButtonHome.onClick.AddListener(() => SetButtonClickEvent("goHome"));
        ButtonSetting.onClick.AddListener(() => SetButtonClickEvent("goSetting"));
        ButtonShop.onClick.AddListener(() => SetButtonClickEvent("goShop"));
        ButtonTrophy.onClick.AddListener(() => SetButtonClickEvent("goTrophy"));
        ButtonPause.onClick.AddListener(() => SetButtonClickEvent("goPause"));
    }

    // �� ��ư �� Ŭ�� �̺�Ʈ ����
    public void SetButtonClickEvent(string Division) 
    {
        if(Division.Equals("goHome"))
        {
            //  Ȩ ��ư Ŭ�� �� ȭ�� ����
            // ���� ȭ������ �̵�
            //UIManager.Instance.GoPanelMain();
            UIManager.Instance.GoPanelAlbumSelect();
            // BGM ����
            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingOut);
            SoundManager.Instance.ForceAudioStop();
        }
        else if (Division.Equals("goSetting"))
        {
            // ���� ��ư Ŭ�� �� ȭ�� ����
            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingOut);
            UIElementSetting.Instance.ButtonClickControll("Setting", "Open");
            // ��ư �̺�Ʈ Lock
            GlobalState.Instance.UserData.data.BackgroundProcActive = false;
        }
        else if (Division.Equals("goShop"))
        {
            //  ���� ��ư Ŭ�� �� ȭ�� ����
            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingOut);
            UIElementSetting.Instance.ButtonClickControll("Shop", "Open");
            // ��ư �̺�Ʈ Lock
            GlobalState.Instance.UserData.data.BackgroundProcActive = false;
        }
        else if (Division.Equals("goTrophy"))
        {
            // Ʈ���� ��ư Ŭ�� �� ȭ�� ����
            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingOut);
            UIElementSetting.Instance.ButtonClickControll("Trophy", "Open");
            // ��ư �̺�Ʈ Lock
            GlobalState.Instance.UserData.data.BackgroundProcActive = false;
        }
        else if (Division.Equals("goPause"))
        {
            // �Ͻ����� ��ư Ŭ�� �� ȭ�� ����
            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingOut);
            UIElementSetting.Instance.ButtonClickControll("Pause", "Open");
            // ��ư �̺�Ʈ Lock
            GlobalState.Instance.UserData.data.BackgroundProcActive = false;
        }
    }

    void Start()
    {
        // �� ��ư �� Ŭ�� �̺�Ʈ ����
        SetButtonEvent();
    }

    void Update()
    {
        
    }
}
