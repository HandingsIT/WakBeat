using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Video;

public abstract class Stage : MonoBehaviourSingleton<Stage>
{
    #region Public Member Variable
    [Header("Stage Base")]
    public Image BallSkin;
    public Image CircleSkin;
    public Image BackGroundSkin;

    public RectTransform PlayGround;
    public GameObject Center;
    public GameObject Player;

    [Header("[ Animation ]")]
    public Animation StageAnim;

    [Header("[ Video Player ]")]
    public VideoPlayer videoPlayer = null;

    [Header("[ Tween Elements ]")]
    public Image InCircleFade;
    public Image OutCircleFade;
    public Text ClearRate;

    [Header("[ Instance Object Container ]")]
    public Transform Container;
    public GameObject CenterPivot;      //게임 로직과 관계없이 중심 Pivot

    [Header("[ Dodge Point ]")]
    public GameObject DodgePoint;
    public Transform DodgePointBase;
    public List<GameObject> DodgePointLists = new List<GameObject>();

    [Header("[ Obstacles ]")]
    public GameObject[] Obstacles;
    public Transform ObstacleInBase;
    public List<GameObject> InObstacleLists = new List<GameObject>();

    public Transform ObstacleOutBase;
    public List<GameObject> OutObstacleLists = new List<GameObject>();

    [Header("[ Particles ]")]
    public ParticleSystem ParticleHP;
    public ParticleSystem ParticleBarrier;

    [Header("[ Save Point ]")]
    public GameObject[] SavePoint;

    [Header("[ Default Save Point Effect ]")]
    public Image[] SavePointCircles;            // TO DO : Effect 분리작업 필요

    [Header("[ Clear Image ]")]
    public Image ClearImage;
    public AnimCurve ClearCurve;

    public Text TextCurrentLine = null;        // 테스트 끝난후 배포시에 제거하겠습니다.
    #endregion

    #region Protected Member Variable
    //---------------------------------------------------
    protected float dodgeRadius = 334f;
    protected float outRadius = 355f;
    protected float inRadius = 312f;
    protected float ballRadius = 0;
    protected float variableRadius;

    protected BMWReader bmwReader = null;        // 채보 스크립트   
    protected AudioSource audioSource = null;
    protected SoundManager SoundManager = null;
    protected GlobalState state;
    protected UserData userData;

    protected string _title = "";                // 곡 제목
    protected string _artist = "";               // 작곡가

    protected float _bpm = 0;                    // Bar Per Minute
    protected float _bar = 0;                    // Bar
    protected float _interval = 0f;              // Interval

    protected float _tick = 0;                   // 1 Bar(칸)에 소요되는 시간
    protected float _beatTime = 0;               // 1 Line에 소요되는 시간
    protected float _ballSpeed = 360;            // 1 Line동안 움직이는 Ball의 스피드

    public static int _currentLine = -1;         // 현재 진행중인 Line의 아이템들
    protected int _totalBeatCount = 0;           // 총 Beat 수

    protected float _timer = 0f;                 // 음악 로직을 처리하기 위한 타이머
    protected float _playTime = 0f;              // Stage 시작후 총 흐른 시간
    protected float _musicCurrentTime = 0f;      // 곡의 현재 진행중인 시간
    protected float _musicTotalTime = 0f;        // 곡의 총 시간

    protected static int _spawnCount = 72;       // TO DO : Object Pooling
    protected static float _spawnAngle = -5f;

    protected bool _isPlay = false;              // 게임 진행중 체크
    protected bool _isFinish = false;            // 게임 종료 체크

    protected bool _isPause = false;             // 일시정지 체크
    protected bool _isGameMode = false;          // 게임 모드 체크
    protected bool _isAutoMode = false;          // 오토 모드 체크

    // Key 입력 부
    private string _keyDivision = string.Empty;

    // Player HP
    private int _currentHP = 1;

    // 한목숨에 Barrier 스킬 사용 여부
    private bool _usedBarrier = false;
    private bool _isBarrier = false;
    #endregion

    public virtual string Directory
    {
        get
        {
            string dir = GlobalState.Instance.BMWFolderPath;

            return dir;
        }
    }

    public virtual string BMWFile
    {
        get
        {
            string file = GlobalState.Instance.BMWFile;

            return file;
        }
    }

    private void Awake()
    {
        // Test 22
        Init();
    }

    protected virtual void Init()
    {
        // Read .bmw file
        bmwReader = new BMWReader();
        bmwReader.ReadFile(Directory, BMWFile);

        //InitConfigSettings();
        InitGlobalSettings();

        //Get User Data (.json file)
        GetBallSkin();

        // Create Stage Objects
        CreateDodgePoint();
        CreateObstacles();

        //Create Skill VFXs
        //CreateVFX();

        // Get Values
        GetMusicInfo();
        GetDefaultColor();

        // Set Values
        SetMusicInfo();

        InitHP();
        _usedBarrier = false;

        InitBallPosition();
        InitBallSpeed();
        InitCalculateTick();

        // Calculate Beat
        _totalBeatCount = bmwReader.ChartingItem.Count;

        // Key 입력 부
        _keyDivision = null == DataManager.dataKeyDivision ? "Integration" : DataManager.dataKeyDivision;
      
        if (ClearImage) ClearImage.gameObject.SetActive(false);
        if (!state.DevMode) ClearRate.gameObject.SetActive(false);     

        if (ClearRate) ClearRate.text = $"Clear Rate\n0%";       

        ShowCurrentLine();

        // 음악 정보 팝업 호출
        if (UIManager.Instance.UIElementPopUp)
        {
            UIManager.Instance.UIElementPopUp.SetPopUpMusicInfo();
        }
    }

