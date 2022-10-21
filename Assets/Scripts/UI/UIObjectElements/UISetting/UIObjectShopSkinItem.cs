using System;
using UnityEngine;
using UnityEngine.UI;

// ��Ų �׸� ����
[Serializable]
public class SkinItemInfo
{
    public Sprite TitleSprite;
    public Sprite IconSprite;
}

public class UIObjectShopSkinItem : MonoBehaviour
{
    // ��Ų �׸� ����
    [SerializeField] private Image SkinTitleImage;
    [SerializeField] private Image SkinIconImage;

    // ��Ų ���� ����
    public void SetSkin(SkinItemInfo info)
    {
        SkinTitleImage.sprite = info.TitleSprite;
        SkinTitleImage.SetNativeSize();
        SkinIconImage.sprite = info.IconSprite;
        SkinIconImage.SetNativeSize();
    }
}
