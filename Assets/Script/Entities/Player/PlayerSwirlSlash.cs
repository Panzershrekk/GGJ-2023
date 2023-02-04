using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwirlSlash : MonoBehaviour
{
    public int Damage;

    void OnTriggerStay2D(Collider2D col)
    {
        GameObject triggeredObject = col.gameObject;
        RootBehavior rootBehavior = triggeredObject.GetComponent<RootBehavior>();
        if (rootBehavior != null)
        {
            rootBehavior.TakeDamage(Damage);
        }
    }
}