    // TO DO : 다른스킬들도 전부 사용시에만 Instantiate 해서 사용
    //private ParticleSystem _chanVFX = null;
    //void CreateVFX()
    //{
    //    if (state.UseGreenhorn)
    //    {
    //        var vfxGameObject = GameObject.Instantiate(GlobalData.Instance.StageInfo.VFXs[0], Player.transform);
    //        var vfx = vfxGameObject.transform.GetChild(0).GetComponent<ParticleSystem>();

    //        if (vfx)
    //        {
    //            _chanVFX = vfx;
    //            _chanVFX.gameObject.SetActive(false);
    //        }
    //    }
    //}

    protected virtual void ShowCurrentLine()
    {
        if (TextCurrentLine != null)
        {
            TextCurrentLine.text = $"Current Line : {_currentLine}";

            if (!state.DevMode)
            {
                TextCurrentLine.gameObject.SetActive(false);
            }
            else
            {
               TextCurrentLine.gameObject.SetActive(true);
            }
        }
    }

    protected virtual void InitGlobalSettings()
    {
        state = GlobalState.Instance;
        userData = GlobalState.Instance.UserData.data;

        _isGameMode = state.GameMode;
        _isAutoMode = state.AutoMode;

        var col = Player.GetComponent<Rigidbody2D>();
        if (col)
        {
            col.simulated = _isGameMode;
        }

        ResetGlobalState();
    }

    protected virtual void InitHP()
    {
        if (state.UseBonusHP && state.UseGreenhorn) // Shop Skill에서 처리 해줘야 함[완료]
        {
            _currentHP = 38;
        }
        else if (state.UseGreenhorn)
        {
            _currentHP = 37;
        }
        else if (state.UseBonusHP)
        {
            _currentHP = 2;
        }
        else
        {
            _currentHP = 1;
        }             
    }

    protected virtual void InitBallPosition()
    {
        variableRadius = outRadius;
        ballRadius = variableRadius;     // Init Ball Position 

        // Init Start Ball Rotation
        float ballAngle = 0;

        if (bmwReader.ChartingItem[0].BallAngle >= 0)
        {
            ballAngle = bmwReader.ChartingItem[0].BallAngle;
        }
        else
        {
            ballAngle = 0;
        }

        Center.transform.localEulerAngles = new Vector3(0f, 0f, -ballAngle);

        Player.transform.localPosition = Center.transform.localPosition + Center.transform.up * ballRadius;
    }

    public virtual void ResetGlobalState()
    {
        state.SavePointTime = 0f;
        state.SavePointLine = 0;
        state.SaveMusicTime = 0.0f;
        state.SaveVideoTime = 0.0f;
        state.SaveAnimationTime = 0.0f;
        state.SavePointAngle = bmwReader.ChartingItem[0].BallAngle;

        state.BallSprite = null;
        state.BackGroundSprite = null;
        state.ObstacleSprite = null;
        state.ObstacleColor = Color.black;

        state.IsPlayerDied = false;
        state.PlayerDeadCount = 0;
    }

    protected virtual void GetMusicInfo()
    {
        if (bmwReader != null)
        {
            _title = bmwReader.MusicInfoItem.Title;
            _artist = bmwReader.MusicInfoItem.Artist;
            _bpm = bmwReader.MusicInfoItem.BPM;
            _bar = bmwReader.MusicInfoItem.Bar;
        }

        // Sound Fade In 
        SoundManager.Instance.FadeInMusicVolume(0.5f);
    }

    protected virtual void SetMusicInfo()
    {
        // Sound 제어 부
        SoundManager = SoundManager.Instance;
        audioSource = SoundManager.MusicAudio;

        SoundManager.Instance.SetStageMusic();
        state.StageMusicLength = (int)audioSource.clip.length;
    }

    protected virtual void GetBallSkin()
    {
        for (int i = 0; i < userData.shopData.skinUsingYn.Length; i++)
        {
            if (userData.shopData.skinUsingYn[i].Contains("Y"))
            {
                if (BallSkin)
                {
                    BallSkin.sprite = GlobalData.Instance.StageInfo.BallSkins[i];
                }
            }
        }
    }

    protected virtual void SetBallSkin(Sprite ball)
    {
        if (BallSkin)
        {
            BallSkin.sprite = ball;
        }
        else
        {
            Debug.LogError("Ball Skin Is Null. Check Setup Album Info");
        }
    }

    protected virtual void SetCircleSprite(Sprite circle)
    {
        if (CircleSkin)
        {
            CircleSkin.sprite = circle;
        }
    }

    protected virtual void SetBackGroundSprite(Sprite backGround)
    {
        if (BackGroundSkin)
        {
            BackGroundSkin.sprite = backGround;
        }
    }

    protected virtual void CreateDodgePoint()
    {
        Center.transform.localEulerAngles = Vector3.zero;

        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject dodge = Instantiate(DodgePoint, DodgePointBase);

            if (dodge)
            {
                dodge.transform.localPosition = Center.transform.localPosition + Center.transform.up * dodgeRadius;
                dodge.transform.localEulerAngles = Center.transform.localEulerAngles;

                var col = dodge.GetComponent<BoxCollider2D>();
                if (col)
                {
                    col.enabled = _isAutoMode;
                }

                var image = dodge.GetComponent<Image>();
                if (image)
                {
                    if (!state.DevMode)
                    {
                        if (_isAutoMode)
                        {
                            image.color = Color.clear;
                        }
                    }
                }

                DodgePointLists.Add(dodge);

                dodge.SetActive(false);

                Center.transform.Rotate(0f, 0f, _spawnAngle);
            }
        }

