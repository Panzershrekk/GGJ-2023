using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSetter : MonoBehaviour {
    public SpriteRenderer SpriteRenderer;
    public int BaseLayer;
    void Awake () {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        BaseLayer = SpriteRenderer.sortingOrder;
        SetPosition();
    }

    void Update () {
        SetPosition();
    }

    void SetPosition () {
        /*Vector3 newPosition = transform.position;
        newPosition.z = transform.position.y;
        transform.position = newPosition;*/
        SpriteRenderer.sortingOrder = -(int)transform.position.y + BaseLayer;
    }

}