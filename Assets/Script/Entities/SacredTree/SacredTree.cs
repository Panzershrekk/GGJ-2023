using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacredTree : MonoBehaviour
{
    public float MaxLife = 100;
    public float LifeLostOnTouch = 3.0f;
    public float InvincibilityFrame = 0.5f;
    public float DropBonusEverySecond = 30.0f;
    public Animator TruncAnimator;
    public SacredFruit FruitToSpawn;
    public Transform LeftLimit;
    public Transform RightLimit;
    public float MinYToFall = 3.5f;
    public float MaxYToFall = 7;
    private float _currentLife;
    private float _nextBonusTimer = 0;
    private float _currentInvincibilityFrame;
    private bool _isInvincible = false;

    public void Start()
    {
        _currentLife = MaxLife;
    }

    public void Update()
    {
        if (GameManager.Instance.IsGameStarted == true && GameManager.Instance.IsGamePaused != true)
        {
            if (_nextBonusTimer >= DropBonusEverySecond)
            {
                SpawnFruit();
                _nextBonusTimer = 0;
            }
            _nextBonusTimer += Time.deltaTime;
            if (_isInvincible == true)
            {
                _currentInvincibilityFrame -= Time.deltaTime;
                if (_currentInvincibilityFrame <= 0)
                {
                    _isInvincible = false;
                }
            }
        }
    }

    void SpawnFruit()
    {
        float xLeftLimit = LeftLimit.position.x;
        float xRightLimit = RightLimit.position.x;

        float randomXPositionToSpawn = Random.Range(xLeftLimit, xRightLimit);
        Vector2 posToSpawn = new Vector2(randomXPositionToSpawn, LeftLimit.position.y);
        float randomYToSpawn = Mathf.Abs(Random.Range(MinYToFall, MaxYToFall));
        SacredFruit sacredFruit = Instantiate(FruitToSpawn, posToSpawn, Quaternion.identity, null);
        sacredFruit.Setup(randomYToSpawn);
    }

    void LoseLife()
    {
        if (GameManager.Instance.IsGameOver == false)
        {
            _currentLife -= (LifeLostOnTouch + GameManager.Instance.DifficultyScaling);
            _isInvincible = true;
            _currentInvincibilityFrame = InvincibilityFrame;
            GameUIManager.Instance.UpdateTreeUI(_currentLife, MaxLife);
            if (_currentLife <= 0)
            {
                GameManager.Instance.FinishGame();
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        GameObject triggeredObject = col.gameObject;
        RootBehavior rootBehavior = triggeredObject.GetComponent<RootBehavior>();
        if (_isInvincible == false)
        {
            if (rootBehavior != null)
            {
                LoseLife();
            }
        }
        if (rootBehavior != null && rootBehavior.CanDoDamage == true)
        {
            TruncAnimator.SetTrigger("Damage");
        }
    }
}
