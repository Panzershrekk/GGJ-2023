using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwirlSlash : MonoBehaviour
{
    public int Damage;
    public float nextDamageTime;
    private float _nextDamage = 0;

    public void Update()
    {
        if (_nextDamage > 0)
        {
            _nextDamage -= Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (_nextDamage <= 0)
        {
            GameObject triggeredObject = col.gameObject;
            RootBehavior rootBehavior = triggeredObject.GetComponent<RootBehavior>();
            if (rootBehavior != null)
            {
                rootBehavior.TakeDamage(Damage);
                _nextDamage = nextDamageTime;
            }
        }
    }
}
