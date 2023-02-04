using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBehavior : MonoBehaviour
{
    public enum RootState
    {
        Small = 0,
        Medium = 1,
        Big = 2,
    }

    public RootSpecific SmallRoot;
    public RootSpecific MediumRoot;
    public RootSpecific BigRoot;
    public SpriteRenderer SpriteRenderer;
    public RootSpawn RootSpawnScript;
    private RootState _currentState;
    private int _currentLife = 0;
    public void Start()
    {
        SetupSpawn();
    }

    public void SetupSpawn()
    {
        _currentState = RootState.Small;
        _currentLife = SmallRoot.LifeGranted;
        SpriteRenderer.sprite = SmallRoot.SpriteChange;
    }

    public void Evolve()
    {
        if (_currentState == RootState.Small)
        {
            _currentState = RootState.Medium;
            _currentLife += MediumRoot.LifeGranted;
            SpriteRenderer.sprite = MediumRoot.SpriteChange;
            //Play anim
        }
        else if (_currentState == RootState.Medium)
        {
            _currentState = RootState.Big;
            _currentLife += BigRoot.LifeGranted;
           SpriteRenderer.sprite = BigRoot.SpriteChange;
            //Play anim
        }
    }

    public void TakeDamage(int amount)
    {
        _currentLife -= amount;
        if (_currentLife < 0)
        {
            //PLAY DEATH ANIM
            Destroy(this.gameObject);
        }
    }
}

[System.Serializable]
public class RootSpecific
{
    public int LifeGranted = 1;
    public Sprite SpriteChange;
}
