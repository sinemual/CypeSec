using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton Init
    private static GameManager _instance;

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    public static GameManager Instance // Init not in order
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
        _instance = FindObjectOfType<GameManager>();
        _instance.Initialize();
    }
    #endregion
    
    private void Initialize()
    {

    }

    public void GameOver()
    {
        Debug.LogError("Game Over!");
    }
}
