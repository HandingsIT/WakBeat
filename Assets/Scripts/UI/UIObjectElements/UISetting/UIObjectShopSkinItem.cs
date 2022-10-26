using System;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectShopSkinItem : MonoBehaviour
{
    // ���� ������Ʈ ����
    public Button buttonOn;
    public Button buttonOff;
    public Button buttonLock;

    public UIObjectShop UIObjectShop { get; set; }

    // ��Ų �׸� ���� > Ÿ��Ʋ
    public Sprite _TitleSprite;
    [SerializeField] private Image SkinTitleImage;
    public Sprite SkinTitleSprite
    {
        get { return _TitleSprite; }
        set
        {
            _TitleSprite = value;
            SkinTitleImage.sprite = _TitleSprite;
            SkinTitleImage.SetNativeSize();
        }
    }

    // ��Ų �׸� ���� > ������
    public Sprite _IconSprite;
    [SerializeField] private Image SkinIconImage;
    public Sprite SkinIconSprite
    {
        get { return _IconSprite; }
        set
        {
            _IconSprite = value;
            SkinIconImage.sprite = _IconSprite;
            SkinIconImage.SetNativeSize();
        }
    }

    // ��Ų �׸� ���� > �ε���
    private int _SkinIndex;
    public int SkinIndex
    {
        get { return _SkinIndex; }
        set
        {
            _SkinIndex = value;
        }
    }

    // ��ư �̺�Ʈ ����
    public void SetButtonEvent()
    {
        buttonOn.onClick.AddListener(On);
        buttonOff.onClick.AddListener(Off);
        buttonLock.onClick.AddListener(Unlock);
    }

    // ��ų ���
    private void Unlock()
    {
        buttonLock.gameObject.SetActive(false);
    }

    // ��ų ��� > �̻��
    private void On()
    {
        buttonOn.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(true);
    }

    // ��ų �̻�� > ���
    private void Off()
    {
        buttonOn.gameObject.SetActive(true);
        buttonOff.gameObject.SetActive(false);
    }
}
