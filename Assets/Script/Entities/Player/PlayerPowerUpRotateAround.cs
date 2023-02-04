using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpRotateAround : MonoBehaviour
{
    public Transform Player;
    public float RotationSpeed = 200;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Player.position, Vector3.back, RotationSpeed * Time.deltaTime);
    }
}