        if(state.ShowDodge)
        {
            DodgePointBase.gameObject.SetActive(state.ShowDodge);
        }
    }

    // TO DO : 각자 Stage에서 구현할것 부모 객체는 추상화만 (각자 스크립트에서 코드2줄만 추가하면됨)
    protected virtual void CreateObstacles()
    {
        CreateInObstacle(0, inRadius);
        CreateOutObstacle(0, outRadius);
    }

    protected virtual void CreateInObstacle(int obstacleType, float radius)
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject inObstacle = Instantiate(Obstacles[obstacleType], ObstacleInBase);

            if (inObstacle)
            {
                inObstacle.transform.localPosition = Center.transform.localPosition + Center.transform.up * radius;
                inObstacle.transform.localEulerAngles = Center.transform.localEulerAngles + new Vector3(0f, 0f, 180f);
                InObstacleLists.Add(inObstacle);

                inObstacle.SetActive(false);

                Center.transform.Rotate(0f, 0f, _spawnAngle);
            }
        }
    }

    protected virtual void CreateOutObstacle(int obstacleType, float radius)
    {
        Center.transform.localEulerAngles = Vector3.zero;

        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject outObstacle = Instantiate(Obstacles[obstacleType], ObstacleOutBase);

            if (outObstacle)
            {
                outObstacle.transform.localPosition = Center.transform.localPosition + Center.transform.up * radius;
                outObstacle.transform.localEulerAngles = Center.transform.localEulerAngles;
                OutObstacleLists.Add(outObstacle);

                outObstacle.SetActive(false);

                Center.transform.Rotate(0f, 0f, _spawnAngle);
            }
        }
    }

    protected virtual void CreateSavePoint(int savePointType)
    {
        GameObject savePoint = Instantiate(SavePoint[savePointType], Container);

        if (savePoint)
        {
            savePoint.transform.localPosition = CenterPivot.transform.localPosition + CenterPivot.transform.up * dodgeRadius;
            
            savePoint.transform.localScale = Vector3.one * 0.3f;
            savePoint.transform.DOScale(Vector3.one, 0.1f).SetAutoKill();
        }
    }

    protected virtual void SetObstaclesSkin(int ballType, Color color)
    {
        var stageInfo = GlobalData.Instance.StageInfo;

        foreach (var obstacle in InObstacleLists)
        {
            var skin = obstacle.GetComponent<Image>();
            if (skin)
            {
                if (stageInfo.ObstacleSkins[ballType])
                {
                    skin.sprite = stageInfo.ObstacleSkins[ballType];
                    skin.color = color;
                }
            }
        }

        foreach (var obstacle in OutObstacleLists)
        {
            var skin = obstacle.GetComponent<Image>();
            if (skin)
            {
                if (stageInfo.ObstacleSkins[ballType])
                {
                    skin.sprite = stageInfo.ObstacleSkins[ballType];
                    skin.color = color;
                }
            }
        }
    }

    void SetObstaclesSprite(Sprite sprite)
    {
        foreach (var obstacle in InObstacleLists)
        {
            var skin = obstacle.GetComponent<Image>();
            if (skin)
            {
                skin.sprite = sprite;
            }
        }

        foreach (var obstacle in OutObstacleLists)
        {
            var skin = obstacle.GetComponent<Image>();
            if (skin)
            {
                skin.sprite = sprite;
            }
        }
    }

    protected virtual void SetObstaclesColor(Color color)
    {
        var stageInfo = GlobalData.Instance.StageInfo;

        foreach (var obstacle in InObstacleLists)
        {
            var skin = obstacle.GetComponent<Image>();
            if (skin)
            {
                skin.color = color;
            }
        }

        foreach (var obstacle in OutObstacleLists)
        {
            var skin = obstacle.GetComponent<Image>();
            if (skin)
            {
                skin.color = color;
            }
        }
    }

    protected virtual void InitBallSpeed()
    {
        if (bmwReader.ChartingItem[0].Speed.ToUpper() == "NONE")
        {
            _ballSpeed = 360f;
        }
        else
        {
            float.TryParse(bmwReader.ChartingItem[0].Speed, out float speed);

            _ballSpeed = speed;
        }
    }

    protected virtual void InitCalculateTick()
    {
        _bpm = bmwReader.MusicInfoItem.BPM;
        _bar = bmwReader.MusicInfoItem.Bar;

        CalculateTick();
    }

    protected virtual void CalculateTick()
    {
        float _bps = _bpm / 60;
        _tick = 1 / _bps;

        _beatTime = _tick * _bar;

        Debug.Log($"Tick : {_tick}, Bar : {_bar}, Beat Time : {_beatTime}");
    }

    protected virtual void PlayProcess()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];
        if (beatItem == null)
        {
            Debug.LogError("PlayPorocess beat Item is null");
            return;
        }

        //PlayAnimation(_animationName);

        ChangeBar();

        AddInterval(beatItem);

        ChangeBallAngle();

        ChangeBallSpeed();

        ShowChartingItems();

        TweenClearRate();

        // 테스트 끝난후 배포시에 삭제 예정
        if (TextCurrentLine != null)
        {
            if (state.DevMode)
            {
                TextCurrentLine.text = $"Current Line : {_currentLine}";
            }
        }
    }

    protected virtual void TweenClearRate()
    {
        if (state.DevMode)
        {
            if (_currentLine > 0)
            {
                float _rate = (((float)_currentLine + 1) / (float)_totalBeatCount) * 100f;
                ClearRate.DOText($"Clear Rate\n{((int)_rate)}%", _tick);
            }
            else
            {

                ClearRate.DOText($"Clear Rate\n0%", _beatTime);
            }
        }
    }

    protected string _animationName = string.Empty;
    protected virtual void PlayAnimation(string animationName)
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        //if (StageAnim.clip == null) return;

        if (beatItem.AnimationIndex >= 0 && bmwReader.AnimationItem.Count > beatItem.AnimationIndex)
        {
            var animItem = bmwReader.AnimationItem[beatItem.AnimationIndex];

            _animationName = animItem.AnimationName;

            if (!StageAnim[_animationName]) return;

            StageAnim.Rewind(_animationName);
            StageAnim[_animationName].normalizedTime = 0f;
            StageAnim[_animationName].speed = 1f;
            StageAnim.Play(_animationName);
        }
    }

    protected virtual void ChangeBar()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem.Bar >= 0)
        {
            _bar = beatItem.Bar;

            CalculateTick();
        }
    }

    protected virtual void AddInterval(ChartingItem item)
    {
        item = bmwReader.ChartingItem[_currentLine];

        if (item.Interval.ToUpper() != "NONE")
        {
            float _bmwInterval = float.Parse(item.Interval);
            _timer += _bmwInterval;
        }
    }

    protected virtual void ChangeBallAngle()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem.BallAngle >= 0)
        {
            Vector3 targetRotate = new Vector3(0f, 0f, -beatItem.BallAngle);

            if (beatItem.BallAngleTime < 0)
            {
                beatItem.BallAngleTime = 0;
            }

            Center.transform.localRotation = Center.transform.localRotation;
            Center.transform.DORotate(targetRotate, beatItem.BallAngleTime);
        }
    }

    Tween speedChangeTween;
    protected virtual void ChangeBallSpeed()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem.Speed.ToUpper() != "NONE")
        {
            float.TryParse(beatItem.Speed, out float speed);

            if (beatItem.SpeedTime > 0)
            {
                speedChangeTween = DOTween.To(() => _ballSpeed, x => _ballSpeed = x, speed, beatItem.SpeedTime).
                SetEase(Ease.Linear).SetAutoKill();
            }
            else
            {
                if (speedChangeTween != null)
                {
                    if (speedChangeTween.IsPlaying())
                    {
                        speedChangeTween.Pause();
                    }
                }

                _ballSpeed = speed;
                beatItem.SpeedTime = 0;
            }
        }     
    }

    protected virtual void ShowChartingItems()
    {
        if (_isAutoMode || state.ShowDodge)
        {
            ShowDodgePoint();
        }

        ShowInObstacles();
        ShowOutObstacles();

        ShowSavePoint();
    }

    protected virtual void ShowDodgePoint()
    {
        if (_currentLine >= bmwReader.ChartingItem.Count) return;

        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem != null)
        {
            // Hide
            foreach (var dodgeList in DodgePointLists)
            {
                dodgeList.SetActive(false);
            }

            // Show Objects
            if (beatItem.DodgePointElements[0].Index > -1)
            {
                foreach (var dodge in beatItem.DodgePointElements)
                {
                    DodgePointLists[dodge.Index].gameObject.SetActive(true);
                }
            }

            // Show Dummy
            if (beatItem.DummyDodgePointElements[0].Index > -1)
            {
                foreach (var dummy in beatItem.DummyDodgePointElements)
                {
                    DodgePointLists[dummy.Index].gameObject.SetActive(true);
                }
            }
        }
    }

    protected virtual void ShowInObstacles()
    {
        if (_currentLine >= bmwReader.ChartingItem.Count) return;

        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem != null)
        {
            // Hide
            foreach (var inList in InObstacleLists)
            {
                inList.SetActive(false);
            }

            // Show Objects
            if (beatItem.InObstacleElements[0].Index > -1)
            {
                foreach (var inObstacle in beatItem.InObstacleElements)
                {
                    InObstacleLists[inObstacle.Index].gameObject.SetActive(true);
                }
            }

            // Show Dummy
            if (beatItem.DummyInObstacleElements[0].Index > -1)
            {
                foreach (var dummy in beatItem.DummyInObstacleElements)
                {
                    InObstacleLists[dummy.Index].gameObject.SetActive(true);
                }
            }         
        }
    }

    protected virtual void ShowOutObstacles()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem != null)
        {
            // Hide
            foreach (var outList in OutObstacleLists)
            {
                outList.SetActive(false);
            }

            // Show Objects
            if (beatItem.OutObstacleElements[0].Index > -1)
            {
                foreach (var outObstacle in beatItem.OutObstacleElements)
                {
                    OutObstacleLists[outObstacle.Index].gameObject.SetActive(true);
                }
            }

            // Show Dummy
            if (beatItem.DummyOutObstacleElements[0].Index > -1)
            {
                foreach (var dummy in beatItem.DummyOutObstacleElements)
                {
                    OutObstacleLists[dummy.Index].gameObject.SetActive(true);
                }
            }
        }
    }

    protected virtual void ShowSavePoint()
    {
        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (beatItem.SavePoint != -1)
        {
            CenterPivot.transform.localEulerAngles = Vector3.zero;
            CenterPivot.transform.localEulerAngles = new Vector3(0f, 0f, beatItem.SavePoint * _spawnAngle);

            CreateSavePoint(0);
        }
    }

    //------------------------------------ Start Game ----------------------------------------
    #region Start Game
    protected virtual void Start()
    {
        //StartGame();
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(PrepareToStartGame());
    }

    IEnumerator PrepareToStartGame()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Prepare();

            yield return new WaitUntil(() => videoPlayer.isPrepared);

            StartGame();
        }
        else
        {
            StartGame();
        }
    }

    protected virtual void StartGame()
    {
        _isPlay = true;

        _currentLine = 0;

        PlayStage();

        PlayProcess();
    }

    protected virtual void PlayStage()
    {
        SaveBallSprite();
        SaveBGSprite();
        SaveObstacleSkinColor();

        InitBallPosition();
        SoundManager.TurnOnStageMusic();
        PlayThisAnimation();
        PlayVideo();
    }

    private void SaveBallSprite()
    {
        state.BallSprite = BallSkin.sprite;
    }

    private void SaveBGSprite()
    {
        state.BackGroundSprite = BackGroundSkin.sprite;
    }

    private void SaveObstacleSkinColor()
    {
        var obstacle = ObstacleInBase.GetChild(0).gameObject;
        var obstacleImage = obstacle.GetComponent<Image>();

        if (obstacleImage)
        {
            state.ObstacleSprite = obstacleImage.sprite;
            state.ObstacleColor = obstacleImage.color;
        }
    }


    protected virtual void PlayVideo()
    {
        if (videoPlayer)
        {
            videoPlayer.time = 0f;
            videoPlayer.playbackSpeed = 1f;
            videoPlayer.Play();
        }
    }

    protected virtual void PlayThisAnimation()
    {
        if (StageAnim.clip == null) return;

        _animationName = StageAnim.clip.name;

        StageAnim[_animationName].time = 0f;
        StageAnim[_animationName].speed = 1f;
        StageAnim.Play(_animationName);
    }
    #endregion

    //------------------------------------ Play Game -----------------------------------------
    #region Play Game!!!
    protected virtual void Update()
    {
        if (_isPlay)
        {
            PlayGame();
            InputExcute();
            CallPause();
        }
    }

    protected virtual void PlayGame()
    {
        _playTime += Time.deltaTime;
        _timer += Time.deltaTime;

        Center.transform.Rotate(0f, 0f, (Time.deltaTime / _beatTime) * -_ballSpeed);
        OperateBallMovement();

        if (_currentLine < bmwReader.ChartingItem.Count - 1)
        {
            if (_timer > _beatTime)
            {
                _currentLine++;
                _timer -= _beatTime;

                PlayProcess();
            }
        }
        else
        {
            if (!_isFinish)
            {
                FinishGame();
            }
        }
        
    }

    protected virtual void OperateBallMovement()
    {
        // 공의 움직임 구현
        Player.transform.localEulerAngles = new Vector3(0f, 0f, Center.transform.localEulerAngles.z);
        Player.transform.localPosition = Center.transform.localPosition + Center.transform.up * ballRadius;

        // Input Change Direction
        if (!_isAutoMode)
        {
            InputChangeDirection();
        }
    }

    // 키 입력 처리
