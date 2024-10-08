public class GlobalData : MonoBehaviourSingleton<GlobalData>
{
    public ScriptInformation Information;
    public AlbumInfo Album;
    public ShopInfo Shop;
    public StageInfo StageInfo;
    public ResultInfo ResultInfo;

    public void Start()
    {

    }

    public enum OBJTYPE
    {
        UNKOWN,
        Obstacle,
        SavePoint,
        DodgePoint,
    }

    public enum STAGE
    {
        STAGE1,
        STAGE2,
        STAGE3,
        STAGE4,
        STAGE5,
    }

    public enum ALBUM
    {
        ISEDOL,
        CONTEST,
        GOMIX,
        WAKALOID,
        CONTEST2,
        FESTIVAL,
    }

    public enum UIMODE
    {
        INTRO,
        MAIN,
        SELECT_ALBUM,
        SELECT_MUSIC,
        GAME,
        RESULT,
    }

    public enum SFX
    {
        MainSelect,
        AlbumMove,
        AlbumSelect,
        SettingOut,
        SettingIn,
        VolumeControl,
        Clear,
    }
}
