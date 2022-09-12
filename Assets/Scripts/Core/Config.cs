public class Config : MonoBehaviourSingleton<Config>
{
    //------------------------- URL ---------------------------------
    //WakZoo
    public string WakZoo { get; internal set; }

    //Origin URL
    public string Origin_Rewind { get; internal set; }
    public string Origin_WinterSpring { get; internal set; }

    //BackGroundMusic
    public string BGM_01 { get; internal set; }
    public string BGM_02 { get; internal set; }
    public string BGM_03 { get; internal set; }

    //ReMix URL
    //Music       
    public string ReMix_Rewind { get; internal set; }
    public string ReMix_WinterSpring { get; internal set; }
                  
    public string ReMix_NobleLick { get; internal set; }
    public string ReMix_Wakaloid { get; internal set; }
    public string ReMix_WakGoodAroma100 { get; internal set; }
    public string ReMix_AvantGarde { get; internal set; }

    public string ReMix_YouHi { get; internal set; }
    public string ReMix_Gotterfly { get; internal set; }
    public string ReMix_KingADance { get; internal set; }
    public string ReMix_IPad { get; internal set; }
    public string ReMix_ReviveLikeADog { get; internal set; }
                  
    public string ReMix_GalUseGirl { get; internal set; }
    public string ReMix_BangOff { get; internal set; }
    public string ReMix_TwistedLove { get; internal set; }
    public string ReMix_Waklio { get; internal set; }
    private void Awake()
    {
        //URL
        WakZoo = string.Empty;

        Origin_Rewind = string.Empty;
        Origin_WinterSpring = string.Empty;

        BGM_01 = string.Empty;
        BGM_02 = string.Empty;
        BGM_03 = string.Empty;

        ReMix_Rewind = string.Empty;
        ReMix_WinterSpring = string.Empty;

        ReMix_NobleLick = string.Empty;
        ReMix_Wakaloid = string.Empty;
        ReMix_WakGoodAroma100 = string.Empty;
        ReMix_AvantGarde = string.Empty;

        ReMix_YouHi = string.Empty;
        ReMix_Gotterfly = string.Empty;
        ReMix_KingADance = string.Empty;
        ReMix_IPad = string.Empty;
        ReMix_ReviveLikeADog = string.Empty;

        ReMix_GalUseGirl = string.Empty;
        ReMix_BangOff = string.Empty;
        ReMix_TwistedLove = string.Empty;
        ReMix_Waklio = string.Empty;

        LoadConfig();
    }

    void LoadConfig()
    {
        IniFile iniFile = new IniFile();
        iniFile.Load("config.ini");

        WakZoo = iniFile["URL"]["WakZoo"].ToString().Trim();

        Origin_Rewind = iniFile["URL"]["Origin_Rewind"].ToString().Trim();
        Origin_WinterSpring = iniFile["URL"]["Origin_WinterSpring"].ToString().Trim();

        BGM_01 = iniFile["URL"]["BGM_01"].ToString().Trim();
        BGM_02 = iniFile["URL"]["BGM_02"].ToString().Trim();
        BGM_03 = iniFile["URL"]["BGM_03"].ToString().Trim();

        ReMix_Rewind = iniFile["URL"]["ReMix_Rewind"].ToString().Trim();
        ReMix_WinterSpring = iniFile["URL"]["ReMix_WinterSpring"].ToString().Trim();

        ReMix_NobleLick = iniFile["URL"]["ReMix_NobleLick"].ToString().Trim();
        ReMix_Wakaloid = iniFile["URL"]["ReMix_Wakaloid"].ToString().Trim();
        ReMix_WakGoodAroma100 = iniFile["URL"]["ReMix_WakGoodAroma100"].ToString().Trim();
        ReMix_AvantGarde = iniFile["URL"]["ReMix_AvantGarde"].ToString().Trim();

        ReMix_YouHi = iniFile["URL"]["ReMix_YouHi"].ToString().Trim();
        ReMix_Gotterfly = iniFile["URL"]["ReMix_Gotterfly"].ToString().Trim();
        ReMix_KingADance = iniFile["URL"]["ReMix_KingADance"].ToString().Trim();
        ReMix_IPad = iniFile["URL"]["ReMix_IPad"].ToString().Trim();
        ReMix_ReviveLikeADog = iniFile["URL"]["ReMix_ReviveLikeADog"].ToString().Trim();

        ReMix_GalUseGirl = iniFile["URL"]["ReMix_GalUseGirl"].ToString().Trim();
        ReMix_BangOff = iniFile["URL"]["ReMix_BangOff"].ToString().Trim();
        ReMix_TwistedLove = iniFile["URL"]["ReMix_TwistedLove"].ToString().Trim();
        ReMix_Waklio = iniFile["URL"]["ReMix_Waklio"].ToString().Trim();
    }

}
