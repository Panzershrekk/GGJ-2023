using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public void UpdateKillText(int value, bool animated)
    {
        ScoreText.text = value.ToString();
        if (animated == true)
        {
            //BouncyAnim
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
}
