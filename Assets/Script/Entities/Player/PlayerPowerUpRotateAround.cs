using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpRotateAround : MonoBehaviour
{
    public Transform Player;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Player.position, Vector3.back, 200 * Time.deltaTime);
    }
}
