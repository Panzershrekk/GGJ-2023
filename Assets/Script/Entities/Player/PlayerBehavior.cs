using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Animator Animator;
    public PlayerSlash Slash;
    public PlayerBigSlash BigSlash;
    void Update()
    {
        if (GameManager.Instance.IsGameStarted == true && GameManager.Instance.IsGamePaused != true && GameManager.Instance.IsPlayerInControl == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 targetPos = Input.mousePosition;
                targetPos.z = Camera.main.nearClipPlane;
                targetPos = Camera.main.ScreenToWorldPoint(targetPos);
                //Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos = (targetPos - transform.position).normalized;
                targetPos.z = this.transform.position.z;
                float AngleRad = Mathf.Atan2(targetPos.y, targetPos.x);
                float AngleDeg = (180 / Mathf.PI) * AngleRad;
                Attack(targetPos, AngleDeg);
            }
        }
    }

    void Attack(Vector3 targetPos, float angleDeg)
    {
        /*Animator.Play("Attack");
        ProjectileBehavior createdProj = Instantiate(projectile, transform.position, Quaternion.identity, null);
        createdProj.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        createdProj.Setup(entity, this.gameObject, targetPos);*/
    }

    void AttackBig(Vector3 targetPos, float angleDeg)
    {
        Debug.Log("Big shack");
        Animator.Play("Attack");
        PlayerBigSlash createdProj = Instantiate(BigSlash, transform.position, Quaternion.identity, null);
        createdProj.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        createdProj.Setup(this.gameObject, targetPos);
    }
}
