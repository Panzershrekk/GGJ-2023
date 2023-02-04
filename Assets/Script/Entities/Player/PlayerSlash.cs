using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    public int Damage;
    private GameObject _belongTo;
    public float TimeToLive = 0.1f;
    private List<RootBehavior> _hitList = new List<RootBehavior>();

    public void Setup(GameObject belongTo)
    {
        _belongTo = belongTo;
        Destroy(this.gameObject, TimeToLive);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        CheckTriggerAttack(col);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        CheckTriggerAttack(col);
    }

    void CheckTriggerAttack(Collider2D col)
    {
        GameObject triggeredObject = col.gameObject;
        if (triggeredObject != _belongTo)
        {
            RootBehavior rootBehavior = triggeredObject.GetComponent<RootBehavior>();
            if (rootBehavior != null &&  !_hitList.Contains(rootBehavior))
            {
                _hitList.Add(rootBehavior);
                rootBehavior.TakeDamage(Damage);
            }
        }
    }

    void OnDestroy()
    {
        _hitList.Clear();
    }
}
