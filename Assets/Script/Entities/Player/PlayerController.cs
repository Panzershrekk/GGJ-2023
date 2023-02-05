using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public Rigidbody2D Rb;
    public SpriteRenderer SpriteRenderer;
    public PlayerBehavior PlayerBehavior;
    public float PlayerMoveSpeed = 5.0f;
    private Vector2 _movement;
    private Vector2 _mousePosition;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameStarted == true && GameManager.Instance.IsGamePaused != true && GameManager.Instance.IsPlayerInControl == true && PlayerBehavior.IsDashing == false)
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
            if (_movement.x != 0 || _movement.y != 0)
            {
                Animator.SetBool("Walking", true);
            }
            else
            {
                Animator.SetBool("Walking", false);
            }
            Flip();
            Rb.velocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.IsGameStarted == true && GameManager.Instance.IsGamePaused != true && GameManager.Instance.IsPlayerInControl == true)
        {
            Rb.MovePosition(Rb.position + _movement * PlayerMoveSpeed * Time.fixedDeltaTime);
        }
    }

    public IEnumerator MakeDash(Vector3 direction, DashSetting setting)
    {
        float dashTime = setting.dashTime;
        float currentTime = 0;
        float currentSpeed = setting.dashSpeed;
        while (currentTime < dashTime)
        {
            transform.position += direction.normalized * currentSpeed * Time.fixedDeltaTime;
            currentTime += Time.fixedDeltaTime;
            if (currentSpeed < setting.maxDashSpeed)
                currentSpeed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        PlayerBehavior.FinishDash();
    }

    public Vector2 GetMovement()
    {
        return _movement;
    }

    public void Flip()
    {
        if (_movement.x > 0)
        {
            SpriteRenderer.flipX = false;
        }
        else if (_movement.x < 0)
        {
            SpriteRenderer.flipX = true;
        }
    }
}