using UnityEngine;

[CreateAssetMenu(fileName = "SetupAlbumInfo", menuName = "ScriptableObjects/SetupAlbumInfo", order = 2)]
public class AlbumInfo : ScriptableObject
{
    [Header("[ Album List (Prefabs) ]")]
    public GameObject[] AlbumLists;

    [Space(10)]
    [Header("[ Album BackGround Images ]")]
    public Sprite[] AlbumBackgournds;

    [Space(10)]
    [Header("[ Album Circle Images ]")]
    public Sprite[] AlbumCircles;

    [Space(10)]
    [Header("[ First Album Music Information ]")]
    public Sprite[] FirstAlbumMusicBackground;
    public Sprite[] FirstAlbumMusicCircle;
    public Sprite[] FirstAlbumMusicLevel;

    [Space(10)]
    [Header("[ Second Album Music Information ]")]
    public Sprite[] SecondAlbumMusicBackground;
    public Sprite[] SecondAlbumMusicCircle;
    public Sprite[] SecondAlbumMusicLevel;

    [Space(10)]
    [Header("[ Third Album Music Information ]")]
    public Sprite[] ThirdAlbumMusicBackground;
    public Sprite[] ThirdAlbumMusicCircle;
    public Sprite[] ThirdAlbumMusicLevel;

    [Space(10)]
    [Header("[ Forth Album Music Information ]")]
    public Sprite[] ForthAlbumMusicBackground;
    public Sprite[] ForthAlbumMusicCircle;
    public Sprite[] ForthAlbumMusicLevel;
}
