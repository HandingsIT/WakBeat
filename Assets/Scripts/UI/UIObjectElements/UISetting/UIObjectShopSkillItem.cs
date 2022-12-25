using System;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectShopSkillItem : MonoBehaviour
{
    // ���� ������Ʈ ����
    public Button buttonOn;
    public Button buttonOff;
    public Button buttonLock;
    public GameObject LockObject;

    // ��ư ���� --> Global Data���� �����ͼ� ���� ������� ����
    //const int SFX_Home = 1;
    //const int SFX_Setting = 4;

    public UIObjectShop UIObjectShop { get; set; }

    // ��ų ���� > Ÿ��Ʋ
    public Sprite _TitleSprite;
    [SerializeField] private Image SkillTitleImage;
    public Sprite SkillTitleSprite
    {
        get { return _TitleSprite; }
        set
        {
            _TitleSprite = value;
            SkillTitleImage.sprite = _TitleSprite;
            SkillTitleImage.SetNativeSize();
        }
    }

    // ��ų ���� > ����
    public Sprite _ExplanationSprite;
    [SerializeField] private Image SkillExplanationImage;
    public Sprite SkillExplanationSprite
    {
        get { return _ExplanationSprite; }
        set
        {
            _ExplanationSprite = value;
            SkillExplanationImage.sprite = _ExplanationSprite;
            SkillExplanationImage.SetNativeSize();
        }
    }

    // ��ų ���� > ������
    public Sprite _IconSprite;
    [SerializeField] private Image SkillIconImage;
    public Sprite SkillIconSprite
    {
        get { return _IconSprite; }
        set
        {
            _IconSprite = value;
            SkillIconImage.sprite = _IconSprite;
            SkillIconImage.SetNativeSize();
        }
    }

    // ��ų ���� > ��� > ����
    public Sprite _LockExplanationSprite;
    [SerializeField] private Image SkillLockExplanationImage;
    public Sprite SkillLockExplanationSprite
    {
        get { return _LockExplanationSprite; }
        set
        {
            _LockExplanationSprite = value;
            SkillLockExplanationImage.sprite = _LockExplanationSprite;
            SkillLockExplanationImage.SetNativeSize();
        }
    }

    // ��Ų �׸� ���� > �ε���
    private int _SkillIndex;
    public int SkillIndex
    {
        get { return _SkillIndex; }
        set
        {
            _SkillIndex = value;
        }
    }

    // ��ư �̺�Ʈ ���� > 2022.12.24 : �ܼ� ó���� ���� Shop.cs ���� �̺�Ʈ ó��
    public void SetButtonEvent()
    {
        buttonOn.onClick.AddListener(On);
        buttonOff.onClick.AddListener(Off);
        buttonLock.onClick.AddListener(Unlock);
    }

    // ��ų ��� > 2022.12.24 : �ܼ� ó���� ���� Shop.cs ���� �̺�Ʈ ó��
    private void Unlock()
    {
        LockObject.SetActive(false);

        // ��ư ���� ���
        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    }

    // ��ų ��� > �̻�� > 2022.12.24 : �ܼ� ó���� ���� Shop.cs ���� �̺�Ʈ ó��
    private void On()
    {
        buttonOn.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(true);

        // ��ư ���� ���
        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    }

    // ��ų �̻�� > ��� > 2022.12.24 : �ܼ� ó���� ���� Shop.cs ���� �̺�Ʈ ó��
    private void Off()
    {
        buttonOn.gameObject.SetActive(true);
        buttonOff.gameObject.SetActive(false);

        // ��ư ���� ���
        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    }
}
