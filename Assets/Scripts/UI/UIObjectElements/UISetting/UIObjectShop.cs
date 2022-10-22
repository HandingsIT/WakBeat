using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectShop : MonoBehaviour
{
    [SerializeField] ShopInfo ShopInfo;
    public Button ButtonClose;
    public GameObject TabObject;

    // ��Ų ������ ������Ʈ
    public GameObject SkinGroup;
    [SerializeField] UIObjectShopSkinItem SkinItemPrefab;

    // ��ų ������ ������Ʈ
    public GameObject SkillGroup;
    [SerializeField] UIObjectShopSkillItem SkillItemPrefab;

    // ���� ������ ������Ʈ
    public GameObject VideoGroup;

    public void SetPrefab()
    {
        int SkinCount = DataManager.SetSkinCount;
        int SkillCount = DataManager.SetSkillCount;
        int SkinIndex = 1;
        int SkillIndex = 1;

        foreach (var info in ShopInfo.SkinItemInfos)
        {
            var item = Instantiate(SkinItemPrefab, SkinItemPrefab.transform.parent);
            item.SetSkin(info);
            item.name = "Skin_Prefab_" + SkinIndex;
            item.gameObject.transform.Find("ItemButtons").Find("ButtonBuy").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(item.name, "Buy"));
            item.gameObject.transform.Find("ItemButtons").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(item.name, "On"));
            item.gameObject.transform.Find("ItemButtons").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(item.name, "Off"));
            item.gameObject.SetActive(true);

            SkinIndex++;
        }

        foreach (var info in ShopInfo.SkillItemInfos)
        {
            var item = Instantiate(SkillItemPrefab, SkillItemPrefab.transform.parent);
            item.SetSkill(info);
            item.name = "Skill_Prefab_" + SkillIndex;
            item.gameObject.transform.Find("Open").Find("SkillButton").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetSkillButtonEvent(item.name, "On"));
            item.gameObject.transform.Find("Open").Find("SkillButton").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetSkillButtonEvent(item.name, "Off"));
            item.gameObject.transform.Find("Lock").Find("SkillButton").Find("ButtonLock").GetComponent<Button>().onClick.AddListener(() => SetSkillButtonEvent(item.name, "Lock"));
            item.gameObject.SetActive(true);

            SkillIndex++;
        }
    }

    // �� ��ư �� �̺�Ʈ ����
    public void SetButtonEvent()
    {
        // ���� ��ư �̺�Ʈ
        ButtonClose.onClick.AddListener(() => SetButtonClickEvent("Close"));
        TabObject.transform.Find("TabSkin").Find("TabOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SkinOn"));
        TabObject.transform.Find("TabSkin").Find("TabOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SkinOff"));
        TabObject.transform.Find("TabSkill").Find("TabOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SkillOn"));
        TabObject.transform.Find("TabSkill").Find("TabOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SkillOff"));
        TabObject.transform.Find("TabVideo").Find("TabOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("VideoOn"));
        TabObject.transform.Find("TabVideo").Find("TabOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("VideoOff"));

        

        /*
        for (int Index = 1; Index <= SkinCount; Index++)
        {
            //SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content")

            Debug.Log(">>>>>>>>>>> " + (SkinGroup.transform.Find("SkinItems") == null));
            Debug.Log(">>>>>>>>>>> " + (SkinGroup.transform.Find("SkinItems").Find("Viewport") == null));
            Debug.Log(">>>>>>>>>>> " + (SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content") == null));

            //Debug.Log(">>>>>>>>>>> " + (SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content") == null));

            Debug.Log(">>>>>>>>>> Index : " + Index);
            Debug.Log(">>>>>>>>>> Index : " + .GetChild(0).gameObject.name);
            Debug.Log(">>>>>>>>>> Index : " + SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").GetChild(1).gameObject.name);
            //SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonBuy").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(Index, "Buy"));

    
            Debug.Log(">>>>>>>>>>> " + (SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons") == null));
            Debug.Log(">>>>>>>>>>> " + (SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonBuy") == null));

            SkinGroup.transform.Find("Skin_Prefab_" + Index).Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(Index, "On"));
            SkinGroup.transform.Find("Skin_Prefab_" + Index).Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(Index, "Off"));

            SkinGroup.transform.Find("ButtonBuy").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(Index, "Buy"));
            SkinGroup.transform.Find("Skin_Prefab_" + Index).Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(Index, "On"));
            SkinGroup.transform.Find("Skin_Prefab_" + Index).Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(Index, "Off"));
        }
        */

        // ���� > ��Ų ���� ��ư �̺�Ʈ
        /*
        SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonBuy").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("BlackBuy"));
        SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("BlackOn"));
        SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("BlackOff"));
        SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonBuy").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("BlueBuy"));
        SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("BlueOn"));
        SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("BlueOff"));
        SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonBuy").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("GreenBuy"));
        SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("GreenOn"));
        SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("GreenOff"));
        SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonBuy").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("OrangeBuy"));
        SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("OrangeOn"));
        SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("OrangeOff"));
        */

        // ���� > ��ų ���� ��ư �̺�Ʈ
        /*
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill1").Find("Open").Find("SkillButton").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill1On"));
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill1").Find("Open").Find("SkillButton").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill1Off"));
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill2").Find("Open").Find("SkillButton").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill2On"));
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill2").Find("Open").Find("SkillButton").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill2Off"));
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill3").Find("Open").Find("SkillButton").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill3On"));
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill3").Find("Open").Find("SkillButton").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill3Off"));
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill4").Find("Open").Find("SkillButton").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill4On"));
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill4").Find("Open").Find("SkillButton").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill4Off"));
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill5").Find("Open").Find("SkillButton").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill5On"));
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill5").Find("Open").Find("SkillButton").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill5Off"));
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill1").Find("Lock").Find("SkillButton").Find("ButtonLock").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill1Lock"));
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill2").Find("Lock").Find("SkillButton").Find("ButtonLock").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill2Lock"));
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill3").Find("Lock").Find("SkillButton").Find("ButtonLock").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill3Lock"));
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill4").Find("Lock").Find("SkillButton").Find("ButtonLock").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill4Lock"));
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill5").Find("Lock").Find("SkillButton").Find("ButtonLock").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("Skill5Lock"));
        */
        // ���� > ���� ���� ��ư �̺�Ʈ
    }

    // ���� > ��Ų > ��ư �̺�Ʈ ����
    public void SetSkinButtonEvent(string Name, string Division)
    {
        Debug.Log(">>>>>>>>>> SetSkinButtonEvent : " + Name + " // " + Division);

        if(Division.Equals("Buy"))
        {
            // �ر� �̱���

        }
        else if (Division.Equals("On"))
        {
            // ���
        }
        else if (Division.Equals("Off"))
        {
            // �̻��
        }
    }

    // ���� > ��ų > ��ư �̺�Ʈ ����
    public void SetSkillButtonEvent(string Name, string Division)
    {
        Debug.Log(">>>>>>>>>> SetSkinButtonEvent : " + Name + " // " + Division);

        if (Division.Equals("Lock"))
        {
            // ��� ����

        }
        else if (Division.Equals("On"))
        {
            // ���
        }
        else if (Division.Equals("Off"))
        {
            // �̻��
        }
    }

    // �� ��ư �� Ŭ�� �̺�Ʈ ����
    public void SetButtonClickEvent(string Division)
    {
        if (Division.Equals("Close"))
        {
            // �Է� �� �ִ� ��� �� �����ͼ� �۷ι��� ����
            SetGlobalValue();

            // â �ݱ� ��ư �̺�Ʈ
            UIElementSetting.Instance.ButtonClickControll("Shop", "Close");
        }
        else if (Division.Equals("SkinOn") || Division.Equals("SkinOff"))
        {
            // ���� > ��Ų ��ư �̺�Ʈ
            SkinGroup.SetActive(true);
            SkillGroup.SetActive(false);
            VideoGroup.SetActive(false);
            TabObject.transform.Find("TabSkin").Find("TabOn").gameObject.SetActive(true);
            TabObject.transform.Find("TabSkin").Find("TabOff").gameObject.SetActive(false);
            TabObject.transform.Find("TabSkill").Find("TabOn").gameObject.SetActive(false);
            TabObject.transform.Find("TabSkill").Find("TabOff").gameObject.SetActive(true);
            TabObject.transform.Find("TabVideo").Find("TabOn").gameObject.SetActive(false);
            TabObject.transform.Find("TabVideo").Find("TabOff").gameObject.SetActive(true);
        }
        else if (Division.Equals("SkillOn") || Division.Equals("SkillOff"))
        {
            // ���� > ��ų ��ư �̺�Ʈ
            SkinGroup.SetActive(false);
            SkillGroup.SetActive(true);
            VideoGroup.SetActive(false);
            TabObject.transform.Find("TabSkin").Find("TabOn").gameObject.SetActive(false);
            TabObject.transform.Find("TabSkin").Find("TabOff").gameObject.SetActive(true);
            TabObject.transform.Find("TabSkill").Find("TabOn").gameObject.SetActive(true);
            TabObject.transform.Find("TabSkill").Find("TabOff").gameObject.SetActive(false);
            TabObject.transform.Find("TabVideo").Find("TabOn").gameObject.SetActive(false);
            TabObject.transform.Find("TabVideo").Find("TabOff").gameObject.SetActive(true);
        }
        else if (Division.Equals("VideoOn") || Division.Equals("VideoOff"))
        {
            // ���� > ���� ��ư �̺�Ʈ
            SkinGroup.SetActive(false);
            SkillGroup.SetActive(false);
            VideoGroup.SetActive(true);
            TabObject.transform.Find("TabSkin").Find("TabOn").gameObject.SetActive(false);
            TabObject.transform.Find("TabSkin").Find("TabOff").gameObject.SetActive(true);
            TabObject.transform.Find("TabSkill").Find("TabOn").gameObject.SetActive(false);
            TabObject.transform.Find("TabSkill").Find("TabOff").gameObject.SetActive(true);
            TabObject.transform.Find("TabVideo").Find("TabOn").gameObject.SetActive(true);
            TabObject.transform.Find("TabVideo").Find("TabOff").gameObject.SetActive(true);
        }
        /*
        else if (Division.Equals("BlackBuy"))
        {
            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonBuy").gameObject.SetActive(false);
        }
        else if (Division.Equals("BlackOn"))
        {
            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);

            setDefaultSkin();
        }
        else if (Division.Equals("BlackOff"))
        {
            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(false);

            SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
        }
        else if (Division.Equals("BlueBuy"))
        {
            SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonBuy").gameObject.SetActive(false);
        }
        else if (Division.Equals("BlueOn"))
        {
            SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);

            setDefaultSkin();
        }
        else if (Division.Equals("BlueOff"))
        {
            SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(false);

            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
        }
        else if (Division.Equals("GreenBuy"))
        {
            SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonBuy").gameObject.SetActive(false);
        }
        else if (Division.Equals("GreenOn"))
        {
            SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);

            setDefaultSkin();
        }
        else if (Division.Equals("GreenOff"))
        {
            SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(false);

            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
        }
        else if (Division.Equals("OrangeBuy"))
        {
            SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonBuy").gameObject.SetActive(false);
        }
        else if (Division.Equals("OrangeOn"))
        {
            SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);

            setDefaultSkin();
        }
        else if (Division.Equals("OrangeOff"))
        {
            SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(false);

            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
        }
        else if (Division.Equals("Skill1On"))
        {
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill1").Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(false);
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill1").Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(true);
        }
        else if (Division.Equals("Skill1Off"))
        {
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill1").Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(true);
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill1").Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(false);
        }
        else if (Division.Equals("Skill1Lock"))
        {
            SetUnLock(1);
        }
        else if (Division.Equals("Skill2On"))
        {
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill2").Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(false);
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill2").Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(true);
        }
        else if (Division.Equals("Skill2Off"))
        {
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill2").Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(true);
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill2").Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(false);
        }
        else if (Division.Equals("Skill2Lock"))
        {
            SetUnLock(2);
        }
        else if (Division.Equals("Skill3On"))
        {
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill3").Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(false);
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill3").Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(true);
        }
        else if (Division.Equals("Skill3Off"))
        {
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill3").Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(true);
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill3").Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(false);
        }
        else if (Division.Equals("Skill3Lock"))
        {
            SetUnLock(3);
        }
        else if (Division.Equals("Skill4On"))
        {
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill4").Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(false);
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill4").Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(true);
        }
        else if (Division.Equals("Skill4Off"))
        {
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill4").Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(true);
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill4").Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(false);
        }
        else if (Division.Equals("Skill4Lock"))
        {
            SetUnLock(4);
        }
        else if (Division.Equals("Skill5On"))
        {
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill5").Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(false);
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill5").Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(true);
        }
        else if (Division.Equals("Skill5Off"))
        {
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill5").Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(true);
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill5").Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(false);
        }
        else if (Division.Equals("Skill5Lock"))
        {
            SetUnLock(5);
        }
        */
    }

    // �⺻ �� ����
    public void setDefaultSkin()
    {
        /*
        if (SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOff").gameObject.activeSelf == true
            && SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonOff").gameObject.activeSelf == true
                && SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonOff").gameObject.activeSelf == true
                    && SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonOff").gameObject.activeSelf == true)
        {
            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(false);
        }
        */
    }

    // ��ų UnLock 
    public void SetUnLock(int Index)
    {
        /*
        switch(Index)
        {
            case 1: 
                SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill1").Find("Lock").gameObject.SetActive(false);
                break;
            case 2:
                SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill2").Find("Lock").gameObject.SetActive(false);
                break;
            case 3:
                SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill3").Find("Lock").gameObject.SetActive(false);
                break;
            case 4:
                SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill4").Find("Lock").gameObject.SetActive(false);
                break;
            case 5:
                SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill5").Find("Lock").gameObject.SetActive(false);
                break;
        }
        */
    }

    // �۷ι� ���� ���� 
    public void SetGlobalValue()
    {

    }

    void Start()
    {
        // ������ / ��ų ������ �ν��Ͻ�ȭ
        SetPrefab();

        // �� ��ư �� Ŭ�� �̺�Ʈ ����
        SetButtonEvent();

        SetButtonClickEvent("SkinOn");
    }

    void Update()
    {
        
    }
}