#region Change Direction
    protected bool _isInState = false;
    public virtual void InputChangeDirection()
    {
        if(DataManager.dataBackgroundProcActive)
        {
            if (Input.anyKey && null != Input.inputString && "" != Input.inputString)
            {
                if (_keyDivision.Equals("Separation"))
                {
                    var innerKey = DataManager.dataInnerOperationKey;
                    var outKey = DataManager.dataOuterOperationKey;

                    // 키 분리 구분 > 분리
                    if (innerKey != null && innerKey.Length > 0 && outKey != null && outKey.Length > 0)
                    {
                        SeperateChangeDirection();                   
                    }
                    else
                    {
                        // 키 분리 구분 > 분리 > 커스텀 한 키가 없을 땐 스페이스로 통일
                        IntegrationChangeDirection();
                    }
                }
                else
                {
                    // 키 분리 구분 > 통합
                    if (Input.anyKeyDown)
                    {
                        IntegrationChangeDirection();                    
                    }
                }
            }

            // 안드로이드 화면 터치 방향전환
#if UNITY_ANDROID
            if (Input.GetMouseButtonDown(0))
            {
                IntegrationChangeDirection();

                //Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                //Input.mousePosition.y, -Camera.main.transform.position.z));

                //Debug.Log(point);
            }
#endif
        }
    }

    public virtual void SeperateChangeDirection()
    {
        var innerOperationKey = DataManager.dataInnerOperationKey;
        var outOperationKey = DataManager.dataOuterOperationKey;

        for (int i = 0; i < innerOperationKey.Length; i++)
        {
            if (!"".Equals(innerOperationKey[i]) && Input.inputString.Equals(innerOperationKey[i]))
            {
                _isInState = true;
                ChangeDirection(_isInState);
            }

            if (!"".Equals(outOperationKey[i]) && Input.inputString.Equals(outOperationKey[i]))
            {
                _isInState = false;
                ChangeDirection(_isInState);
            }
        }
    }

    public virtual void IntegrationChangeDirection()
    {
        _isInState = !_isInState;
        ChangeDirection(_isInState);
    }

    public virtual void ChangeDirection(bool isIn)
    {
        if (isIn)
        {
            ballRadius = inRadius;
            ChangeDirectionEffect();
        }
        else
        {
            ballRadius = outRadius;
            ChangeDirectionEffect();
        }
    }

    protected float _changeDirectionDuration = 0.2f;
    protected virtual void ChangeDirectionEffect()
    {
        if (_isInState)
        {
            TweenChangeDirection(InCircleFade, _changeDirectionDuration);
        }
        else
        {
            TweenChangeDirection(OutCircleFade, _changeDirectionDuration);
        }
    }

    protected virtual void TweenChangeDirection(Image circle, float duration)
    {
        circle.color = new Color(0f, 0f, 0f, 0.2f);
        circle.DOColor(Color.clear, duration).SetAutoKill();
    }
