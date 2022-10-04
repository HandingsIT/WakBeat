using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.UIElements;
using System.Net.Http.Headers;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    // ----------------------------- Fade --------------------------------
    #region Fade Management
    // Toggle Fade In Out => bool (isShow)
    public void FadeInOut(Image fadeImage, float fadeTime, bool isShow)
    {
        if (isShow == true)
        {
            fadeImage.CrossFadeAlpha(1f, fadeTime, false);
        }

        if (isShow == false)
        {
            fadeImage.CrossFadeAlpha(0f, fadeTime, false);
        }
    }

    public void FadeToBlack(Image fadeImage, float fadeTime)
    {
        fadeImage.CrossFadeAlpha(0f, 0f, false);
        fadeImage.CrossFadeAlpha(1f, fadeTime, false);
    }

    public void FadeToWhite(Image fadeImage, float fadeTime)
    {
        fadeImage.CrossFadeAlpha(1f, 0f, false);
        fadeImage.CrossFadeAlpha(0f, fadeTime, false);
    }
    #endregion


    // -------------------------------------------------------------------
    public GameObject[] MainPanels;
    public GameObject PanelSetting;
    public GameObject ButtonGoHome;
    public GameObject ButtonGoStore;
    public GameObject ButtonGoSetting;
    public GameObject ButtonGoTrophy;
    public GameObject ButtonGoPause;

    public void WantShowPanel(int index)
    {
        if (MainPanels != null)
        {
            for (int i = 0; i < MainPanels.Length; i++)
            {
                MainPanels[i].SetActive(index == i);
                if (index == i)
                {
                    GlobalState.Instance.CurrentPanelIndex = i;

                    Debug.Log($"{nameof(GlobalState.Instance.CurrentPanelIndex)} : {GlobalState.Instance.CurrentPanelIndex}");
                }
            }
        }

        // ȭ�� ��ȯ �� ��ư ���� ����
        switch (index)
        {
            case (int)GlobalData.UIMODE.INTRO:
                ShowSettingPanel(false, 9);
                break;
            case (int)GlobalData.UIMODE.MAIN:
                ShowSettingPanel(true, 0);
                break;
            case (int)GlobalData.UIMODE.SELECT_ALBUM:
                ShowSettingPanel(true, 1);
                break;
            case (int)GlobalData.UIMODE.SELECT_MUSIC:
                ShowSettingPanel(true, 1);
                break;
            case (int)GlobalData.UIMODE.GAME:
                ShowSettingPanel(true, 2);
                break;
            case (int)GlobalData.UIMODE.RESULT:
                ShowSettingPanel(true, 3);
                break;
        }
    }

    // ȭ�� ��ȯ �� ��ư ���� ���� Function
    public void ShowSettingPanel(bool isShow, int index)
    {
        PanelSetting.SetActive(isShow);

        switch(index) {
            case 0:
                ButtonGoHome.SetActive(false);
                ButtonGoStore.SetActive(false);
                ButtonGoSetting.SetActive(true);
                ButtonGoTrophy.SetActive(true);
                ButtonGoPause.SetActive(false);
                break;
            case 1:
                ButtonGoHome.SetActive(true);
                ButtonGoStore.SetActive(true);
                ButtonGoSetting.SetActive(true);
                ButtonGoTrophy.SetActive(false);
                ButtonGoPause.SetActive(false);
                break;
            case 2:
                ButtonGoHome.SetActive(false);
                ButtonGoStore.SetActive(false);
                ButtonGoSetting.SetActive(false);
                ButtonGoTrophy.SetActive(false);
                ButtonGoPause.SetActive(true);
                break;
            case 3:
                ButtonGoHome.SetActive(false);
                ButtonGoStore.SetActive(false);
                ButtonGoSetting.SetActive(true);
                ButtonGoTrophy.SetActive(false);
                ButtonGoPause.SetActive(false);
                break;
            case 9:
                break;
        }
    }

    public void GoPanelIntro()
    {
        WantShowPanel(0);
    }

    public void GoPanelMain()
    {
        WantShowPanel(1);
    }

    public void GoPanelAlbumSelect()
    {
        WantShowPanel(2);
    }

    public void GoPanelMusicSelect()
    {
        WantShowPanel(3);
    }

    public void GoPanelGamePlay()
    {
        WantShowPanel(4);
    }

    public void GoPanelResult()
    {
        WantShowPanel(5);
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
