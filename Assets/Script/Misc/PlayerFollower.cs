using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform toFollow;
    
    void Update()
    {
        this.transform.position = toFollow.position;
    }
}
