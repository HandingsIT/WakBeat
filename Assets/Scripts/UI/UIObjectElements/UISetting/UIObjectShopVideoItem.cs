using System;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectShopVideoItem : MonoBehaviour
{
    public Button buttonPlay;

    public UIObjectShop UIObjectShop { get; set; }

    // ��Ų �׸� ���� > Ÿ��Ʋ
    public Sprite _TitleSprite;
    [SerializeField] private Image VideoTitleImage;
    public Sprite VideoTitleSprite
    {
        get { return _TitleSprite; }
        set
        {
            _TitleSprite = value;
            VideoTitleImage.sprite = _TitleSprite;
            VideoTitleImage.SetNativeSize();
        }
    }

    // ��Ų �׸� ���� > ������
    public Sprite _IconSprite;
    [SerializeField] private Image VideoIconImage;
    public Sprite VideoIconSprite
    {
        get { return _IconSprite; }
        set
        {
            _IconSprite = value;
            VideoIconImage.sprite = _IconSprite;
            VideoIconImage.SetNativeSize();
        }
    }

    // ��Ų �׸� ���� > �ε���
    private int _VideoIndex;
    public int VideoIndex
    {
        get { return _VideoIndex; }
        set
        {
            _VideoIndex = value;
        }
    }
}
