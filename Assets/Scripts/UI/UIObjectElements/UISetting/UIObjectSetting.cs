using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectSetting : MonoBehaviour
{
    // ��ư ������Ʈ ����
    public Button ButtonClose;
    public Button ButtonExit;
    public GameObject ButtonKeySetting;
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
        SFXSlider.onValueChanged.AddListener(delegate { setSoundChange("SFX"); });
        BGMSlider.onValueChanged.AddListener(delegate { setSoundChange("BGM"); });

        // ���� > Ű ���� > �и� �˾� ��ư �̺�Ʈ
        SeparationClose.onClick.AddListener(() => SetButtonClickEvent("SeparationClose"));
        SeparationInBoxs.transform.Find("SeparationInBox1").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(1, "In", false); });
        SeparationInBoxs.transform.Find("SeparationInBox1").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(1, "In"));
        SeparationInBoxs.transform.Find("SeparationInBox2").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(2, "In", false); });
        SeparationInBoxs.transform.Find("SeparationInBox2").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(2, "In"));
        SeparationInBoxs.transform.Find("SeparationInBox3").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(3, "In", false); });
        SeparationInBoxs.transform.Find("SeparationInBox3").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(3, "In"));
        SeparationInBoxs.transform.Find("SeparationInBox4").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(4, "In", false); });
        SeparationInBoxs.transform.Find("SeparationInBox4").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(4, "In"));
        SeparationOutBoxs.transform.Find("SeparationOutBox1").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(1, "Out", false); });
        SeparationOutBoxs.transform.Find("SeparationOutBox1").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(1, "Out"));
        SeparationOutBoxs.transform.Find("SeparationOutBox2").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(2, "Out", false); });
        SeparationOutBoxs.transform.Find("SeparationOutBox2").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(2, "Out"));
        SeparationOutBoxs.transform.Find("SeparationOutBox3").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(3, "Out", false); });
        SeparationOutBoxs.transform.Find("SeparationOutBox3").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(3, "Out"));
        SeparationOutBoxs.transform.Find("SeparationOutBox4").Find("BoxOn").GetComponent<InputField>().onValueChanged.AddListener(delegate { SetSeparationInputField(4, "Out", false); });
        SeparationOutBoxs.transform.Find("SeparationOutBox4").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(4, "Out"));

        // �Է�â forcus out �̺�Ʈ
        SeparationInBoxs.transform.Find("SeparationInBox1").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(1, "In", true); });
        SeparationInBoxs.transform.Find("SeparationInBox2").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(2, "In", true); });
        SeparationInBoxs.transform.Find("SeparationInBox3").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(3, "In", true); });
        SeparationInBoxs.transform.Find("SeparationInBox4").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(4, "In", true); });
        SeparationOutBoxs.transform.Find("SeparationOutBox1").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(1, "Out", true); });
        SeparationOutBoxs.transform.Find("SeparationOutBox2").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(2, "Out", true); });
        SeparationOutBoxs.transform.Find("SeparationOutBox3").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(3, "Out", true); });
        SeparationOutBoxs.transform.Find("SeparationOutBox4").Find("BoxOn").GetComponent<InputField>().onEndEdit.AddListener(delegate { SetSeparationInputField(4, "Out", true); });
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

    // ȿ��/����� ���� �̺�Ʈ
    public void setSoundChange(string Division)
    {
        if (Division.Equals("SFX"))
        {
            SFXGauge = SFXSlider.value;
            SoundManager.Instance.CtrlSFXVolume(SFXGauge);
        }
        else
        {
            BGMGauge = BGMSlider.value;
            SoundManager.Instance.CtrlBGMVolume(BGMGauge);
        }
    }

    // Ű ���� > �и� > Input Box Click �̺�Ʈ
    public void SetSeparationInputClick(int Index, string Division)
    {
        if (Division.Equals("In"))
        {
            SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").gameObject.SetActive(true);
            SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOff").gameObject.SetActive(false);
        }
        else if (Division.Equals("Out"))
        {
            SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").gameObject.SetActive(true);
            SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOff").gameObject.SetActive(false);
        }
    }

    // Ű ���� > �и� > Input box Field �̺�Ʈ
    public void SetSeparationInputField(int Index, string Division, Boolean flag)
    {
        string InputValue = Division.Equals("In") ?
            SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").GetComponent<InputField>().text :
                SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").GetComponent<InputField>().text;

        // �Է�â���� �Է� �� Backspace �� �����ų� ���콺 ��Ŀ�� �ƿ� �� �� �Է� â ��Ȱ��ȭ
        if (Input.GetKey(KeyCode.Backspace) || flag == true)
        {
            if (Division.Equals("In"))
            {
                SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").gameObject.SetActive(false);
                SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOff").gameObject.SetActive(true);
            }
            else if (Division.Equals("Out"))
            {
                SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").gameObject.SetActive(false);
                SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOff").gameObject.SetActive(true);
            }
        }
    }

    // 1. Out ��ư�� ��ü �ؾ� �� > �Է� �� ��Ȱ��ȭ �� �Է� �� ���� ��Ȱ��ȭ�� ��� �Ǹ� �Է� ���� �̹����� �ٲ�
    //     - ��ư�� �Է� ���� �ؽ�Ʈ�� ���� �̹��� ���
    // 2. �۷ι� ������ �� ���� �ϴ� ���μ��� �۾�

    // �۷ι� ���� ���� 
    public void SetGlobalValue()
    {
        /*
        string InputValue = Division.Equals("In") ?
            SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").GetComponent<InputField>().text :
                SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").GetComponent<InputField>().text;
        */
        string[] Inner = new string[4];
        string[] Outer = new string[4];
        float SFXValue = SFXSlider.value;
        float BGMValue = BGMSlider.value;

        for (int Index = 1; Index >= 4; Index++)
        {
            string InBoxValue = SeparationInBoxs.transform.Find("SeparationInBox" + Index).Find("BoxOn").GetComponent<InputField>().text;
            string OutBoxValue = SeparationOutBoxs.transform.Find("SeparationOutBox" + Index).Find("BoxOn").GetComponent<InputField>().text;

            Inner.Append(InBoxValue);
            Outer.Append(OutBoxValue);
        }

        Debug.Log(">>>>>>>>>>>>>>>>>>>>> Sound : " + SFXValue + " // " + BGMValue);
    }

    void Start()
    {
        SetButtonEvent();

        SetButtonClickEvent("IntegrationOff");
    }

    void Update()
    {
        
    }
}
