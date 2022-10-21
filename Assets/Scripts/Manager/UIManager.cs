using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public GameObject UIElementSetting;

    [SerializeField] private UIElementFadePanel uiElementFadePanel;

    void Start()
    {

    }

    private void Init()
    {

    }

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

        //// ȭ�� ��ȯ �� ��ư ���� ����  // ��� ���̵� ó���ϰڽ��ϴ� KD_Han
        UIElementSetting.GetComponent<UIElementSetting>().PanelViewController(index);
    }

    public void GoPanelIntro()
    {
        WantShowPanel((int)GlobalData.UIMODE.INTRO);
    }

    public void GoPanelMain()
    {
        WantShowPanel((int)GlobalData.UIMODE.MAIN);
    }

    public void GoPanelAlbumSelect()
    {
        WantShowPanel((int)GlobalData.UIMODE.SELECT_ALBUM);
    }

    public void GoPanelMusicSelect()
    {
        WantShowPanel((int)GlobalData.UIMODE.SELECT_MUSIC);
    }

    public void GoPanelGamePlay()
    {
        WantShowPanel((int)GlobalData.UIMODE.GAME);
    }

    public void GoPanelResult()
    {
        WantShowPanel((int)GlobalData.UIMODE.RESULT);
    }

    public void OnClickStartButton()
    {
        if (uiElementFadePanel)
        {
            uiElementFadePanel.MainToAlbumTransition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
