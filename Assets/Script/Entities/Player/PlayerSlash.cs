using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    public int Damage;
    private GameObject _belongTo;

    public void Setup(GameObject belongTo)
    {
        _belongTo = belongTo;
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
