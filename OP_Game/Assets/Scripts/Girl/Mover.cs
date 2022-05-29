using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Mover : MonoBehaviour
{
    public Vector2 startPosition;

    public Vector2 endPosition;

    public float step;

    private float progress;

    public GameObject npc;
    
    // Start is called before the first frame update
    void Start()
    {
        progress = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, progress);
        progress += step;
        if (Math.Abs(endPosition.x - npc.transform.position.x) < 1e-9)
            npc.GetComponent<Animator>().Play("GirlIdle");
    }
}
