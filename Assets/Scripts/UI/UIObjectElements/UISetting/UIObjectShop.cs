using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Mono.Cecil;

public class UIObjectShop : MonoBehaviour
{
    /*
    [SerializeField] private ShopInfo ShopInfo;
    */
    public Button ButtonClose;
    public GameObject TabObject;
    private int SkinCount = DataManager.dataSkinCount;
    private int SkillCount = DataManager.dataSkillCount;
    // �˾�â ȣ�� �� UI ����
    private float _duration = 0.15f;
    // ��ư ����
    const int SFX_Home = 1;
    const int SFX_Setting = 4;
    // ��Ų ������ ������Ʈ
    public GameObject SkinGroup;
    [SerializeField] private GameObject SkinPrefab;
    [SerializeField] private Transform SkinPanel;
    // ��ų ������ ������Ʈ
    public GameObject SkillGroup;
    [SerializeField] private GameObject SkillPrefab;
    [SerializeField] private Transform SkillPanel;
    // ���� ������ ������Ʈ
    public GameObject VideoGroup;
    // �۷ι� ������ �� ��������
    public string[] _SkinUnLockYn = new string[DataManager.dataSkinCount];
    public string[] _SkinUsingYn = new string[DataManager.dataSkinCount];
    public string[] _SkillUnLockYn = new string[DataManager.dataSkillCount];
    public string[] _SkillUsingYn = new string[DataManager.dataSkillCount];

    private Boolean isStart = false;

