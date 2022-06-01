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

    private bool isReachEnd = true;
    
    // Start is called before the first frame update
    void Start()
    {
        progress = 0f;
        switch (npc.tag)
        {
            case "Girl":
                npc.GetComponent<Animator>().Play("GirlWalk");
                break;
            case "Friend1":
                npc.GetComponent<Animator>().Play("Friend1Walk");
                break;
            case "Friend2":
                npc.GetComponent<Animator>().Play("Friend2Walk");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, progress);
        progress += step;
        if (Math.Abs(endPosition.x - npc.transform.position.x) < 1e-9
            && Math.Abs(endPosition.y - npc.transform.position.y) < 1e-9)
        {
            switch (npc.tag)
            {
                case "Girl":
                    npc.GetComponent<Animator>().Play("GirlIdle");
                    break;
                case "Friend1":
                    npc.GetComponent<Animator>().Play("Friend1Idle");
                    break;
                case "Friend2":
                    npc.GetComponent<Animator>().Play("Friend2Idle");
                    break;
            }
            enabled = false;
        }
    }
}
