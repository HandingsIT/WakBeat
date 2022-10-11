using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectSetting : MonoBehaviour
{
    // ��ư ������Ʈ ����
    public Button ButtonClose;
    public Button ButtonExit;
    public Button ButtonIntegrationOn;
    public Button ButtonIntegrationOff;
    public Button ButtonSeparationOn;
    public Button ButtonSeparationOff;
    // ȿ��/����� �����̴� ����
    public Slider EffectSlider;
    public Slider BackgroundSlider;
    public float EffectGauge;
    public float BackgroundGauge;
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
        ButtonIntegrationOn.onClick.AddListener(() => SetButtonClickEvent("IntegrationOn"));
        ButtonIntegrationOff.onClick.AddListener(() => SetButtonClickEvent("IntegrationOff"));
        ButtonSeparationOn.onClick.AddListener(() => SetButtonClickEvent("SeparationOn"));
        ButtonSeparationOff.onClick.AddListener(() => SetButtonClickEvent("SeparationOff"));
        // ȿ��/����� ���� �̺�Ʈ
        EffectSlider.onValueChanged.AddListener(delegate { setSoundChange("Effect"); });
        BackgroundSlider.onValueChanged.AddListener(delegate { setSoundChange("Background"); });
        // ���� > Ű ���� > �и� �˾� ��ư �̺�Ʈ
        SeparationClose.onClick.AddListener(() => SetButtonClickEvent("SeparationClose"));
        SeparationInBoxs.transform.Find("SeparationInBox1").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(1, "In", "On"));
        SeparationInBoxs.transform.Find("SeparationInBox1").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(1, "In", "Off"));
        SeparationInBoxs.transform.Find("SeparationInBox2").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(2, "In", "On"));
        SeparationInBoxs.transform.Find("SeparationInBox2").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(2, "In", "Off"));
        SeparationInBoxs.transform.Find("SeparationInBox3").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(3, "In", "On"));
        SeparationInBoxs.transform.Find("SeparationInBox3").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(3, "In", "Off"));
        SeparationInBoxs.transform.Find("SeparationInBox4").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(4, "In", "On"));
        SeparationInBoxs.transform.Find("SeparationInBox4").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(4, "In", "Off"));
        SeparationOutBoxs.transform.Find("SeparationOutBox1").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(1, "Out", "On"));
        SeparationOutBoxs.transform.Find("SeparationOutBox1").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(1, "Out", "Off"));
        SeparationOutBoxs.transform.Find("SeparationOutBox2").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(2, "Out", "On"));
        SeparationOutBoxs.transform.Find("SeparationOutBox2").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(2, "Out", "Off"));
        SeparationOutBoxs.transform.Find("SeparationOutBox3").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(3, "Out", "On"));
        SeparationOutBoxs.transform.Find("SeparationOutBox3").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(3, "Out", "Off"));
        SeparationOutBoxs.transform.Find("SeparationOutBox4").Find("BoxOn").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(4, "Out", "On"));
        SeparationOutBoxs.transform.Find("SeparationOutBox4").Find("BoxOff").GetComponent<Button>().onClick.AddListener(() => SetSeparationInputClick(4, "Out", "Off"));
    }

    // �� ��ư �� Ŭ�� �̺�Ʈ ����
    public void SetButtonClickEvent(string Division)
    {
        if (Division.Equals("Close"))
        {
            // â �ݱ� ��ư �̺�Ʈ
            UIElementSetting.Instance.PanelSetting.SetActive(false);
        }
        else if (Division.Equals("Exit"))
        {
            // �������� ��ư �̺�Ʈ
            UIElementSetting.Instance.PanelSetting.SetActive(false);
            UIManager.Instance.GoPanelMain();
            SoundManager.Instance.ForceAudioStop();
        }
        else if (Division.Equals("IntegrationOn"))
        {
            // Ű ���� > ���� On Ŭ�� �̺�Ʈ
            ButtonIntegrationOn.gameObject.SetActive(false);
            ButtonIntegrationOff.gameObject.SetActive(true);
            ButtonSeparationOn.gameObject.SetActive(true);
            ButtonSeparationOff.gameObject.SetActive(false);

            SeparationGroup.SetActive(true);
        }
        else if (Division.Equals("IntegrationOff"))
        {
            // Ű ���� > ���� Off Ŭ�� �̺�Ʈ
            ButtonIntegrationOn.gameObject.SetActive(true);
            ButtonIntegrationOff.gameObject.SetActive(false);
            ButtonSeparationOn.gameObject.SetActive(false);
            ButtonSeparationOff.gameObject.SetActive(true);

            SeparationGroup.SetActive(false);
        }
        else if (Division.Equals("SeparationOn"))
        {
            // Ű ���� > �и� On Ŭ�� �̺�Ʈ
            ButtonIntegrationOn.gameObject.SetActive(true);
            ButtonIntegrationOff.gameObject.SetActive(false);
            ButtonSeparationOn.gameObject.SetActive(false);
            ButtonSeparationOff.gameObject.SetActive(true);

            SeparationGroup.SetActive(false);
        }
        else if (Division.Equals("SeparationOff"))
        {
            // Ű ���� > �и� Off Ŭ�� �̺�Ʈ
            ButtonIntegrationOn.gameObject.SetActive(false);
            ButtonIntegrationOff.gameObject.SetActive(true);
            ButtonSeparationOn.gameObject.SetActive(true);
            ButtonSeparationOff.gameObject.SetActive(false);

            SeparationGroup.SetActive(true);
        }
        else if (Division.Equals("SeparationClose"))
        {
            // Ű ���� > �и� > �˾� �ݱ� ��ư �̺�Ʈ
            ButtonIntegrationOn.gameObject.SetActive(true);
            ButtonIntegrationOff.gameObject.SetActive(false);
            ButtonSeparationOn.gameObject.SetActive(false);
            ButtonSeparationOff.gameObject.SetActive(true);

            SeparationGroup.SetActive(false);
        }
    }

    // ȿ��/����� ���� �̺�Ʈ
    public void setSoundChange(string Division)
    {
        if(Division.Equals("Effect"))
        {
            EffectGauge = EffectSlider.value;
            SoundManager.Instance.CtrlSFXVolume(EffectGauge);
        }
        else
        {
            BackgroundGauge = BackgroundSlider.value;
            SoundManager.Instance.CtrlBGMVolume(BackgroundGauge);
        }
    }

    // Ű ���� > �и� > Input Box Click �̺�Ʈ
    public void SetSeparationInputClick(int Index, string Division, string OnOff)
    {
        if (OnOff.Equals("On"))
        {
            if(Division.Equals("In"))
            {
                SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").gameObject.SetActive(false);
                SeparationInBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOff").gameObject.SetActive(true);
            } else if (Division.Equals("Out"))
            {
                SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOn").gameObject.SetActive(false);
                SeparationOutBoxs.transform.Find("Separation" + Division + "Box" + Index).Find("BoxOff").gameObject.SetActive(true);
            }
        }
        else if (OnOff.Equals("Off"))
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
