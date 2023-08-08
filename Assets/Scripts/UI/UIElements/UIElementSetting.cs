using UnityEngine;
using UnityEngine.UI;

public class UIElementSetting : MonoBehaviourSingleton<UIElementSetting>
{
    public GameObject PanelButton;
    public GameObject Background;
    public GameObject PanelSetting;
    public GameObject PanelShop;
    public GameObject PanelTrophy;
    public GameObject PanelPause;

    // ��ư ǥ�� ����
    public void PanelViewController(int index)
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
                ShowSettingPanel(true, 2);
                break;
            case (int)GlobalData.UIMODE.GAME:
                ShowSettingPanel(true, 3);
                break;
            case (int)GlobalData.UIMODE.RESULT:
                ShowSettingPanel(true, 4);
                break;
        }
    }

    // ȭ�� ��ȯ �� ��ư ���� ���� Function ȣ��
    public void ShowSettingPanel(bool isShow, int index)
    {
        // ��ư ���� View ����
        PanelButton.SetActive(isShow);
        // �� ��ư �� View ���� ���� Function
        //PanelButton.GetComponent<UIObjectButton>().buttonViewController(index);
        PanelButton.transform.Find("UIObjectButton").GetComponent<UIObjectButton>().ButtonViewController(index);
    }

    // ��ư Ŭ�� �� ȭ�� ���� �̺�Ʈ
    public void ButtonClickControll(string Division, string OpenYN)
    {
        if(Division.Equals("Setting"))
        {
            if(OpenYN.Equals("Open"))
            {
                Background.SetActive(true);
                PanelSetting.SetActive(true);
            }
            else
            {
                Background.SetActive(false);
                PanelSetting.SetActive(false);
            }
        }
        else if (Division.Equals("Shop"))
        {
            if(OpenYN.Equals("Open"))
            {
                Background.SetActive(true);
                PanelShop.SetActive(true);
            }
            else
            {
                Background.SetActive(false);
                PanelShop.SetActive(false);
            }
        }
        else if (Division.Equals("Trophy"))
        {
            if (OpenYN.Equals("Open"))
            {
                Background.SetActive(true);
                PanelTrophy.SetActive(true);
            }
            else
            {
                Background.SetActive(false);
                PanelTrophy.SetActive(false);
            }
        }
        else if (Division.Equals("Pause"))
        {
            if (OpenYN.Equals("Open"))
            {
                DataManager.dataBackgroundProcActive = false;

                Background.SetActive(true);
                PanelPause.SetActive(true);

                PanelButton.transform.Find("UIObjectButton").Find("ButtonPause").GetComponent<Button>().interactable = false;

                // ���� �Ͻ� ���� ���μ��� > ����
                Stage.Instance.OnClickPause();
            }
            else
            {
                DataManager.dataBackgroundProcActive = true;

                Background.SetActive(false);
                PanelPause.SetActive(false);

                PanelButton.transform.Find("UIObjectButton").Find("ButtonPause").GetComponent<Button>().interactable = true;

                // ���� �Ͻ� ���� ���μ��� > ����
                Stage.Instance.OnClickPause();
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
}