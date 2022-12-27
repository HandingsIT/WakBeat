using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectPause : MonoBehaviour
{
    public Button ButtonBack;
    public Button ButtonRestart;
    public Button ButtonHome;

    [SerializeField] private GameObject _PopupMusicInfo;
    [SerializeField] private UIElementPopUp UIElementPopUp;

    //private Boolean _isDestroy = false;

    // �˾�â ȣ�� �� UI ����
    //private float _duration = 0.15f;

    // ��ư �̺�Ʈ ����
    public void SetButtonEvent()
    {
        ButtonBack.onClick.AddListener(() => { SetButtonClickEvent("Back"); });
        ButtonRestart.onClick.AddListener(() => { SetButtonClickEvent("Restart"); });
        ButtonHome.onClick.AddListener(() => { SetButtonClickEvent("Home"); });
    }

    // ��ư �̺�Ʈ ó��
    public void SetButtonClickEvent(string Division)
    {
        // ��ư �̺�Ʈ Unlock        //���ó���� UIElementSetting.Instance.ButtonClickControll ���⼭ ����
        //DataManager.dataBackgroundProcActive = true;

        // â �ݱ� ��ư �̺�Ʈ
        UIElementSetting.Instance.ButtonClickControll("Pause", "Close"); 

        if (Division.Equals("Back")) 
        {
            // �ڷ� ���� > ���� �̾� �ϱ� > 3�� �ʿ� ����
        }
        else if (Division.Equals("Restart"))
        {
            // ����� �ϱ� > ���� �ʱ�ȭ
            // **** �ʱ�ȭ ���μ��� �ۼ�  

            //�ܼ� ��Ʈ������ �ٽ� ����
            GameFactory.Instance.DistroyStage();
            GameFactory.Instance.CreateStage();

            // Music Info Restart           

            // **** ���� ���� : ���� Ŭ�� �� Destroy�� ���� �ȵ�
            DestroyPopUp();

            //UIManager.Instance.UIElementPopUp.SetPopUpMusicInfo();  // DoTween Ȥ�� �ν��Ͻ� ��� ���� �ʿ�

            //_isDestroy = true;
            //UIElementPopUp.SetPopUpMusicInfo();
        }
        else if (Division.Equals("Home"))
        {
            // Ȩ���� �̵� (���� ���� ȭ������ �̵�) > �� ���൵ ���� �۷ι� ������ ����
            // **** Ȩ���� �̵� �� �۷ι� ������ ���� ���μ��� �ۼ�
            
            Stage.Instance.GoBackSelectStage();

            // Music Info Destroy
            DestroyPopUp();
        }
    }

    // �˾� â ȣ�� �� UI ��� ����
    private void OnEnable()
    {
        /*
        // 2022.12.12 > Stage�� OnClickPause ���� �� Animation �������� ���� Dotween �۵� ����
        // ������ 0���� ����
        this.transform.localScale = Vector3.zero;
        // 1���� Ŀ���鼭 �ð��� 0.2��, ��ȯ �� ť�� ���·� ����
        this.transform.DOScale(Vector3.one, _duration).SetEase(Ease.InCubic);
        */
    }

    public void OnClickContinue()
    {
        OnClickButtonEvent();
    }

    public void OnClickRestart()
    {
        DestroyPopUp();
        OnClickButtonEvent();

        GameFactory.Instance.DistroyStage();
        GameFactory.Instance.CreateStage();

    }

    public void OnClickGoHome()
    {
        DestroyPopUp();
        OnClickButtonEvent();

        Stage.Instance.GoBackSelectStage();
    }

    void OnClickButtonEvent()
    {
        Stage.Instance.OnClickPause();
        UIElementSetting.Instance.ButtonClickControll("Pause", "Close");
    }

    void DestroyPopUp()
    {
        var popUp = GameObject.Find(_PopupMusicInfo.name + "(Clone)");

        if (popUp)
        {
            Destroy(popUp);
        }
    }

    void Start()
    {
        // ��ư �̺�Ʈ ����
        SetButtonEvent();
    }

    /*
    public void OnDestroy()
    {
        Debug.Log(">>>>>>>>>>>>>> OnDestroy : " + _isDestroy);
        if(_isDestroy)
        {
            UIElementPopUp.SetPopUpMusicInfo();
        }
    }
    */
}