#endregion

    public virtual void SavePointEnter()
    {
        state.SavePointTime = _timer;

        state.SavePointLine = _currentLine;

        state.SavePointAngle = Center.transform.localEulerAngles.z;

        state.SaveMusicTime = audioSource.time;

        if (StageAnim[_animationName]) state.SaveAnimationTime = StageAnim[_animationName].time;

        if (videoPlayer) state.SaveVideoTime = (float)videoPlayer.time;

        //TO DO : 각자 스테이지에서 구현할 것 
        EnterSavePointEffect();

        SaveBallSprite();
        SaveBGSprite();
        SaveObstacleSkinColor();
    }

    protected virtual void ResetSavePointState()
    {
        _timer = state.SavePointTime;
        _currentLine = state.SavePointLine;
        audioSource.time = state.SaveMusicTime;

        if (videoPlayer) videoPlayer.time = state.SaveVideoTime;
        if (StageAnim[_animationName]) StageAnim[_animationName].time = state.SaveAnimationTime;

        if (state.BallSprite != null)
        {
            BallSkin.sprite = state.BallSprite;
        }

        if (state.BackGroundSprite != null)
        {
            BackGroundSkin.sprite = state.BackGroundSprite;
        }

        if (state.ObstacleSprite != null)
        {
            SetObstaclesSprite(state.ObstacleSprite);
        }

        SetObstaclesColor(state.ObstacleColor);

        var savePoint = GameObject.Find("SavePoint(Clone)");
        if (savePoint) Destroy(savePoint);

        InitHP();
        _usedBarrier = false;

        TweenClearRate();

        var beatItem = bmwReader.ChartingItem[_currentLine];

        if (state.SavePointLine > 0)
        {
            AddInterval(beatItem);

            Center.transform.localEulerAngles = new Vector3(0f, 0f, state.SavePointAngle);
            Player.transform.localPosition = Center.transform.localPosition + Center.transform.up * ballRadius;
        }
        else
        {
            InitBallPosition();

            _isInState = false;
        }

        PlayProcess();
    }

    protected virtual void PlayerDieAndSavePointPlay()
    {        
        _currentHP--;

        if (_currentHP <= 0)
        {
            state.PlayerDeadCount++;
            ResetSavePointState();

            Debug.Log("Player Die!!");
        }
        else
        {
            EffectVFX();
        }
    }

    public virtual void PlayerDie()
    {
        if (!_isBarrier)
        {
            PlayerDieAndSavePointPlay();
        }
    }

