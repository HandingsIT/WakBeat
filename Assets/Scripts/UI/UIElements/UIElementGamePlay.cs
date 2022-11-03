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
        GameFactory.Instance.CreateGame();
    }

    //private void OnDisable()
    //{
    //    GameManager.Instance.DistroyGame();
    //}

    void FinishGame()
    {
        if (GlobalState.Instance.UserData.data.BackgroundProcActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameFactory.Instance.DistroyGame();
                UIManager.Instance.GoPanelResult();
            }
        }
    }

    void OnClickEsc()
    {
        if (GlobalState.Instance.UserData.data.BackgroundProcActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameFactory.Instance.DistroyGame();
                UIManager.Instance.GoPanelMusicSelect();
            }
        }
    }
}
