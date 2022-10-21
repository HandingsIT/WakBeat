using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectShop : MonoBehaviour
{
    public Button ButtonClose;
    public GameObject TabObject;
    public GameObject SkinGroup;
    public GameObject SkillGroup;
    public GameObject VideoGroup;

    // 각 버튼 별 이벤트 정의
    public void SetButtonEvent()
    {
        // 상점 버튼 이벤트
        ButtonClose.onClick.AddListener(() => SetButtonClickEvent("Close"));
        TabObject.transform.Find("TabSkin").Find("TabOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SkinOn"));
        TabObject.transform.Find("TabSkin").Find("TabOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SkinOff"));
        TabObject.transform.Find("TabSkill").Find("TabOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SkillOn"));
        TabObject.transform.Find("TabSkill").Find("TabOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("SkillOff"));
        TabObject.transform.Find("TabVideo").Find("TabOn").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("VideoOn"));
        TabObject.transform.Find("TabVideo").Find("TabOff").GetComponent<Button>().onClick.AddListener(() => SetButtonClickEvent("VideoOff"));

        // 상점 > 스킨 상점 버튼 이벤트
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

        // 상점 > 스킬 상점 버튼 이벤트
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
        // 상점 > 비디오 상점 버튼 이벤트
    }

    // 각 버튼 별 클릭 이벤트 정의
    public void SetButtonClickEvent(string Division)
    {
        if (Division.Equals("Close"))
        {
            // 입력 되 있는 모든 값 가져와서 글로벌에 저장
            SetGlobalValue();

            // 창 닫기 버튼 이벤트
            UIElementSetting.Instance.ButtonClickControll("Shop", "Close");
        }
        else if (Division.Equals("SkinOn") || Division.Equals("SkinOff"))
        {
            // 상점 > 스킨 버튼 이벤트
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
            // 상점 > 스킬 버튼 이벤트
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
            // 상점 > 비디오 버튼 이벤트
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

    // 기본 공 세팅
    public void setDefaultSkin()
    {
        if (SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOff").gameObject.activeSelf == true
            && SkinGroup.transform.Find("SkinItems").Find("ItemBlue").Find("ItemButtons").Find("ButtonOff").gameObject.activeSelf == true
                && SkinGroup.transform.Find("SkinItems").Find("ItemOrange").Find("ItemButtons").Find("ButtonOff").gameObject.activeSelf == true
                    && SkinGroup.transform.Find("SkinItems").Find("ItemGreen").Find("ItemButtons").Find("ButtonOff").gameObject.activeSelf == true)
        {
            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("ItemBlack").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(false);
        }
    }

    // 스킬 UnLock 
    public void SetUnLock(int Index)
    {
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
    }

    // 글로벌 변수 세팅 
    public void SetGlobalValue()
    {

    }

    void Start()
    {
        // 각 버튼 별 클릭 이벤트 생성
        SetButtonEvent();

        SetButtonClickEvent("SkinOn");
    }

    void Update()
    {
        
    }
}
