using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectSetting : MonoBehaviour
{
    // ���� ȭ�� ������Ʈ
    public GameObject PanelSetting;
    // ��ư ����
    public Button ButtonClose;
    public Button ButtonExit;
    public Button KeyIntegOn;
    public Button KeyIntegOff;
    public Button KeySeparOn;
    public Button KeySeparOff;
    // ȿ����/����� Slider
    public Slider EffectSlider;
    public Slider BackgroundSlider;
    public float EffectGauge;
    public float BackgroundGauge;
    // (�۷ι������ͷ� �����ʿ�) Ű ���� �з� : ���� > I / �и� > S
    // �۷ι� ����Ʈ ���� ��ġ, �۷ι� ����� ���� ��ġ
    public string GBKeyboardDivision;
    public float GBEffectSound;
    public float GBBackgroundSound;
    // Ű���� > �и� ����
    public GameObject SeparationPopup;
    public Button SeparationButtonClose;

    // â �ݱ� ��ư Ŭ�� ��
    public void buttonCloseClick()
    {
        // �۷ι� ������ ���ÿ��� ���� �� ���� ����
        SetGlobalValueSetting("CLOSE");
        // �˾� �ݱ�
        PanelSetting.SetActive(false);
    }

    // �������� ��ư Ŭ�� ��
    public void buttonExitClick()
    {
        // �۷ι� ������ ���ÿ��� ���� �� ���� ����
        SetGlobalValueSetting("EXIT");
        // �˾� �ݱ�
        PanelSetting.SetActive(false);
        // �������� ���ư��� > ���Ŀ� ���� ����� ����
        UIManager.Instance.GoPanelMain();
        SoundManager.Instance.ForceAudioStop();
    }

    // Ű ���� ���� On ��ư Ŭ�� ��
    public void KeyIntegOnClick()
    {
        int InOnIdx = KeyIntegOn.transform.GetSiblingIndex();
        int InOffIdx = KeyIntegOff.transform.GetSiblingIndex();
        int SeOnIdx = KeySeparOn.transform.GetSiblingIndex();
        int SeOffIdx = KeySeparOff.transform.GetSiblingIndex();

        if (InOnIdx > InOffIdx)
        {
            if (SeOnIdx < SeOffIdx)
            {
                KeySeparOn.transform.SetSiblingIndex(SeOffIdx);
                KeySeparOff.transform.SetSiblingIndex(SeOnIdx);
            }

            KeyIntegOn.transform.SetSiblingIndex(InOffIdx);
            KeyIntegOff.transform.SetSiblingIndex(InOnIdx);

            // �۷ι������� KeyboardDivision ����
            GBKeyboardDivision = "I";

            // Ű ���� > �и� > ���� â On
            SeparationPopup.SetActive(true);
        }
    }

    // Ű ���� ���� Off ��ư Ŭ�� ��
    public void KeyIntegOffClick()
    {
        int InOnIdx = KeyIntegOn.transform.GetSiblingIndex();
        int InOffIdx = KeyIntegOff.transform.GetSiblingIndex();
        int SeOnIdx = KeySeparOn.transform.GetSiblingIndex();
        int SeOffIdx = KeySeparOff.transform.GetSiblingIndex();

        if (InOnIdx < InOffIdx)
        {
            if (SeOnIdx > SeOffIdx)
            {
                KeySeparOn.transform.SetSiblingIndex(SeOffIdx);
                KeySeparOff.transform.SetSiblingIndex(SeOnIdx);
            }

            KeyIntegOff.transform.SetSiblingIndex(InOnIdx);
            KeyIntegOn.transform.SetSiblingIndex(InOffIdx);

            // Ű ���� > �и� > ���� â Off
            SeparationPopup.SetActive(false);
        }
    }

    // Ű ���� �и� On ��ư Ŭ�� ��
    public void KeySeparOnClick()
    {
        int SeOnIdx = KeySeparOn.transform.GetSiblingIndex();
        int SeOffIdx = KeySeparOff.transform.GetSiblingIndex();
        int InOnIdx = KeyIntegOn.transform.GetSiblingIndex();
        int InOffIdx = KeyIntegOff.transform.GetSiblingIndex();

        if (SeOnIdx > SeOffIdx)
        {
            if (InOnIdx < InOffIdx)
            {
                KeyIntegOff.transform.SetSiblingIndex(InOnIdx);
                KeyIntegOn.transform.SetSiblingIndex(InOffIdx);
            }

            KeySeparOn.transform.SetSiblingIndex(SeOffIdx);
            KeySeparOff.transform.SetSiblingIndex(SeOnIdx);

            // �۷ι������� KeyboardDivision ����
            GBKeyboardDivision = "S";

            // Ű ���� > �и� > ���� â Off
            SeparationPopup.SetActive(false);
        }
    }

    // Ű ���� �и� Off ��ư Ŭ�� ��
    public void KeySeparOffClick()
    {
        int SeOnIdx = KeySeparOn.transform.GetSiblingIndex();
        int SeOffIdx = KeySeparOff.transform.GetSiblingIndex();
        int InOnIdx = KeyIntegOn.transform.GetSiblingIndex();
        int InOffIdx = KeyIntegOff.transform.GetSiblingIndex();

        if (SeOnIdx < SeOffIdx)
        {
            if (InOnIdx > InOffIdx)
            {
                KeyIntegOff.transform.SetSiblingIndex(InOnIdx);
                KeyIntegOn.transform.SetSiblingIndex(InOffIdx);
            }

            KeySeparOn.transform.SetSiblingIndex(SeOffIdx);
            KeySeparOff.transform.SetSiblingIndex(SeOnIdx);
        }

        // Ű ���� > �и� > ���� â On
        SeparationPopup.SetActive(true);
    }

    // ȿ���� �Ҹ� ��ġ ���� 
    public void setEffectSoundChange()
    {
        EffectGauge = EffectSlider.value;
        SoundManager.Instance.CtrlSFXVolume(EffectGauge);
    }

    // ����� �Ҹ� ��ġ ����
    public void setBackgroundSoundChange()
    {
        BackgroundGauge = BackgroundSlider.value;
        SoundManager.Instance.CtrlBGMVolume(BackgroundGauge);
    }

    // Ű ���� > �и� â �ݱ� ��ư Ŭ�� ��
    public void SeparationButtonCloseClick()
    {
        // �Է� �� ���� �� �� ����

        // �и� Off ��ư Ŭ�� �̺�Ʈ ȣ��
        KeySeparOnClick();
    }

    // ��ư �̺�Ʈ ����
    public void SetEvent()
    {
        // ���� ȭ�� ��ư �̺�Ʈ �߰�
        ButtonClose.onClick.AddListener(buttonCloseClick);
        ButtonExit.onClick.AddListener(buttonExitClick);
        KeyIntegOn.onClick.AddListener(KeyIntegOnClick);
        KeyIntegOff.onClick.AddListener(KeyIntegOffClick);
        KeySeparOn.onClick.AddListener(KeySeparOnClick);
        KeySeparOff.onClick.AddListener(KeySeparOffClick);
        EffectSlider.onValueChanged.AddListener(delegate { setEffectSoundChange(); });
        BackgroundSlider.onValueChanged.AddListener(delegate { setBackgroundSoundChange(); });
        // Ű ���� > �и� ��ư �̺�Ʈ �߰� 
        SeparationButtonClose.onClick.AddListener(SeparationButtonCloseClick);

        // Ű ������ ���� �� ��
        if (GBKeyboardDivision.Equals("I"))
        {
            KeyIntegOffClick();
        }
        // Ű ������ �и� �� ��
        else if (GBKeyboardDivision.Equals("S"))
        {
            KeySeparOffClick();
        }
    }

    // �۷ι����� ��������
    public void GetGlobalValueSetting()
    {
        // ȿ���� �� ���� ������ ����
        EffectSlider.value = GBEffectSound;
        BackgroundSlider.value = GBBackgroundSound;

        // Ű���� �� ���� (����/�и�)

        // Ű���� > �и� �� ���� (8�� ĭ�� �� ����)
    }

    // �۷ι����� ����
    public void SetGlobalValueSetting(string Division)
    {
        // �۷ι� ���� ����

        // ���� ���� �� �۷ι� ������ ���Ϸ� ��ȯ�Ͽ� Drop
        if (Division.Equals("EXIT"))
        {

        }
    }

    void Start()
    {
        // ��ư �̺�Ʈ �߰�
        SetEvent();

        // �۷ι� ���� ��������
        GetGlobalValueSetting();
    }

    void Update()
    {
        
    }
}
