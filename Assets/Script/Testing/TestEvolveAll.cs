using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvolveAll : MonoBehaviour
{
    public void EvolveAll()
    {
        var allRoot = FindObjectsOfType(typeof(RootBehavior));
        foreach(RootBehavior r in allRoot)
        {
            r.Evolve();
        }
    }
}
