using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StageCharting : Stage
{
    [Space(10)]
    [Header ("----- [Charting Componenet] -----")]

    public TMP_Dropdown DropDownAlbum;
    public TMP_Dropdown DropDownStage;
    public TMP_Text TextGameMode;
    public TMP_Text TextAutoMode;

    public TMP_Text TextPause;

    public GameObject DebugTab;
    public TMP_Text TextDebug;

    public GameObject SpawnPoint;
    public Transform SpawnPointBase;
    public TMP_Text TextSpawnPoint;

    public GameObject HalfAngle;
    public TMP_Text TextHalfAngle;

    public Image InCircleFade;
    public Image OutCircleFade;


    private CircleCollider2D playerCollider;

    private bool isplay = false;
    private bool _isGameMode = false;
    private bool _isShowDebugtab = true;

    //------------ For Debuging -----------
    public TMP_Text[] DebugElements;

    private const int Title = 0;
    private const int Artist = 1;
    private const int BPM = 2;
    private const int Bar = 3;
    private const int TickTime = 4;
    private const int LineTime = 5;
    private const int BallSpeed = 6;
    private const int BallAngle = 7;
    private const int SongTotalTime = 8;
    private const int CurrentSongTime = 9;
    private const int CurrentLine = 10;
    private const int TotalLine = 11;
 

    protected override void Init()
    {
        base.Init();

        CreateSpawnPoint();

        _isGameMode = Config.Instance.GameMode;
        _isAutoMode = Config.Instance.AutoMode;

        GlobalState.Instance.SavePointAngle = bmwReader.ChartingItem[0].BallAngle;

        if (GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            rigid.simulated = _isGameMode;
        }

        if (this.GetComponent<CircleCollider2D>() != null)
        {
            playerCollider = this.GetComponent<CircleCollider2D>();
            playerCollider.offset = new Vector2(Ball.transform.localPosition.x, Ball.transform.localPosition.y);
        }

        TextGameMode.text = $"Game Mode \n {_isGameMode}";
        TextAutoMode.text = $"Auto Mode \n {_isAutoMode}";
    }

    private void CreateSpawnPoint()
    {
        Center.transform.localEulerAngles = Vector3.zero;

        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject point = GameObject.Instantiate(SpawnPoint, SpawnPointBase);
            var pointInfo = point.GetComponent<SpawnPoint>();

            if (pointInfo)
            {
                point.transform.localPosition = Center.transform.localPosition + Center.transform.up * dodgeRadius;
                point.transform.localEulerAngles = Center.transform.localEulerAngles;
                pointInfo.Index = i.ToString();

                Center.transform.Rotate(0f, 0f, _spawnAngle);
            }
        }

        SpawnPointBase.gameObject.SetActive(_isShowSpawnPoint);
    }

    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AppManager.Instance.Quit();
        }
    }

    protected override void PlayGame()
    {
        base.PlayGame();

        OperateBallMovement();
        playerCollider.offset = new Vector2((Ball.transform.localPosition.x * PlayGround.localScale.x) + PlayGround.localPosition.x, (Ball.transform.localPosition.y *  PlayGround.localScale.y) + PlayGround.localPosition.y);
        playerCollider.radius = 15f * Mathf.Abs(PlayGround.localScale.x);

        DebugElements[BallAngle].text = $"Ball Angle : {Mathf.Abs(Center.transform.localEulerAngles.z - 360f).ToString("F2")}";
        DebugElements[SongTotalTime].text = $"���� ���� ���� �ð� : {audioSource.time.ToString("F2")}";
    }

    protected override void PlayProcess()
    {
        base.PlayProcess();

        DebugElements[CurrentLine].text = $"Current Line : {_currentLine}";
    }

    protected override void InputChangeDirection()
    {
        if (!_isAutoMode)
        {
            base.InputChangeDirection();
        }
    }

    protected override void SeperateChangeDirection()
    {
        base.SeperateChangeDirection();

        if (_isInState)
        {
            FadeInOutCircle(InCircleFade, 0.2f);
        }
        else
        {
            FadeInOutCircle(OutCircleFade, 0.2f);
        }
    }

    protected override void IntegrationChangeDirection()
    {
        base.IntegrationChangeDirection();

        if (_isInState)
        {
            FadeInOutCircle(InCircleFade, 0.2f);
        }
        else
        {
            FadeInOutCircle(OutCircleFade, 0.2f);
        }
    }

    Tween FadeTween;
    private void FadeInOutCircle(Image circle, float duration)
    {
        circle.color = Color.black;
        FadeTween = circle.DOColor(Color.clear, duration);
    }

    protected override void ChangeBar()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem.Bar >= 0)
        {
            _bar = beatItem.Bar;

            CalculateTick();

            DebugElements[Bar].text = $"Bar : {beatItem.Bar}";
            DebugElements[TickTime].text = $"1Bar�� �ɸ��� �ð� : {_tick}��";
            DebugElements[LineTime].text = $"1�ٿ� �ɸ��� �ð� : {_beatTime}��";
        }
    }


    protected override void ChangeBallSpeed()
    {
        base.ChangeBallSpeed();

        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem.Speed >= 0)
        {
            DebugElements[BallSpeed].text = $"Ball Speed : {beatItem.Speed}";
        }
    }


    private void ResetStage()
    {
        HideChartingItems();
        InitVariable();

        // Read .bmw file
        bmwReader = new BMWReader();
        bmwReader.ReadFile(Directory + "/" + BMWFile);       

        // Get Values
        GetMusicInfo();
        InitBallPosition();
        InitBallSpeed();
        InitCalculateTick();

        // Calculate Beat
        _totalBeatCount = bmwReader.ChartingItem.Count;

        ResetAudio();

        ResetDebugValue();

        GlobalState.Instance.IsPlayerDied = false;
        GlobalState.Instance.PlayerDeadCount = 0;
        GlobalState.Instance.SavePoint = 0;
        GlobalState.Instance.SavePointAngle = bmwReader.ChartingItem[0].BallAngle;
        
        _savePointTime = 0f;

        TextPause.text = "Pause";
    }


    private void InitVariable()
    {
        _bpm = 0;
        _bar = 0;
        _interval = 0f;
        _tick = 0;
        _beatTime = 0;
        _currentLine = -1;
        _timer = 0f;
        _playTime = 0f;
        savePointNum = 0;
        _totalBeatCount = 0;

        _currentBeat = 0;
        _isPlay = false;
        _isPause = false;
        Time.timeScale = 1;        
    }

    private void HideChartingItems()
    {
        foreach (var dodgeList in DodgePointList)
        {
            dodgeList.SetActive(false);
        }

        foreach (var inList in InObstacleList)
        {
            inList.SetActive(false);
        }

        foreach (var outList in OutObstacleList)
        {
            outList.SetActive(false);
        }

        if (GameObject.Find("SavePoint(Clone)"))
        {
            GameObject savePoint = GameObject.Find("SavePoint(Clone)");
            Destroy(savePoint);
        }
    }

    private void ResetAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.time = 0f;
        SoundManager.Instance.SetStageMusic();
    }

    private void ResetDebugValue()
    {
        var musicInfo = bmwReader.MusicInfoItem;

        CurrentLineText.text = $"Current Line : {_currentLine}";

        DebugElements[Title].text = $"��� : {musicInfo.Title}";
        DebugElements[Artist].text = $"�۰ : {musicInfo.Artist}";
        DebugElements[BPM].text = $"BPM : {musicInfo.BPM}";
        DebugElements[Bar].text = $"Bar : {musicInfo.Bar}";
        DebugElements[TickTime].text = $"1Bar Time : {_tick}��";
        DebugElements[LineTime].text = $"1Line Time : {_beatTime}��";
        DebugElements[BallSpeed].text = $"Ball Speed : {(int)speed}";
        DebugElements[BallAngle].text = $"Ball Angle : {Center.transform.localEulerAngles.z}";
        DebugElements[SongTotalTime].text = $"���� ���� ���� �ð� : {audioSource.time.ToString("F2")}";
        DebugElements[CurrentSongTime].text = $"���� ���� �� �ð� : {audioSource.clip.length}";
        DebugElements[CurrentLine].text = $"Current Line : {_currentLine}";
        DebugElements[TotalLine].text = $"Total Line : {bmwReader.ChartingItem.Count - 1}";
    }

    protected override void StartGame()
    {
        ResetStage(); 
        base.StartGame();
    }

    protected override void FinishGame()
    {
        _isPlay = false;
        ResetStage();
    }

    public void DropDownAlbumValueChanged()
    {
        int index = DropDownAlbum.value;

        GlobalState.Instance.AlbumIndex = index;
        Debug.Log($"{GlobalState.Instance.AlbumIndex}");
    }

    public void DropDownStageValueChanged()
    {
        int index = DropDownStage.value;

        GlobalState.Instance.StageIndex = index;
        Debug.Log($"{GlobalState.Instance.StageIndex}");
    }
    
    public void OnClickGameMode()
    {
        _isGameMode = !_isGameMode;

        //To do : Game Mode Restart ���� ó�� �ؾ���
        if (GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            rigid.simulated = _isGameMode;
        }

        TextGameMode.text = $"Game Mode \n {_isGameMode}";
    }

    private bool _isAutoMode = false;
    public void OnClickAutoMode()
    {
        _isAutoMode = !_isAutoMode;

        foreach (var dodge in DodgePointList)
        {
            if (dodge.GetComponent<BoxCollider2D>() != null)
            {
                dodge.GetComponent<BoxCollider2D>().enabled = _isAutoMode;
            }
        }

        TextAutoMode.text = $"Auto Mode \n {_isAutoMode}";
    }

    public void OnClickLoad()
    {
        ResetStage();
    }

    public void OnClickStart()
    {
        StartGame();
    }

    private bool _isPause = false;
    public void OnClickPause()
    {
        if (_isPlay)
        {
            _isPause = !_isPause;

            if (_isPause)
            {
                TextPause.text = "Play";

                Time.timeScale = 0;
                audioSource.Pause();
            }
            else
            {
                TextPause.text = "Pause";

                Time.timeScale = 1;
                audioSource.Play();
            }
        }
    }


    public void OnClickStop()
    {
        ResetStage();
    }
    
    public void OnClickDebugTab()
    {
        _isShowDebugtab = !_isShowDebugtab;

        DebugTab.SetActive(_isShowDebugtab);

        if (_isShowDebugtab)
        {
            TextDebug.text = "Show\nDebug Tab";
        }
        else
        {
            TextDebug.text = "Hide\nDebug Tab";
        }
    }


    private bool _isShowSpawnPoint = false;
    public void OnClickSpawnPointTab()
    {
        _isShowSpawnPoint = !_isShowSpawnPoint;

        SpawnPointBase.gameObject.SetActive(_isShowSpawnPoint);

        if (_isShowSpawnPoint)
        {
            TextSpawnPoint.text = "Show\nSpawn Points";
        }
        else
        {
            TextSpawnPoint.text = "Hide\nSpawn Points";
        }
    }

    private bool _isShowHalfAngle = false;
    public void OnClickHalfAngle()
    {
        _isShowHalfAngle = !_isShowHalfAngle; 
        
        HalfAngle.SetActive(_isShowHalfAngle);

        if (_isShowHalfAngle)
        {
            TextHalfAngle.text = "Show\nQuater Angle";
        }
        else
        {
            TextHalfAngle.text = "Hide\nQuater Angle";
        }
    }

    private float _savePointTime = 0f;
    public void SavePointEnter()
    {
        _savePointTime = _timer;

        GlobalState.Instance.SavePoint = _currentLine;
        GlobalState.Instance.SaveMusicPlayingTime = audioSource.time;
        GlobalState.Instance.SavePointAngle = Center.transform.localEulerAngles.z;
    }

    private void ResetSavePointState()
    {
        _currentLine = GlobalState.Instance.SavePoint;

        var beatItem = bmwReader.ChartingItem[_currentLine];
        _timer = _savePointTime;

        audioSource.time = GlobalState.Instance.SaveMusicPlayingTime;

        if (GlobalState.Instance.SavePoint > 0)
        {
            GlobalState.Instance.SavePointAngle = beatItem.BallAngle;

            if (beatItem.Interval >= 0)
            {
                _timer -= beatItem.Interval;
            }

            Center.transform.localEulerAngles = new Vector3(0f, 0f, -GlobalState.Instance.SavePointAngle);
            Ball.transform.localPosition = Center.transform.localPosition + Center.transform.up * ballRadius;
        }
        else
        {
            InitBallPosition();
            _isInState = false;
        }

        PlayProcess();
    }

    //private float _reStartTime = -3f;
    public void PlayerDieAndSavePointPlay()
    {
        Debug.Log("Player Die!!");

        //GlobalState.Instance.IsPlayerDied = true;
        GlobalState.Instance.PlayerDeadCount++;

        ResetSavePointState();

    }

    //private void TweenPlayerDead()
    //{
    //    var sequence = DOTween.Sequence();

    //    sequence
    //        .OnStart(() => UpdateText("3"))
    //        .Append(FadeOutText())
    //        .AppendCallback(() => UpdateText("2"))
    //        .Append(FadeOutText())
    //        .AppendCallback(() => UpdateText("1"))
    //        .Append(FadeOutText())
    //        .AppendCallback(() => UpdateText("GO!"))
    //        .Append(FadeOutText())
    //        .OnComplete(PlayCountDownEnd);
    //}

    //private void PlayCountDownEnd()
    //{
    //    SoundManager.Instance.MusicAudio.Play();
    //    GlobalState.Instance.IsPlayerDied = false;
    //    //cnt = 0;
    //}

    //private void InitializeAlpha()
    //{
    //    countDownText.alpha = 1.0f;
    //}

    //private void UpdateText(string text)
    //{
    //    InitializeAlpha();

    //    countDownText.text = text;
    //}

    //private Tween FadeOutText()
    //{
    //    return countDownText.DOFade(0, 1.0f);
    //}

    void PlayerDie()
    {
        PlayerDieAndSavePointPlay();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SavePoint"))
        {
            //Debug.Log("�θ�ü : ���̺� ����Ʈ!!");
            Destroy(other.gameObject);
            SavePointEnter();
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("�θ�ü : Heat Obstacle");
            PlayerDie();
        }

        if (other.gameObject.CompareTag("DodgePoint"))
        {
            //Debug.Log("�θ�ü : Heat Dodge Point");
            IntegrationChangeDirection();
        }
    }
}
