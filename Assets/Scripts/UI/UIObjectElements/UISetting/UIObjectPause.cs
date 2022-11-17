using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectPause : MonoBehaviour
{
    public Button ButtonBack;
    public Button ButtonRestart;
    public Button ButtonHome;

    // �˾�â ȣ�� �� UI ����
    private float _duration = 0.15f;

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
        if (Division.Equals("Back")) 
        { 
            // �ڷ� ���� > ���� �̾� �ϱ� > 3�� �ʿ� ����
        }
        else if (Division.Equals("Restart"))
        {
            // ����� �ϱ� > ���� �ʱ�ȭ
        }
        else if (Division.Equals("Home"))
        {
            // Ȩ���� �̵� (���� ���� ȭ������ �̵�) > �� ���൵ ���� �۷ι� ������ ����
        }

        // ��ư �̺�Ʈ Unlock
        GlobalState.Instance.UserData.data.BackgroundProcActive = true;

        // â �ݱ� ��ư �̺�Ʈ
        UIElementSetting.Instance.ButtonClickControll("Pause", "Close");
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
        // ��ư �̺�Ʈ ����
        SetButtonEvent();
    }
}