#region Input Extute
    // ---------------------- Skill Input Excute ---------------------
    public void InputExcute()
    {
        if (state.UseNewGaMe)
        {
            InputX();
        }

        if (state.UseBarrier)
        {
            InputZ();
        }
    }

    // 일시정지 창 호출
    public void CallPause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(_isPause)
            {
                SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingOut);
                UIElementSetting.Instance.ButtonClickControll("Pause", "Close");
            } 
            else
            {
                SoundManager.Instance.PlaySoundFX((int)GlobalData.SFX.SettingOut);
                UIElementSetting.Instance.ButtonClickControll("Pause", "Open");
            }
        }
    }

    protected virtual void InputX()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            CenterPivot.transform.localEulerAngles = Center.transform.localEulerAngles - new Vector3(0, 0, _spawnAngle);
            CreateSavePoint(0);
        }
    }

    protected virtual void InputZ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UseBarrier();
        }
    }
#endregion

#region Skill Effect
    protected virtual void EffectVFX()
    {
        //if (state.UseGreenhorn && !state.UseBonusHP)
        //{
        //    EffectChan();
        //}
        //else if (state.UseGreenhorn && state.UseBonusHP)
        //{
        //    if (_currentHP >= 2)
        //    {
        //        EffectChan();
        //    }
        //    else
        //    {
        //        EffectHP();
        //    }
        //}
        //else if (state.UseBonusHP)
        //{
        //    EffectHP();
        //}

        EffectHP();

        //if (state.UseBonusHP)
        //{
        //    EffectHP();
        //}
    }

    //void EffectChan()
    //{
    //    if (_chanVFX)
    //    {
    //        _chanVFX.gameObject.SetActive(true);
    //        _chanVFX.Play();

    //        Invoke(nameof(HideParticleChan), 0.5f);
    //    }
    //}

    //void HideParticleChan()
    //{
    //    _chanVFX.gameObject.SetActive(false);
    //}

    void EffectHP()
    {
        ParticleHP.gameObject.SetActive(true);

        ParticleHP.Play();

        Invoke(nameof(HideParticleHP), 0.3f);
    }

    void HideParticleHP()
    {
        ParticleHP.gameObject.SetActive(false);
    }

    void UseBarrier()
    {
        if (!_usedBarrier)
        {
            _isBarrier = true;
            BarrierEffect();           
        }

        _usedBarrier = true;
    }

    protected virtual void BarrierEffect()
    {
        ParticleBarrier.gameObject.SetActive(true);

        ParticleBarrier.Play();

        Invoke(nameof(HideParticleBarrier), 3f);
    }

    void HideParticleBarrier()
    {
        ParticleBarrier.gameObject.SetActive(false);
        _isBarrier = false;
    }
