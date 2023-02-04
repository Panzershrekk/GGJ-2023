using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacredFruit : MonoBehaviour
{
    public float SwirlTime = 10f;
    public float FallSpeed = 5.0f;
    public Animator Animator;
    public bool CanBePicked { get; private set; }
    private Vector2 _pointToReach;
    private bool _pointReached = false;
    public void Update()
    {
        if (_pointReached != true)
        {
            var step = FallSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _pointToReach, step);

            if (Vector3.Distance(transform.position, _pointToReach) < 0.001f)
            {
                Animator.Play("Ready");
                CanBePicked = true;
                _pointReached = true;
            }
        }
    }

    public void Setup(float yToReach)
    {
        _pointReached = false;
        CanBePicked = false;
        _pointToReach = new Vector2(this.transform.position.x, this.transform.position.y - yToReach);
    }

    public void PickUp()
    {
        CanBePicked = false;
        Animator.Play("Picked");
        Destroy(this.gameObject, 1.5f);
    }
}
