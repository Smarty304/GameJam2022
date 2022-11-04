using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #region KoopCode
    public enum GameMode
    {
        SinglePlayer,
        LocalMultiplayer
    }

    [Header("Game Mode - Multiplayer"), Space]
    public GameMode currentGameMode;
    //Single Player
    public GameObject inScenePlayer;

    void Start()
    {
        SetupBasedOnGameState();

    }

    void SetupBasedOnGameState()
    {
        switch(currentGameMode)
        {
            case GameMode.SinglePlayer:
                inScenePlayer.name += " (SinglePlayer)";
                break;
        }
    }
    #endregion
}
