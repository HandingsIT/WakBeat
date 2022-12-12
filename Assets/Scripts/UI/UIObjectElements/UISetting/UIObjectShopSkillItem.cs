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
        LockObject.SetActive(false);

        // ��ư ���� ���
        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    }

    // ��ų ��� > �̻��
    private void On()
    {
        buttonOn.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(true);

        switch (SkillIndex)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                GlobalState.Instance.ShowDodge = false;
                break;
            case 4:
                GlobalState.Instance.AutoMode = false;
                Debug.Log(GlobalState.Instance.AutoMode);
                break;
        }

        // ��ư ���� ���
        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    }

    // ��ų �̻�� > ���
    private void Off()
    {
        buttonOn.gameObject.SetActive(true);
        buttonOff.gameObject.SetActive(false);

        switch (SkillIndex)
        {
            case 0:
            case 1:
            case 2:
                break;
            case 3:
                GlobalState.Instance.ShowDodge = true;
                break;
            case 4:
                GlobalState.Instance.AutoMode = true;
                Debug.Log(GlobalState.Instance.AutoMode); 
                break;
        }

        // ��ư ���� ���
        SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingIn);
    }
}
