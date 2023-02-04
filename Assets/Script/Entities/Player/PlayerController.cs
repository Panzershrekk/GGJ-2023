using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public Rigidbody2D Rb;
    public float PlayerMoveSpeed = 5.0f;
    private Vector2 _movement;
    private Vector2 _mousePosition;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameStarted == true && GameManager.Instance.IsGamePaused != true && GameManager.Instance.IsPlayerInControl == true)
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

    public void Flip()
    {
        if (_movement.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_movement.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}