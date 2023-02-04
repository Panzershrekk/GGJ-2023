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
    public Animator Animator;
    public float EvolutionTime = 4f;
    public RootSpecific SmallRoot;
    public RootSpecific MediumRoot;
    public RootSpecific BigRoot;
    public SpriteRenderer SpriteRenderer;
    public RootSpawn RootSpawnScript;
    private RootState _currentState;
    private int _currentLife = 0;
    private float _evolutionTimer = 0f;
    private bool _evolving = false;
    private bool _isDead = false;
    public void Start()
    {
        SetupSpawn();
    }

    public void Update()
    {
        if (_currentState != RootState.Big && _isDead == false)
        {
            //Bad mojo mon
            if (_evolving == false && _evolutionTimer >= EvolutionTime - 0.5f)
            {
                Animator.SetTrigger("Evolve");
                _evolving = true;
            }
            if (_evolutionTimer >= EvolutionTime)
            {
                _evolutionTimer = 0;
                Evolve();
            }
            _evolutionTimer += Time.deltaTime;
        }
    }

    public void SetupSpawn()
    {
        _currentState = RootState.Small;
        _currentLife = SmallRoot.LifeGranted;
        SpriteRenderer.sprite = SmallRoot.SpriteChange;
    }

    public void Evolve()
    {
        _evolving = false;
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
        if (_isDead == false)
        {
            _currentLife -= amount;
            if (_currentLife < 0)
            {
                //PLAY DEATH ANIM
                RootSpawnScript.enabled = false;
                _isDead = true;
                GameManager.Instance.AddScore(1);
                Animator.Play("Die");
                Destroy(this.gameObject, 1);
            }
            else
            {
                Animator.Play("Hit");
            }
        }
    }
}

[System.Serializable]
public class RootSpecific
{
    public int LifeGranted = 1;
    public Sprite SpriteChange;
}
