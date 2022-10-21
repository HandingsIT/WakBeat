using System;
using UnityEngine;
using UnityEngine.UI;

// ��ų �׸� ����
[Serializable]
public class SkillItemInfo
{
    public Sprite TitleSprite;
    public Sprite ExplanationSprite;
    public Sprite IconSprite;
    public Sprite LockExplanationSprite;
}

public class UIObjectShopSkillItem : MonoBehaviour
{
    // ��ų �׸� ����
    [SerializeField] private Image SkillTitleImage;
    [SerializeField] private Image SkillExplanationImage;
    [SerializeField] private Image SkillIconImage;
    [SerializeField] private Image SkillLockExplanationImage;

    // ��ų ���� ����
    public void SetSkill(SkillItemInfo info)
    {
        SkillTitleImage.sprite = info.TitleSprite;
        SkillExplanationImage.sprite = info.ExplanationSprite;
        SkillIconImage.sprite = info.IconSprite;
        SkillIconImage.SetNativeSize();
        SkillLockExplanationImage.sprite = info.LockExplanationSprite; 
    }
}
