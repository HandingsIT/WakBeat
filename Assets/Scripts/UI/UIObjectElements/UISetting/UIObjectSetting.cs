using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    public int SeparationSize = 4;
    public string[] _InBoxValues = new string[4];
    public string[] _OutBoxValues = new string[4];

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
        //SFXSlider.onValueChanged.AddListener(delegate { setSFXSoundChange(); });
        BGMSlider.onValueChanged.AddListener(delegate { setBGMSoundChange(); });

        // ���� > Ű ���� > �и� �˾� ��ư �̺�Ʈ
        SeparationClose.onClick.AddListener(() => SetButtonClickEvent("SeparationClose"));
        SeparationInBoxs.transform.Find("SeparationInBox1").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 1); });
        SeparationInBoxs.transform.Find("SeparationInBox1").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationInBoxs.transform.Find("SeparationInBox1").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
        SeparationInBoxs.transform.Find("SeparationInBox2").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 2); });
        SeparationInBoxs.transform.Find("SeparationInBox2").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationInBoxs.transform.Find("SeparationInBox2").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
        SeparationInBoxs.transform.Find("SeparationInBox3").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 3); });
        SeparationInBoxs.transform.Find("SeparationInBox3").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationInBoxs.transform.Find("SeparationInBox3").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
        SeparationInBoxs.transform.Find("SeparationInBox4").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("In", 4); });
        SeparationInBoxs.transform.Find("SeparationInBox4").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationInBoxs.transform.Find("SeparationInBox4").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
        SeparationOutBoxs.transform.Find("SeparationOutBox1").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 1); });
        SeparationOutBoxs.transform.Find("SeparationOutBox1").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationOutBoxs.transform.Find("SeparationOutBox1").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
        SeparationOutBoxs.transform.Find("SeparationOutBox2").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 2); });
        SeparationOutBoxs.transform.Find("SeparationOutBox2").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationOutBoxs.transform.Find("SeparationOutBox2").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
        SeparationOutBoxs.transform.Find("SeparationOutBox3").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 3); });
        SeparationOutBoxs.transform.Find("SeparationOutBox3").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationOutBoxs.transform.Find("SeparationOutBox3").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
        SeparationOutBoxs.transform.Find("SeparationOutBox4").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationOffInputField("Out", 4); });
        SeparationOutBoxs.transform.Find("SeparationOutBox4").GetComponent<InputField>().onValueChanged.AddListener((word) => SeparationOutBoxs.transform.Find("SeparationOutBox4").GetComponent<InputField>().text = Regex.Replace(word, @"[^a-z]", ""));
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
            SeparationGroup.SetActive(true);
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
            ButtonKeySetting.transform.Find("ButtonIntegrationOn").gameObject.SetActive(false);
            ButtonKeySetting.transform.Find("ButtonIntegrationOff").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.SetActive(true);
            ButtonKeySetting.transform.Find("ButtonSeparationOff").gameObject.SetActive(false);

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
        if (Division.Equals("In"))
        {
            var inputFieldImage = SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<Image>();
            var inputField = SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<InputField>();

            inputField.readOnly = true;
            inputFieldImage.sprite = OffBoxImage;

            if (_InBoxValues.Length <= 0) { _InBoxValues = new string[SeparationSize]; }

            _InBoxValues[Index - 1] = inputField.text;
        }
        else
        {
            var inputFieldImage = SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<Image>();
            var inputField = SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).GetComponent<InputField>();

            inputField.readOnly = true;
            inputFieldImage.sprite = OffBoxImage;

            if (_OutBoxValues.Length <= 0) { _OutBoxValues = new string[SeparationSize]; }

            _OutBoxValues[Index - 1] = inputField.text;
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
    public void GetGlobalValue(Boolean flag)
    {
        string KeyDivision = "";

        // ���� ���� �ҷ����� ���� ��
        if (flag)
        {
            KeyDivision = GlobalState.Instance.UserData.data.KeyDivision.Equals("Integration") ? "IntegrationOff" : "SeparationOff";

            SFXSlider.value = GlobalState.Instance.UserData.data.SFXValue;
            BGMSlider.value = GlobalState.Instance.UserData.data.BGMValue;
            _InBoxValues = GlobalState.Instance.UserData.data.InnerOperationKey;
            _OutBoxValues = GlobalState.Instance.UserData.data.OuterOperationKey;

            for (int i = 0; i < GlobalState.Instance.UserData.data.InnerOperationKey.Length; i++)
            {
                if (null != GlobalState.Instance.UserData.data.InnerOperationKey[i] && !"".Equals(GlobalState.Instance.UserData.data.InnerOperationKey[i]))
                {
                    SeparationInBoxs.transform.Find("SeparationInBox" + (i + 1)).GetComponent<InputField>().text = GlobalState.Instance.UserData.data.InnerOperationKey[i];
                }
                SeparationInBoxs.transform.Find("SeparationInBox" + (i + 1)).GetComponent<InputField>().readOnly = true;
                SeparationInBoxs.transform.Find("SeparationInBox" + (i + 1)).GetComponent<Image>().sprite = OffBoxImage;
            }

            for (int i = 0; i < GlobalState.Instance.UserData.data.OuterOperationKey.Length; i++)
            {
                if (null != GlobalState.Instance.UserData.data.OuterOperationKey[i] && !"".Equals(GlobalState.Instance.UserData.data.OuterOperationKey[i]))
                {
                    SeparationOutBoxs.transform.Find("SeparationOutBox" + (i + 1)).GetComponent<InputField>().text = GlobalState.Instance.UserData.data.OuterOperationKey[i];
                }
                SeparationOutBoxs.transform.Find("SeparationOutBox" + (i + 1)).GetComponent<InputField>().readOnly = true;
                SeparationOutBoxs.transform.Find("SeparationOutBox" + (i + 1)).GetComponent<Image>().sprite = OffBoxImage;
            }
        } 
        else
        {
            KeyDivision = "IntegrationOff";

            for (int Index = 1; Index < 5; Index++)
            {
                var inputInFieldImage = SeparationInBoxs.transform.Find("SeparationInBox" + Index).GetComponent<Image>();
                var inputInField = SeparationInBoxs.transform.Find("SeparationInBox" + Index).GetComponent<InputField>();

                inputInField.readOnly = true;
                inputInFieldImage.sprite = OffBoxImage;

                var inputOutFieldImage = SeparationOutBoxs.transform.Find("SeparationOutBox" + Index).GetComponent<Image>();
                var inputOutField = SeparationOutBoxs.transform.Find("SeparationOutBox" + Index).GetComponent<InputField>();

                inputOutField.readOnly = true;
                inputOutFieldImage.sprite = OffBoxImage;
            }
        }

        SetButtonClickEvent(KeyDivision);
    }

    // �۷ι� ���� ����
    public void SetGlobalValue()
    {
        float SFXValue = SFXSlider.value;
        float BGMValue = BGMSlider.value;
        string KeyDivision = "";

        if(ButtonKeySetting.transform.Find("ButtonSeparationOn").gameObject.activeSelf)
        {
            Boolean SeparationYn = false;
            for(int i = 0; i < _InBoxValues.Length; i++)
            {
                if(null != _InBoxValues[i] && !"".Equals(_InBoxValues[i]))
                {
                    SeparationYn = true;
                }
            }
            for (int i = 0; i < _OutBoxValues.Length; i++)
            {
                if (null != _OutBoxValues[i] && !"".Equals(_OutBoxValues[i]))
                {
                    SeparationYn = true;
                }
            }

            if (SeparationYn)
            {
                KeyDivision = "Separation";
            } else
            {
                KeyDivision = "Integration";
            }
        }
        else
        {
            KeyDivision = "Integration";
        }

        // ���� ������ ����
        DataManager.SetKeyDivision = KeyDivision;
        DataManager.SetBGMValue = BGMValue;
        DataManager.SetSFXValue = SFXValue;
        DataManager.SetInnerOperationKey = _InBoxValues;
        DataManager.SetOuterOperationKey = _OutBoxValues;

        // ���� ������ ���� �� ���� ����
        DataManager.SaveUserData();
    }

    void Start()
    {
        // ��ư �̺�Ʈ ����
        SetButtonEvent();

        // �۷ι� ���� ȭ�� ����
        GetGlobalValue(GlobalState.Instance.UserData.data.FileYn);
    }

    void Update()
    {
        // Input Field ��Ŀ�� �� Ȱ��ȭ
        SetSeparationOnInputField();
    }
}