    public void SetPrefab()
    {
        // ��Ų ������ ��������
        var SkinTitle = GlobalData.Instance.Shop.SkinTitle;
        var SkinIcon = GlobalData.Instance.Shop.SkinIcon;

        // ��ų ������ ��������
        var SkillTitle = GlobalData.Instance.Shop.SkillTitle;
        var SkillIcon = GlobalData.Instance.Shop.SkillIcon;
        var SkillExplanation = GlobalData.Instance.Shop.SkillExplanation;
        var SkillLockExplanation = GlobalData.Instance.Shop.SkillLockExplanation;

        // ��Ų ������ ����
        for(int SkinIndex = 0; SkinIndex < SkinTitle.Length; SkinIndex++)
        {
            var _Skin = (GameObject)Instantiate(SkinPrefab, SkinPanel);
            var SkinInfo = _Skin.GetComponent<UIObjectShopSkinItem>();

            if (SkinInfo)
            {
                if (SkinTitle.Length == SkinIcon.Length)
                {
                    SkinInfo.name = "Skin_Prefab_" + SkinIndex;
                    SkinInfo.SkinTitleSprite = SkinTitle[SkinIndex];
                    SkinInfo.SkinIconSprite = SkinIcon[SkinIndex];
                    SkinInfo.SkinIndex = SkinIndex;
                    // ��Ų ��ư �̺�Ʈ ȣ��
                    //SkinInfo.SetButtonEvent();
                    SkinInfo.gameObject.SetActive(true);
                }
            }
        }

        // ��ų ������ ����
        for(int SkillIndex = 0; SkillIndex < SkillTitle.Length; SkillIndex++)
        {
            var _Skill = (GameObject)Instantiate(SkillPrefab, SkillPanel);
            var SkillInfo = _Skill.GetComponent<UIObjectShopSkillItem>();

            if (SkillInfo)
            {
                if(SkillTitle.Length == SkillIcon.Length 
                    && SkillTitle.Length == SkillExplanation.Length 
                        && SkillTitle.Length == SkillLockExplanation.Length)
                {
                    SkillInfo.name = "Skill_Prefab_" + SkillIndex;
                    SkillInfo.SkillTitleSprite = SkillTitle[SkillIndex];
                    SkillInfo.SkillIconSprite = SkillIcon[SkillIndex];
                    SkillInfo.SkillExplanationSprite = SkillExplanation[SkillIndex];
                    SkillInfo.SkillLockExplanationSprite = SkillLockExplanation[SkillIndex];
                    SkillInfo.SkillIndex = SkillIndex;
                    // ��ų ��ư �̺�Ʈ ȣ��
                    //SkillInfo.SetButtonEvent();
                    SkillInfo.gameObject.SetActive(true);
                }
            }
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

        // ��Ų ��ư �̺�Ʈ > ��ų ��ư�� �����ڿ� �� ���� > ��Ų�� �ϳ��� On �Ǿ� �ϴ� ���� ������ ���ʿ��� �ۼ�
        for(int Index = 0; Index < SkinCount; Index++)
        {
            var Prefab = SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).GetComponent<UIObjectShopSkinItem>();
            int PrefabIndex = Prefab.SkinIndex;

            SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonBuy").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(PrefabIndex, "Buy"));
            SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(PrefabIndex, "On"));
            SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetSkinButtonEvent(PrefabIndex, "Off"));
        }

        // 2022.12.24 > ��ų ��뵵 �ܼ��� �������� ���� ��ų�� �ϳ��� On �Ǿ�� �ϹǷ� ���ʿ��� �ۼ�
        for(int Index = 0; Index < SkillCount; Index++)
        {
            var Prefab = SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).GetComponent<UIObjectShopSkillItem>();
            int PrefabIndex = Prefab.SkillIndex;

            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOn").GetComponent<Button>().onClick.AddListener(() => SetSkillButtonEvent(PrefabIndex, "On"));
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOff").GetComponent<Button>().onClick.AddListener(() => SetSkillButtonEvent(PrefabIndex, "Off"));
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Lock").Find("SkillButton").Find("ButtonLock").GetComponent<Button>().onClick.AddListener(() => SetSkillButtonEvent(PrefabIndex, "Lock"));
        }
    }

    // �� ��ư �� Ŭ�� �̺�Ʈ ����
    public void SetButtonClickEvent(string Division)
    {
        if (Division.Equals("Close"))
        {
            // �Է� �� �ִ� ��� �� �����ͼ� �۷ι��� ����
            SetGlobalValue();

            // ���� â �ʱ�ȭ
            SetButtonClickEvent("SkinOn");

            // ��ư �̺�Ʈ Unlock
            DataManager.dataBackgroundProcActive = true;

            // â �ݱ� ��ư �̺�Ʈ
            UIElementSetting.Instance.ButtonClickControll("Shop", "Close");

            // ��ư ���� ���
            setSoundPrint();
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

            // ��ư ���� ���
            setSoundPrint();
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

            // ��ư ���� ���
            setSoundPrint();
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

            // ��ư ���� ���
            setSoundPrint();
        }
    }

    // ��ư ���� ���
    public void setSoundPrint()
    {
        if (!isStart)
        {
            SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
        }
    }

    // ���� > ��Ų > �� ��ư �� �̺�Ʈ ����
    public void SetSkinButtonEvent(int Index, string Division)
    {
        if (Division.Equals("Buy"))
        {
            // �ر� �̱��� > ���� �ʿ� �������� �� ����� ��ư Active Ȱ��ȭ
            SetUnLockSkin(Index);
        }
        else if (Division.Equals("On"))
        {
            // ��� > �̻��
            SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
            SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
        }
        else if (Division.Equals("Off"))
        {
            // �̻�� > ��� > ��ȯ �� ������ ��Ų �̻������ ��ȯ
            for(int i = 0; i < SkinCount; i++)
            {
                if(i != Index)
                {
                    SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + i).Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(false);
                    SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + i).Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(true);
                } 
                else
                {
                    SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + i).Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(true);
                    SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + i).Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(false);
                }
            }
        }

        // ��� Off �϶� 0�� ��Ų On
        SetSkinButtonDefault();

        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    }

    // ��Ų ��ư ��� Off �϶� 0�� ��Ų On
    public void SetSkinButtonDefault()
    {
        Boolean flag = false;
        for (int i = 0; i < SkinCount; i++)
        {
            if (SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + i).Find("ItemButtons").Find("ButtonOn").gameObject.activeSelf
                && !SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + i).Find("ItemButtons").Find("ButtonOff").gameObject.activeSelf)
            {
                flag = true;
                break;
            }
        }

        if (!flag)
        {
            SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_0").Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(true);
            SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_0").Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(false);
        }
    }

    // ��Ų ��� ����
    public void SetUnLockSkin(int Index)
    {
        SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonBuy").gameObject.SetActive(false);
    }

    // ���� > ��ų > �� ��ư �� �̺�Ʈ ����
    public void SetSkillButtonEvent(int Index, string Division)
    {
        if (Division.Equals("Lock"))
        {
            SetUnLockSkill(Index);
        }
        else if (Division.Equals("On"))
        {
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(false);
            SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(true);

            switch(Index)
            {
                case 0: GlobalState.Instance.UseBonusHP = false; break;
                case 1: GlobalState.Instance.UseBarrier = false; break;
                case 2: GlobalState.Instance.UseNewGaMe = false; break;
                case 3: GlobalState.Instance.ShowDodge = false; break;
                case 4: GlobalState.Instance.AutoMode = false; break;
                default: break;
            }

            GlobalState.Instance.UsedItems = "����";
        }
        else if (Division.Equals("Off"))
        {
            // �̻�� > ��� > ��ȯ �� ������ ��Ų �̻������ ��ȯ
            for (int i = 0; i < SkillCount; i++)
            {
                if (i != Index)
                {
                    SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + i).Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(false);
                    SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + i).Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(true);
                }
                else
                {
                    SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + i).Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(true);
                    SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + i).Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(false);

                    // �� ���ӿ��� ��� �� �� ��� ��ų �� ����
                    string _SkillName = "";
                    switch(i)
                    {
                        case 0: 
                            _SkillName = "����";
                            GlobalState.Instance.UseBonusHP = true;
                            break;
                        case 1: 
                            _SkillName = "�Ͻù���";
                            GlobalState.Instance.UseBarrier = true;
                            break;
                        case 2: 
                            _SkillName = "������";
                            GlobalState.Instance.UseNewGaMe = true;
                            break;
                        case 3: 
                            _SkillName = "�м���";
                            GlobalState.Instance.ShowDodge = true;
                            break;
                        case 4: 
                            _SkillName = "��������";
                            GlobalState.Instance.AutoMode = true;
                            break;
                        default: _SkillName = "����"; break;

                    }
                    GlobalState.Instance.UsedItems = _SkillName;
                }
            }
        }

        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    }

    // ��ų ��� ����
    public void SetUnLockSkill(int Index)
    {
        SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Lock").gameObject.SetActive(false);
    }

    // Ŭ���� �������� ���� ���� Ȥ�� ��� ������ ��ų ��� ���� ��Ȱ��ȭ
    public void SetUnLockSkill()
    {
        for (int Index = 0; Index < SkillCount; Index++)
        {
            if (DataManager.dataClearStageCount >= DataManager.dataSkillUnLockCondition[Index] || _SkillUnLockYn[Index].Equals("Y"))
            {
                SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Lock").gameObject.SetActive(false);
            }
        }
    }

    // �۷ι� ���� ȭ�� ����
    public void GetGlobalValue()
    {
        Boolean _ShopCompulsionActive = DataManager.dataShopCompulsionActive;

        // ���� �ر� On Off ó�� > True : ���� �ر� ��� / False : ���� �ر� �̻��
        if (_ShopCompulsionActive)
        {
            // ��Ų�� ���� ����
            for (int Index = 0; Index < SkinCount; Index++)
            {
                SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonBuy").gameObject.SetActive(false);
            }

            // ��ų ���� �ر� �̻�� ó��
            for (int Index = 0; Index < SkillCount; Index++)
            {
                SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Lock").Find("SkillButton").Find("ButtonLock").gameObject.SetActive(false);
            }
        } 

        // �������� Ŭ���� ��
        int _ClearStageCount = (int)DataManager.dataClearStageCount;
        // ��Ų �۷ι� ����
        _SkinUnLockYn = DataManager.dataSkinUnLockYn;
        _SkinUsingYn = DataManager.dataSkinUsingYn;
        // ��ų �۷ι� ����
        _SkillUnLockYn = DataManager.dataSkillUnLockYn;
        _SkillUsingYn = DataManager.dataSkillUsingYn;

        // �̹� ������ ��Ų�� ��� ��ư ��Ȱ��ȭ
        for(int Index = 0; Index < SkinCount; Index++)
        {
            if (_SkinUnLockYn[Index].Equals("Y"))
            {
                SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonBuy").gameObject.SetActive(false);
            }
        }

        // ��ų �ر� ����
        SetUnLockSkill();

        // ��Ų ��� �ݿ�
        for (int Index = 0; Index < SkinCount; Index++)
        {
            if (null != _SkinUsingYn[Index] && _SkinUsingYn[Index].Equals("Y"))
            {
                SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOn").gameObject.SetActive(true);
                SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOff").gameObject.SetActive(false);
            }
        }

        // ��ų ��� �ݿ�
        for (int Index = 0; Index < SkillCount; Index++)
        {
            if (null != _SkillUsingYn && _SkillUsingYn[Index].Equals("Y"))
            {
                SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.SetActive(true);
                SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.SetActive(false);
            }
        }

        // ��ŸƮ ���� > true > ù ȣ�� �� ��ư ���� �����
        isStart = true;

        // ���� ���� �� Ȱ��ȭ
        SetButtonClickEvent("SkinOn");

        // ��ŸƮ ���� > false > ���� ���
        isStart = false;
    }

    // �۷ι� ���� ����
    public void SetGlobalValue()
    {
        if (_SkinUnLockYn.Length <= 0) { _SkinUnLockYn = new string[DataManager.dataSkinCount]; }
        if (_SkinUsingYn.Length <= 0) { _SkinUsingYn = new string[DataManager.dataSkinCount]; }
        if (_SkillUnLockYn.Length <= 0) { _SkillUnLockYn = new string[DataManager.dataSkillCount]; }
        if (_SkillUsingYn.Length <= 0) { _SkillUsingYn = new string[DataManager.dataSkillCount]; }

        // �۷ι� ���� �� ��Ų ���� ����
        for (int Index = 0; Index < DataManager.dataSkinCount; Index++)
        {
            // ��Ų�� ���� �ر� ���� �ȵ�����
            if(SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonBuy").gameObject.activeSelf)
            {
                _SkinUnLockYn[Index] = "N";
            } 
            else
            {
                _SkinUnLockYn[Index] = "Y";
            }

            // ��Ų ��� ����
            if (SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOn").gameObject.activeSelf
                && !SkinGroup.transform.Find("SkinItems").Find("Viewport").Find("Content").Find("Skin_Prefab_" + Index).Find("ItemButtons").Find("ButtonOff").gameObject.activeSelf)
            {
                _SkinUsingYn[Index] = "Y";
            }
            else
            {
                _SkinUsingYn[Index] = "N";
            }
        }

        // �۷ι� ���� �� ��ų ���� ����
        for (int Index = 0; Index < DataManager.dataSkillCount; Index++)
        {
            // ��ų ��� ���� ����
            if(SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Lock").gameObject.activeSelf)
            {
                _SkillUnLockYn[Index] = "N";
            }
            else
            {
                _SkillUnLockYn[Index] = "Y";
            }
            
            // ��ų ��� ����
            if(SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOn").gameObject.activeSelf
                && !SkillGroup.transform.Find("SkillItems").Find("Viewport").Find("Content").Find("Skill_Prefab_" + Index).Find("Open").Find("SkillButton").Find("ButtonOff").gameObject.activeSelf)
            {
                _SkillUsingYn[Index] = "Y";
            }
            else
            {
                _SkillUsingYn[Index] = "N";
            }
        }

        // ���� ������ ����
        DataManager.dataSkinUnLockYn = _SkinUnLockYn;
        DataManager.dataSkinUsingYn = _SkinUsingYn;
        DataManager.dataSkillUnLockYn = _SkillUnLockYn;
        DataManager.dataSkillUsingYn = _SkillUsingYn;

        // ���� ������ ���� �� ���� ����
        DataManager.SaveUserData();
    }

    // �˾� â ȣ�� �� UI ��� ����
    private void OnEnable()
    {
        // ������ 0���� ����
        this.transform.localScale = Vector3.zero;
        // 1���� Ŀ���鼭 �ð��� 0.2��, ��ȯ �� ť�� ���·� ����
        this.transform.DOScale(Vector3.one, _duration).SetEase(Ease.InCubic);
    }

    void Start()
    {
        // ��Ų / ��ų ������ �ν��Ͻ�ȭ
        SetPrefab();

        // ��ư �̺�Ʈ ����
        SetButtonEvent();

        // �۷ι� ���� ȭ�� ����
        GetGlobalValue();
    }

    void Update()
    {
        // ��Ų �ر� ����


        // ��ų �ر� ����
        SetUnLockSkill();
    }
}
