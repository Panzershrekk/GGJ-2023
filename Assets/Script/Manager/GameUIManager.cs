using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        
    }
}
