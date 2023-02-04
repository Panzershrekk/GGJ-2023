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
    private float _currentScaling = 0f;
    private float _nextScaling = 0f;
    private float _elaspedTime;
    private bool _pseudoWin = false;
    private int _killScore = 0;
    public void Update()
    {
        if (IsGameStarted == true && IsGamePaused != true && IsGameOver != false)
        {
            _elaspedTime += Time.deltaTime;
            _nextScaling += Time.deltaTime;
            if (_pseudoWin == false && _elaspedTime > TimeRequiredToWin)
            {
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

    public void StartGame()
    {
        IsGameStarted = true;
        SetKillScore(0);
    }

    public void FinishGame()
    {
        IsGameOver = true;
        if (_pseudoWin == true)
        {
            //win scenarion
        }
        else
        {
            //lost scenario
        }
    }

    public void SetKillScore(int value)
    {
        _killScore = value;
    }

    public void AddScore(int scoreToAdd)
    {
        _killScore += scoreToAdd;
    }

    public float GetCurrentScaling()
    {
        return (_currentScaling);
    }
}
