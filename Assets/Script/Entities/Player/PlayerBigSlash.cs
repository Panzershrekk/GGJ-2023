using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBigSlash : MonoBehaviour
{
    public int Damage;
    public float Speed = 10f;
    public float TimeToLive = 5.0f;
    [Header("Knockback parameter")]
    private GameObject _belongTo;
    private Vector3 _direction;
    private bool _isSetup;

    void Update()
    {
        if (_isSetup == true)
        {
            transform.position += _direction.normalized * Speed * Time.deltaTime;
        }
    }

    public void Setup(GameObject belongTo, Vector3 direction)
    {
        _belongTo = belongTo;
        _direction = direction;
        _isSetup = true;
        Destroy(this.gameObject, TimeToLive);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject triggeredObject = col.gameObject;
        if (triggeredObject != _belongTo)
        {
           RootBehavior rootBehavior = triggeredObject.GetComponent<RootBehavior>();
            if (rootBehavior != null)
            {
                rootBehavior.TakeDamage(Damage);
            }
        }
    }
}