#endregion

#region Save Point Effect
    //-------------------------- Save Point Effect --------------------------------
    public Color[] EffectColor = new Color[7];
    protected Color[] EffectAlpha0Color = new Color[7];

    protected Color DefaultColor1 = new Color(0.4f, 0.4f, 0.72f, 0.8f);
    protected Color DefaultColor2 = new Color(1f, 0.5f, 0.5f, 0.8f);
    protected Color DefaultColor3 = new Color(1f, 0.58f, 1f, 0.8f);
    protected Color DefaultColor4 = new Color(0.95f, 1f, 0.54f, 0.8f);
    protected Color DefaultColor5 = new Color(0.57f, 1f, 1f, 0.8f);
    protected Color DefaultColor6 = new Color(0.3f, 0.3f, 0.3f, 0.8f);
    protected Color DefaultColor7 = new Color(0.45f, 0.71f, 0.65f, 0.8f);

    protected Color DefaultAlpha0Color1 = new Color(0.4f, 0.4f, 0.72f, 0f);
    protected Color DefaultAlpha0Color2 = new Color(1f, 0.5f, 0.5f, 0f);
    protected Color DefaultAlpha0Color3 = new Color(1f, 0.58f, 1f, 0f);
    protected Color DefaultAlpha0Color4 = new Color(0.95f, 1f, 0.54f, 0f);
    protected Color DefaultAlpha0Color5 = new Color(0.57f, 1f, 1f, 0f);
    protected Color DefaultAlpha0Color6 = new Color(0.3f, 0.3f, 0.3f, 0f);
    protected Color DefaultAlpha0Color7 = new Color(0.45f, 0.71f, 0.65f, 0f);

    protected virtual void EnterSavePointEffect()
    {
        if (SavePointCircles == null) return;

        float _effectDuration = 0.2f;

        for (int i = 0; i < SavePointCircles.Length; i++)
        {
            // Set Scale
            SavePointCircles[i].rectTransform.localScale = Vector2.zero;

            // Set Color
            if (EffectColor[i] == null) EffectColor[i] = Color.red;
            SavePointCircles[i].color = EffectColor[i];

            // Enable
            SavePointCircles[i].gameObject.SetActive(true);

            // Tween
            var tween = SavePointCircles[i].rectTransform.DOScale(Vector2.one, _effectDuration);
            tween.SetAutoKill().SetEase(Ease.InCubic).OnComplete(() => { HideEffect(); });
        }
    }

    protected virtual void HideEffect()
    {
        float _fadeDuration = 0.6f;

        for (int i = 0; i < SavePointCircles.Length; i++)
        {
            if (EffectAlpha0Color[i] == null) EffectAlpha0Color[i] = Color.red;

            var tween = SavePointCircles[i].DOColor(EffectAlpha0Color[i], _fadeDuration);
            tween.SetAutoKill().SetDelay(0.2f).SetEase(Ease.OutCubic).OnComplete(() => { SavePointCircles[i].gameObject.SetActive(false); });
        }
    }

    protected virtual void GetDefaultColor()
    {
        EffectColor[0] = DefaultColor1;
        EffectColor[1] = DefaultColor2;
        EffectColor[2] = DefaultColor3;
        EffectColor[3] = DefaultColor4;
        EffectColor[4] = DefaultColor5;
        EffectColor[5] = DefaultColor6;
        EffectColor[6] = DefaultColor7;

        EffectAlpha0Color[0] = DefaultAlpha0Color1;
        EffectAlpha0Color[1] = DefaultAlpha0Color2;
        EffectAlpha0Color[2] = DefaultAlpha0Color3;
        EffectAlpha0Color[3] = DefaultAlpha0Color4;
        EffectAlpha0Color[4] = DefaultAlpha0Color5;
        EffectAlpha0Color[5] = DefaultAlpha0Color6;
        EffectAlpha0Color[6] = DefaultAlpha0Color7;
    }

    protected virtual void SetEffectCircleColor(Color fst, Color snd, Color third, Color fourth, Color fifth, Color six, Color seven)
    {
        EffectColor[0] = fst;
        EffectColor[1] = snd;
        EffectColor[2] = third;
        EffectColor[3] = fourth;
        EffectColor[4] = fifth;
        EffectColor[5] = six;
        EffectColor[6] = seven;
    }

    protected virtual void SetEffectCircleAlpah0Color(Color fst, Color snd, Color third, Color fourth, Color fifth, Color six, Color seven)
    {
        EffectAlpha0Color[0] = fst;
        EffectAlpha0Color[1] = snd;
        EffectAlpha0Color[2] = third;
        EffectAlpha0Color[3] = fourth;
        EffectAlpha0Color[4] = fifth;
        EffectAlpha0Color[5] = six;
        EffectAlpha0Color[6] = seven;
    }
