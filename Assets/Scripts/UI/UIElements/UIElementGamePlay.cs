using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementGamePlay : MonoBehaviour
{
    private void Awake()
    {
        SoundManager.Instance.ForceAudioStop();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        FinishGame();
        OnClickEsc();
    }

    private void OnEnable()
    {
        SoundManager.Instance.ForceAudioStop();
        GameFactory.Instance.CreateStage();
    }

    private void OnDisable()
    {
        GameFactory.Instance.DistroyStage();
    }

    void FinishGame()
    {
        if (GlobalState.Instance.UserData.data.BackgroundProcActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SoundManager.Instance.ForceAudioStop();

                GameFactory.Instance.DistroyStage();
                UIManager.Instance.GoPanelResult();

                SoundManager.Instance.TurnOnGameBackGround();
            }
        }
    }

    void OnClickEsc()
    {
        if (GlobalState.Instance.UserData.data.BackgroundProcActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameFactory.Instance.DistroyStage();
                UIManager.Instance.GoPanelMusicSelect();
            }
        }
    }
}
