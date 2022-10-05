using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UISetting : MonoBehaviour
{
    public GameObject PanelButton;

    // ��ư ǥ�� ����
    public void buttonViewController(int index)
    {
        // Function Parameter
        // 1. true/false : ��ư ���� ��ü�� ���� �� ������
        // 2. Index : �� ��ư�� �������� ���� ����
        //     Index Detail : 0 > ���� ȭ�� ��ư ����
        //                            1 > �ٹ� ���� �� �� ���� ��ư ����
        //                            2 > �ΰ��� ȭ�� ��ư ����
        //                            3 > ��� ȭ�� ��ư ����
        //                            9 > ��ü ��ư ����� (ex. ��Ʈ�� ȭ�� ��)
        switch (index)
        {
            case (int)GlobalData.UIMODE.INTRO:
                ShowSettingPanel(false, 9);
                break;
            case (int)GlobalData.UIMODE.MAIN:
                ShowSettingPanel(true, 0);
                break;
            case (int)GlobalData.UIMODE.SELECT_ALBUM:
                ShowSettingPanel(true, 1);
                break;
            case (int)GlobalData.UIMODE.SELECT_MUSIC:
                ShowSettingPanel(true, 1);
                break;
            case (int)GlobalData.UIMODE.GAME:
                ShowSettingPanel(true, 2);
                break;
            case (int)GlobalData.UIMODE.RESULT:
                ShowSettingPanel(true, 3);
                break;
        }
    }

    // ȭ�� ��ȯ �� ��ư ���� ���� Function ȣ��
    public void ShowSettingPanel(bool isShow, int index)
    {
        // ��ư ���� View ����
        PanelButton.SetActive(isShow);
        // �� ��ư �� View ���� ���� Function
        PanelButton.GetComponent<UIObjectButton>().buttonViewController(index);
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
}