#endregion
#endregion

    //----------------------------------- Finish Game ----------------------------------------
    #region Finish Game!!!
    public virtual void FinishGame()
    {
        //DOTween.KillAll();      

        StageClear();
    }

    public virtual void StageClear()
    {
        // Clear SFX
        SoundManager.PlaySoundFX((int)GlobalData.SFX.Clear);

        ClearImage.gameObject.SetActive(true);

        // Alpha 0 to 1
        ClearImage.DOColor(Color.white, 0.2f).SetAutoKill();

        // Scale Tween
        var tween = ClearImage.rectTransform.DOScale(Vector2.one, 2f);
        tween.SetAutoKill().SetEase(ClearCurve.Curve).OnComplete(() => { OnCompleteFinish(); });

        _isFinish = true;
    }

    public virtual void OnCompleteFinish()
    {
        _isPlay = false;

        if (DataManager.dataBackgroundProcActive)
        {
            //Save Stage Result At GlobalState 
            SaveGameResult();

            // TO DO : Go Panel Result
            SoundManager.Instance.ForceAudioStop();
            UIManager.Instance.UIElementFadePanel.StageToResult();
        }
    }

    public virtual void GoBackSelectStage()
    {
        _isPlay = false;

        if (DataManager.dataBackgroundProcActive)
        {
            //SaveGameResult();

            //Reset GlobalState Value
            ResetGlobalState();

            // TO DO : Go Panel Music Select
            SoundManager.Instance.ForceAudioStop();

            //UIManager.Instance.GoPanelMusicSelect();
            UIManager.Instance.UIElementFadePanel.StageToMusic();
        }
    }

    // TO DO : 게임 플레이 결과 Global Data나 Global Stage에 저장 -> 그 데이터로 결과창 구현 로직 처리
    protected virtual void SaveGameResult()
    {
        // Save Total Play Time
        state.StagePlayTime = (int)_playTime;
        SaveDataClear();
    }

    protected virtual void SaveDataClear()
    {
        int _stageIndex = GlobalState.Instance.StageIndex;
        int _albumIndex = GlobalState.Instance.AlbumIndex;

        switch (_albumIndex)
        {
            case 0: 
                if(!DataManager.dataAlbum1ClearYn[_stageIndex].Equals("P"))
                {
                    DataManager.dataAlbum1ClearYn[_stageIndex] = state.PlayerDeadCount > 0 ? "Y" : "P";
                }
                break;
            case 1:
                if (!DataManager.dataAlbum2ClearYn[_stageIndex].Equals("P"))
                {
                    DataManager.dataAlbum2ClearYn[_stageIndex] = state.PlayerDeadCount > 0 ? "Y" : "P";
                }
                break;
            case 2:
                if (!DataManager.dataAlbum3ClearYn[_stageIndex].Equals("P"))
                {
                    DataManager.dataAlbum3ClearYn[_stageIndex] = state.PlayerDeadCount > 0 ? "Y" : "P";
                }
                break;
            case 3:
                if (!DataManager.dataAlbum4ClearYn[_stageIndex].Equals("P"))
                {
                    DataManager.dataAlbum4ClearYn[_stageIndex] = state.PlayerDeadCount > 0 ? "Y" : "P";
                }
                break;
            case 4:
                if (!DataManager.dataAlbum5ClearYn[_stageIndex].Equals("P"))
                {
                    DataManager.dataAlbum5ClearYn[_stageIndex] = state.PlayerDeadCount > 0 ? "Y" : "P";
                }
                break;
        }

        int _clearCount = 0;
        for (int i = 0; i < DataManager.dataAlbum1ClearYn.Length; i++)
        {
            _clearCount += DataManager.dataAlbum1ClearYn[i] == "Y" || DataManager.dataAlbum1ClearYn[i] == "P" ? 1 : 0;
        }
        for (int j = 0; j < DataManager.dataAlbum2ClearYn.Length; j++)
        {
            _clearCount += DataManager.dataAlbum2ClearYn[j] == "Y" || DataManager.dataAlbum2ClearYn[j] == "P" ? 1 : 0;
        }
        for (int k = 0; k < DataManager.dataAlbum3ClearYn.Length; k++)
        {
            _clearCount += DataManager.dataAlbum3ClearYn[k] == "Y" || DataManager.dataAlbum3ClearYn[k] == "P" ? 1 : 0;
        }
        for (int m = 0; m < DataManager.dataAlbum4ClearYn.Length; m++)
        {
            _clearCount += DataManager.dataAlbum4ClearYn[m] == "Y" || DataManager.dataAlbum4ClearYn[m] == "P" ? 1 : 0;
        }
        for (int n = 0; n < DataManager.dataAlbum5ClearYn.Length; n++)
        {
            _clearCount += DataManager.dataAlbum5ClearYn[n] == "Y" || DataManager.dataAlbum5ClearYn[n] == "P" ? 1 : 0;
        }

        DataManager.dataClearStageCount = _clearCount;

        // 설정 데이터 변경 후 파일 저장
        DataManager.SaveUserData();
    }
#endregion

    // ------------------------------- UI Setting Function -----------------------------------
    public virtual void OnClickPause()
    {
        if (_isPlay)
        {
            _isPause = !_isPause;

            if (_isPause)
            {
                Time.timeScale = 0;
                audioSource.Pause();

                if(videoPlayer) videoPlayer.Pause();
            }
            else
            {
                Time.timeScale = 1;
                audioSource.Play();

                if (videoPlayer) videoPlayer.Play();
            }
        }
    }
}
