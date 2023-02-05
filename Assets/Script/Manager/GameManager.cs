using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// THIS CLASS US SINGLETON PATTERN
//

public class GameManager : MonoBehaviour
{
    #region Singleton pattern

    /*
    ** Singleton pattern
    */

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public bool IsGameStarted = false;
    public bool IsGameOver = false;
    public bool IsGamePaused = false;
    public bool IsPlayerInControl = true;

    public float TimeRequiredToWin = 60;
    
    public float DifficultyScaling = 0.1f;
    public float TimeForScalingInSecond = 10f;
    private float _currentScaling = 1f;
    private float _nextScaling = 0f;
    private float _elaspedTime;
    private bool _pseudoWin = false;
    private int _killScore = 0;
    public void Update()
    {
        if (IsGameStarted == true && IsGamePaused == false && IsGameOver == false)
        {
            _elaspedTime += Time.deltaTime;
            GameUIManager.Instance.UpdateElapsedTime(_elaspedTime, TimeRequiredToWin);
            _nextScaling += Time.deltaTime;
            if (_pseudoWin == false && _elaspedTime > TimeRequiredToWin)
            {
                GameUIManager.Instance.UpdateTimeUI();
                //Popup de hardmode ?
                _pseudoWin = true;
            }
            if (_nextScaling >= TimeForScalingInSecond)
            {
                _currentScaling += DifficultyScaling;
                _nextScaling = 0;
            }
        }
    }

    public void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        IsGameStarted = true;
        SetKillScore(0);
        _currentScaling = 1;
    }

    public void FinishGame()
    {
        IsGameOver = true;
        IsPlayerInControl = false;
        GameUIManager.Instance.GameDone(_pseudoWin);
    }

    public void SetKillScore(int value)
    {
        _killScore = value;
        GameUIManager.Instance.UpdateKillText(_killScore, false);
    }

    public void AddScore(int scoreToAdd)
    {
        _killScore += scoreToAdd;
        GameUIManager.Instance.UpdateKillText(_killScore, true);

    }

    public float GetCurrentScaling()
    {
        return (_currentScaling);
    }
}
