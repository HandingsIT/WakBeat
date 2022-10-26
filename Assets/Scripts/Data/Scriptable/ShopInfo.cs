using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetupShopInfo", menuName = "ScriptableObjects/SetupShopInfo", order = 3)]

public class ShopInfo : ScriptableObject
{
    [Space(10)]
    [Header("[ Shop Skin Info]")]
    public Sprite[] SkinTitle;
    public Sprite[] SkinIcon;

    [Space(10)]
    [Header("[ Shop Skill Info]")]
    public Sprite[] SkillTitle;
    public Sprite[] SkillExplanation;
    public Sprite[] SkillIcon;
    public Sprite[] SkillLockExplanation;

    /*
    [Header("[ Shop Info List (Prefabs) ]")]
    public List<SkillItemInfo> SkillItemInfos;
    public List<SkinItemInfo> SkinItemInfos;
    */
    /*
    [Header("[ Shop List (Prefabs) ]")]
    public GameObject[] SkinLists;
    public GameObject[] SkillLists;

    [Space(10)]
    [Header("[ Skin Infomation ]")]
    public Sprite[] SkinBackgournds;  // ��Ų �׸� ���
    public Sprite[] SkinTitles;               // ��Ų ��
    public Sprite[] SkinIcons;               // ��Ų ������

    [Space(10)]
    [Header("[ Skin Button Infomation ]")]
    public Sprite[] SkinButtonOns;      // ��Ų On ��ư
    public Sprite[] SkinButtonOffs;     // ��Ų Off ��ư
    public Sprite[] SkinButtonBuys;     // ��Ų Buy ��ư

    [Space(10)]
    [Header("[ Skill Open Infomation ]")]
    public Sprite[] SkillOpenBackgournds;       // Open �� ��ų ���
    public Sprite[] SkillOpenTitles;        // ��ų ��
    public Sprite[] SkillOpenTexts;        // ��ų ����
    public Sprite[] SkillOpenIcons;        // ��ų ������

    [Space(10)]
    [Header("[ Skill Open Button Infomation ]")]
    public Sprite[] SkillOpenButtonOns;        // ��ų On ��ư
    public Sprite[] SkillOpenButtonOffs;        // ��ų Off ��ư

    [Space(10)]
    [Header("[ Skill Lock Infomation ]")]
    public Sprite[] SkillLockBackgournds;       // Lock �� ��ų ���
    public Sprite[] SkillLockTexts;        // ��ų ��� ����
    public Sprite[] SkillLockIcons;        // ��ų ��� ������

    [Space(10)]
    [Header("[ Skill Lock Button Infomation ]")]
    public Sprite[] SkillLockButtonUnlocks;        // ��ų Unlock ��ư
    */
}
