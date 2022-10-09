using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectShop : MonoBehaviour
{
    // ���� ȭ�� ������Ʈ
    public GameObject PanelShop;
    // �� Tab �� ȭ�� ������Ʈ
    public GameObject TabSkinGroup;
    public GameObject TabSkillGroup;
    public GameObject TabVideoGroup;
    // ��ư ����
    public Button ButtonClose;
    public Button ButtonSkinTabOn;
    public Button ButtonSkinTabOff;
    public Button ButtonSkillTabOn;
    public Button ButtonSkillTabOff;
    public Button ButtonVideoTabOn;
    public Button ButtonVideoTabOff;

    // ��ư ���� �̺�Ʈ ����
    public void ButtonEvent(string Division)
    {
        // Division : ��ư �̺�Ʈ ������
        if(Division.Equals("SkinOn") || Division.Equals("SkinOff"))
        {
            // ��Ų ���� On Ŭ�� ��
            ButtonSkinTabOn.gameObject.SetActive(true);
            ButtonSkinTabOff.gameObject.SetActive(false);
            ButtonSkillTabOn.gameObject.SetActive(false);
            ButtonSkillTabOff.gameObject.SetActive(true);
            ButtonVideoTabOn.gameObject.SetActive(false);
            ButtonVideoTabOff.gameObject.SetActive(true);
            TabSkinGroup.SetActive(true);
            TabSkillGroup.SetActive(false);
            TabVideoGroup.SetActive(false);
        }
        else if (Division.Equals("SkillOn") || Division.Equals("SkillOff"))
        {
            ButtonSkinTabOn.gameObject.SetActive(false);
            ButtonSkinTabOff.gameObject.SetActive(true);
            ButtonSkillTabOn.gameObject.SetActive(true);
            ButtonSkillTabOff.gameObject.SetActive(false);
            ButtonVideoTabOn.gameObject.SetActive(false);
            ButtonVideoTabOff.gameObject.SetActive(true);
            TabSkinGroup.SetActive(false);
            TabSkillGroup.SetActive(true);
            TabVideoGroup.SetActive(false);
        }
        else if (Division.Equals("VideoOn") || Division.Equals("VideoOff"))
        {
            ButtonSkinTabOn.gameObject.SetActive(false);
            ButtonSkinTabOff.gameObject.SetActive(true);
            ButtonSkillTabOn.gameObject.SetActive(false);
            ButtonSkillTabOff.gameObject.SetActive(true);
            ButtonVideoTabOn.gameObject.SetActive(true);
            ButtonVideoTabOff.gameObject.SetActive(false);
            TabSkinGroup.SetActive(false);
            TabSkillGroup.SetActive(false);
            TabVideoGroup.SetActive(true);
        }
    }

    // â �ݱ� ��ư Ŭ�� ��
    public void buttonCloseClick()
    {
        // �۷ι� ������ ���ÿ��� ���� �� ���� ����
        SetGlobalValueSetting("CLOSE");
        // �˾� �ݱ�
        PanelShop.SetActive(false);
    }

    public void SetEvent()
    {
        // ���� ȭ�� ��ư �̺�Ʈ �߰�
        ButtonClose.onClick.AddListener(buttonCloseClick);
        ButtonSkinTabOn.onClick.AddListener(() => ButtonEvent("SkinOn"));
        ButtonSkinTabOff.onClick.AddListener(() => ButtonEvent("SkinOff"));
        ButtonSkillTabOn.onClick.AddListener(() => ButtonEvent("SkillOn"));
        ButtonSkillTabOff.onClick.AddListener(() => ButtonEvent("SkillOff"));
        ButtonVideoTabOn.onClick.AddListener(() => ButtonEvent("VideoOn"));
        ButtonVideoTabOff.onClick.AddListener(() => ButtonEvent("VideoOff"));

        // ȭ�� ���� Tab Ȱ��ȭ ����
        ButtonEvent("SkinOn");
    }

    // �۷ι����� ��������
    public void GetGlobalValueSetting()
    {

    }

    // �۷ι����� ����
    public void SetGlobalValueSetting(string Division)
    {

    }

    void Start()
    {
        SetEvent();
        GetGlobalValueSetting();
    }

    void Update()
    {
        
    }
}
