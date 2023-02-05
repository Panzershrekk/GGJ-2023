using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Animator Animator;
    public float BigSlashRechargeTime = 8.0f;
    public float NormalVectorAttackSpawnMultiplicative = 2.0f;
    public PlayerSlash Slash;
    public PlayerBigSlash BigSlash;
    public PlayerFollower FollowingSwirl;
    public Transform AttackOrigin;
    private float _swirlRemainingTime = 0f;
    private float _currentBigSlashTime = 0;
    private bool _canBigSlash = false;

    void Update()
    {
        if (GameManager.Instance.IsGameStarted == true && GameManager.Instance.IsGamePaused != true && GameManager.Instance.IsPlayerInControl == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PrepareAttack(true);
            }
            if (Input.GetMouseButtonDown(1) && _canBigSlash == true)
            {
                PrepareAttack(false);
                _canBigSlash = false;
                _currentBigSlashTime = 0;
            }
            if (_canBigSlash == false)
            {
                GameUIManager.Instance.UpdateRechargeUI(_currentBigSlashTime, BigSlashRechargeTime);
                if (_currentBigSlashTime >= BigSlashRechargeTime)
                {
                    _currentBigSlashTime = BigSlashRechargeTime;
                    _canBigSlash = true;
                }
                else
                {
                    _currentBigSlashTime += Time.deltaTime;
                }
            }
        }
        if (FollowingSwirl.gameObject.activeInHierarchy == true)
        {
            _swirlRemainingTime -= Time.deltaTime;
            if (_swirlRemainingTime <= 0)
            {
                FollowingSwirl.gameObject.SetActive(false);
            }
            
        }
    }

    void PrepareAttack(bool isLeftClick)
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos = (targetPos - transform.position).normalized;
        targetPos.z = 0;
        float AngleRad = Mathf.Atan2(targetPos.y, targetPos.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        if (isLeftClick == true)
        {
            Attack(targetPos, AngleDeg);
        }
        else
        {
            AttackBig(targetPos, AngleDeg);
        }
    }

    void Attack(Vector3 targetPos, float angleDeg)
    {
        Animator.Play("Attack");
        Vector3 attackOriginatingFrom = new Vector3(transform.position.x, transform.position.y + AttackOrigin.position.y);
        PlayerSlash createdProj = Instantiate(Slash, attackOriginatingFrom + targetPos.normalized * NormalVectorAttackSpawnMultiplicative, Quaternion.identity, null);
        createdProj.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        createdProj.Setup(this.gameObject);
    }

    void AttackBig(Vector3 targetPos, float angleDeg)
    {
        Animator.Play("Attack");
        Vector3 attackOriginatingFrom = new Vector3(transform.position.x, transform.position.y + AttackOrigin.position.y);
        PlayerBigSlash createdProj = Instantiate(BigSlash, attackOriginatingFrom + targetPos.normalized * NormalVectorAttackSpawnMultiplicative, Quaternion.identity, null);
        createdProj.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        createdProj.Setup(this.gameObject, targetPos);
    }

    void ActivateSwirl(float time)
    {
        _swirlRemainingTime = time;
        FollowingSwirl.gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject triggeredObject = col.gameObject;
        SacredFruit sacredFruit = triggeredObject.GetComponent<SacredFruit>();
        if (sacredFruit != null && sacredFruit.CanBePicked == true)
        {
            ActivateSwirl(sacredFruit.SwirlTime);
            sacredFruit.PickUp();
        }
    }
}
