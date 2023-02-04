using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Animator Animator;
    public float NormalVectorAttackSpawnMultiplicative = 2.0f;
    public PlayerSlash Slash;
    public PlayerBigSlash BigSlash;
    public PlayerFollower FollowingSwirl;

    private float _swirlRemainingTime = 0f;
    void Update()
    {
        if (GameManager.Instance.IsGameStarted == true && GameManager.Instance.IsGamePaused != true && GameManager.Instance.IsPlayerInControl == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PrepareAttack(true);
            }
            if (Input.GetMouseButtonDown(1))
            {
                PrepareAttack(false);
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
        Vector3 targetPos = Input.mousePosition;
        targetPos.z = Camera.main.nearClipPlane;
        targetPos = Camera.main.ScreenToWorldPoint(targetPos);
        targetPos = (targetPos - transform.position).normalized;
        targetPos.z = this.transform.position.z;
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
        PlayerSlash createdProj = Instantiate(Slash, transform.position + targetPos * NormalVectorAttackSpawnMultiplicative, Quaternion.identity, null);
        createdProj.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        createdProj.Setup(this.gameObject);
    }

    void AttackBig(Vector3 targetPos, float angleDeg)
    {
        Animator.Play("Attack");
        PlayerBigSlash createdProj = Instantiate(BigSlash, transform.position + targetPos * NormalVectorAttackSpawnMultiplicative, Quaternion.identity, null);
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
