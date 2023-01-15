using UnityEngine;

public class AppManager : MonoBehaviourSingleton<AppManager>
{
    private void Awake()
    {
        //Screen.SetResolution(1920, 1080, true);

        //Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, true);
        SetResolution();
    }

    public void SetResolution()
    {
        int setWidth = 1920; 
        int setHeight = 1080;

#if UNITY_ANDROID
        int deviceWidth = Screen.width;   // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
#else
        Screen.SetResolution(setWidth, setHeight, true);
        Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, true);
#endif
    }

    void Start()
    {
        Application.targetFrameRate = 144;
    }


    void Update()
    {
        if (GlobalState.Instance.DevMode)
        {
            ShowFrame();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    // On GUI Frame Check
    private float deltaTime = 0.0f;
    private bool isShow = false;
    protected virtual void ShowFrame()
    {
        if (isShow)
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            isShow = !isShow;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Application.targetFrameRate = 30;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Application.targetFrameRate = 60;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Application.targetFrameRate = 144;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Application.targetFrameRate = -1;
        }
    }

    [SerializeField, Range(1, 100)] private int size = 80;
    [SerializeField] private Color color = Color.green;
    private void OnGUI()
    {
        if (isShow)
        {
            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(100, 150, Screen.width, Screen.height);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = size;
            style.normal.textColor = color;

            float ms = deltaTime * 1000f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);

            GUI.Label(rect, text, style);
        }
    }

}
