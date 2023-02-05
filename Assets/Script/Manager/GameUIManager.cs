using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    #region Singleton pattern

    /*
    ** Singleton pattern
    */

    private static GameUIManager _instance;
    public static GameUIManager Instance { get { return _instance; } }


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
    public TMP_Text ScoreText;
    public Image FillRecharge;
    public Image FillLife;
    public Image FillTime;
    public TMP_Text TimeText;
    
    public Animator PowerReadyAnimator;
    public Animator TextBounce;
    public Animator Shine;
    public GameObject ShinePanel;
    public Animator LoadingAnimator;
    public Animator EndResultAnimator;

    public void UpdateKillText(int value, bool animated)
    {
        ScoreText.text = value.ToString();
        if (animated == true)
        {
            TextBounce.Play("Bounce");
        }
    }

    public void UpdateTreeUI(float life, float maxLife)
    {
        float normalizedRemainingLife = life / maxLife;
        FillLife.fillAmount = normalizedRemainingLife;
    }

    public void UpdateRechargeUI(float currentRecharge, float maxTime)
    {
        float normalizedRemainingLife = currentRecharge / maxTime;
        FillRecharge.fillAmount = normalizedRemainingLife;
    }

    public void UpdateElapsedTime(float elaspedTime, float winTime)
    {
        var timeSpan = TimeSpan.FromSeconds(elaspedTime);
        
        TimeText.text = timeSpan.ToString("mm\\:ss");
        FillTime.fillAmount = elaspedTime / winTime;
    }

    public void UpdateTimeUI()
    {
        ShinePanel.SetActive(true);
        Shine.Play("Shiny");
    }

    public void GameDone()
    {
        LoadingAnimator.Play("FadeOutStay");
        StartCoroutine(DisplayFinalUI());
    }

    public IEnumerator DisplayFinalUI()
    {
        yield return new WaitForSeconds (1);
        EndResultAnimator.Play("Display");
    }

    public IEnumerator ChangeScene(int scendIndex)
    {
        EndResultAnimator.Play("Hide");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scendIndex);
    }

    public void PressReplay()
    {
        StartCoroutine(ChangeScene(1));
    }

    public void PressQuit()
    {
        StartCoroutine(ChangeScene(0));
    }
}
