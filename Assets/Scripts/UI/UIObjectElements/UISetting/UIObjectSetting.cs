using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class UIObjectSetting : MonoBehaviour
{
    // ��ư ������Ʈ ����
    public Button ButtonClose;
    public Button ButtonExit;
    public GameObject ButtonKeySetting;
    const int SFX_Home = 1;
    const int SFX_Setting = 4;
    // ȿ��/����� �����̴� ����
    public Slider SFXSlider;
    public Slider BGMSlider;
    public float SFXGauge;
    public float BGMGauge;
    // �и� �˾� ������Ʈ ����
    public GameObject SeparationGroup;
    public Button SeparationClose;
    public GameObject SeparationInBoxs;
    public GameObject SeparationOutBoxs;
    public Sprite OnBoxImage;
    public Sprite OffBoxImage;
    public string[] InBoxValues = new string[4];
    public string[] OutBoxValues = new string[4];

    // �� ��ư �� �̺�Ʈ ����
    public void SetButtonEvent()
    {
        // ���� ȭ�� ��ư �̺�Ʈ
        ButtonClose.onClick.AddListener(() => SetButtonClickEvent("Close"));
        ButtonExit.onClick.AddListener(() => SetButtonClickEvent("Exit"));
        ButtonKeySetting.transform.Find("ButtonIntegrationOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("IntegrationOn"));
        ButtonKeySetting.transform.Find("ButtonIntegrationOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("IntegrationOff"));
        ButtonKeySetting.transform.Find("ButtonSeparationOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SeparationOn"));
        ButtonKeySetting.transform.Find("ButtonSeparationOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SeparationOff"));

        // ȿ��/����� ���� �̺�Ʈ
        SFXSlider.onValueChanged.AddListener(delegate { setSFXSoundChange(); });
        BGMSlider.onValueChanged.AddListener(delegate { setBGMSoundChange(); });

        // ���� > Ű ���� > �и� �˾� ��ư �̺�Ʈ
        SeparationClose.onClick.AddListener(() => SetButtonClickEvent("SeparationClose"));

        SeparationInBoxs.transform.Find("SeparationInBox1").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 1); });
        SeparationInBoxs.transform.Find("SeparationInBox2").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 2); });
        SeparationInBoxs.transform.Find("SeparationInBox3").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 3); });
        SeparationInBoxs.transform.Find("SeparationInBox4").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 4); });
        SeparationOutBoxs.transform.Find("SeparationOutBox1").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 1); });
        SeparationOutBoxs.transform.Find("SeparationOutBox2").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 2); });
        SeparationOutBoxs.transform.Find("SeparationOutBox3").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 3); });
        SeparationOutBoxs.transform.Find("SeparationOutBox4").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 4); });
    }

    // �� ��ư �� Ŭ�� �̺�Ʈ ����
    public void SetButtonClickEvent(string Division)
    {
        if (Division.Equals("Close"))
        {
            // �Է� �� �ִ� ��� �� �����ͼ� �۷ι��� ����
            SetGlobalValue();

            // â �ݱ� ��ư �̺�Ʈ
            UIElementSetting.Instance.ButtonClickControll("Setting", "Close");

        }
        else if (Division.Equals("Exit"))
        {
            // �Է� �� �ִ� ��� �� �����ͼ� �۷ι��� ����
            SetGlobalValue();

            // �������� ��ư �̺�Ʈ
            UIElementSetting.Instance.ButtonClickControll("Setting", "Close");
            UIManager.Instance.GoPanelMain();
            SoundManager.Instance.ForceAudioStop();
        }
        else if (Division.Equals("IntegrationOn"))
        {
            // Ű ���� > ���� On Ŭ�� �̺�Ʈ
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(false);
            
            SeparationGroup.SetActive(true);
        }
        else if (Division.Equals("IntegrationOff"))
        {
            // Ű ���� > ���� Off Ŭ�� �̺�Ʈ
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(true);
           
            SeparationGroup.SetActive(false);
        }
        else if (Division.Equals("SeparationOn"))
        {
            // Ű ���� > �и� On Ŭ�� �̺�Ʈ
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(true);
            
            SeparationGroup.SetActive(false);
        }
        else if (Division.Equals("SeparationOff"))
        {
            // Ű ���� > �и� Off Ŭ�� �̺�Ʈ
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(false);

            SeparationGroup.SetActive(true);
        }
        else if (Division.Equals("SeparationClose"))
        {
            // Ű ���� > �и� > �˾� �ݱ� ��ư �̺�Ʈ
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(true);

            SeparationGroup.SetActive(false);
        }
    }

    // ����� ���� �̺�Ʈ
    public void setBGMSoundChange()
    {
        BGMGauge = BGMSlider.value;
        SoundManager.Instance.CtrlBGMVolume(BGMGauge);
    }

    // ȿ���� ���� �� 1ȸ ȿ���� ���
    public void setSFXSoundChange()
    {
        SFXGauge = SFXSlider.value;
        SoundManager.Instance.CtrlSFXVolume(SFXGauge);
        SoundManager.Instance.PlaySoundFX(SFX_Home);
    }

    // ��Ŀ�� ���� �� Input Field ��Ȱ��ȭ
    public void SetSeparationOffInputField(string Division, int Index)
    {
        if(Division.Equals("In"))
        {
            var inputFieldImage = SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<Image>();
            var inputField = SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<InputField>();

            inputField.readOnly = true;
            inputFieldImage.sprite = OffBoxImage;

            InBoxValues[Index - 1] = inputField.text;
        }
        else
        {
            var inputFieldImage = SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<Image>();
            var inputField = SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<InputField>();

            inputField.readOnly = true;
            inputFieldImage.sprite = OffBoxImage;

            OutBoxValues[Index - 1] = inputField.text;
        }
    }

    // ��Ŀ�� ���� �� Input Field Ȱ��ȭ
    public void SetSeparationOnInputField()
    {
        if (SeparationInBoxs.transform.Find("SeparationInBox1").GetComponent<InputField>().isFocused)
        {
            SeparationInBoxs.transform.Find("SeparationInBox1").GetComponent<InputField>().readOnly = false;
            SeparationInBoxs.transform.Find("SeparationInBox1").GetComponent<Image>().sprite = OnBoxImage;
        }
        else if (SeparationInBoxs.transform.Find("SeparationInBox2").GetComponent<InputField>().isFocused)
        {
            SeparationInBoxs.transform.Find("SeparationInBox2").GetComponent<InputField>().readOnly = false;
            SeparationInBoxs.transform.Find("SeparationInBox2").GetComponent<Image>().sprite = OnBoxImage;
        }
        else if (SeparationInBoxs.transform.Find("SeparationInBox3").GetComponent<InputField>().isFocused)
        {
            SeparationInBoxs.transform.Find("SeparationInBox3").GetComponent<InputField>().readOnly = false;
            SeparationInBoxs.transform.Find("SeparationInBox3").GetComponent<Image>().sprite = OnBoxImage;
        }
        else if (SeparationInBoxs.transform.Find("SeparationInBox4").GetComponent<InputField>().isFocused)
        {
            SeparationInBoxs.transform.Find("SeparationInBox4").GetComponent<InputField>().readOnly = false;
            SeparationInBoxs.transform.Find("SeparationInBox4").GetComponent<Image>().sprite = OnBoxImage;
        }
        else if (SeparationOutBoxs.transform.Find("SeparationOutBox1").GetComponent<InputField>().isFocused)
        {
            SeparationOutBoxs.transform.Find("SeparationOutBox1").GetComponent<InputField>().readOnly = false;
            SeparationOutBoxs.transform.Find("SeparationOutBox1").GetComponent<Image>().sprite = OnBoxImage;
        }
        else if (SeparationOutBoxs.transform.Find("SeparationOutBox2").GetComponent<InputField>().isFocused)
        {
            SeparationOutBoxs.transform.Find("SeparationOutBox2").GetComponent<InputField>().readOnly = false;
            SeparationOutBoxs.transform.Find("SeparationOutBox2").GetComponent<Image>().sprite = OnBoxImage;
        }
        else if (SeparationOutBoxs.transform.Find("SeparationOutBox3").GetComponent<InputField>().isFocused)
        {
            SeparationOutBoxs.transform.Find("SeparationOutBox3").GetComponent<InputField>().readOnly = false;
            SeparationOutBoxs.transform.Find("SeparationOutBox3").GetComponent<Image>().sprite = OnBoxImage;
        }
        else if (SeparationOutBoxs.transform.Find("SeparationOutBox4").GetComponent<InputField>().isFocused)
        {
            SeparationOutBoxs.transform.Find("SeparationOutBox4").GetComponent<InputField>().readOnly = false;
            SeparationOutBoxs.transform.Find("SeparationOutBox4").GetComponent<Image>().sprite = OnBoxImage;
        }
    }

    // �۷ι� ���� ȭ�� ����
    public void GetGlobalValue()
    {
        SetButtonClickEvent("IntegrationOff");
    }

    // �۷ι� ���� ����
    public void SetGlobalValue()
    {
        float SFXValue = SFXSlider.value;
        float BGMValue = BGMSlider.value;
    }

    void Start()
    {
        // ��ư �̺�Ʈ ����
        SetButtonEvent();

        // �۷ι� ���� ȭ�� ����
        GetGlobalValue();
    }

    void Update()
    {
        // Input Field ��Ŀ�� �� Ȱ��ȭ
        SetSeparationOnInputField();
    }
}
