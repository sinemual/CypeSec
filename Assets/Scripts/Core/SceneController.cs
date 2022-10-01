using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    #region Singleton Init
    private static SceneController _instance;

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static SceneController Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }

    static void Init() // Init script
    {
        _instance = FindObjectOfType<SceneController>();
        _instance.Initialize();
    }
    #endregion

    public enum GameState { Playing, Building, Mining, Pause };
    public GameState currentGameState;
    public Cueen cueen;
    public TileData enterDungeon;
    public GameObject patrolPoint;

    void Initialize()
    {
        currentGameState = GameState.Playing;
        AudioManager.Instance.Play("ost2");
    }

    [NaughtyAttributes.Button]
    public void BuildModeToggle()
    {
        if (currentGameState != GameState.Pause)
            currentGameState = currentGameState == GameState.Building ? GameState.Playing : GameState.Building;

        if (currentGameState == GameState.Building)
        {
            UIManager.Instance.HideAll();
            BuildingsManager.Instance.ActivateBuildPlaces();
        }
        else
        {
            BuildingsManager.Instance.DisableBuildPlaces();
        }
    }

    public void Alarm()
    {
        Debug.Log("ALARM!");
    }
}
