using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogues;
using MainCamera;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Flipper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var friendOne = GameObject.FindGameObjectWithTag("Friend1");
        var friendTwo = GameObject.FindGameObjectWithTag("Friend2");
        var girl = GameObject.FindGameObjectWithTag("Girl");
        if (Math.Abs(friendOne.GetComponent<Mover>().endPosition.x - friendOne.transform.position.x) < 1e-9)
            friendOne.GetComponent<SpriteRenderer>().flipX = true;
        if (Math.Abs(friendTwo.GetComponent<Mover>().endPosition.x - friendTwo.transform.position.x) < 1e-9)
            friendTwo.GetComponent<SpriteRenderer>().flipX = true;
        if (Math.Abs(girl.GetComponent<Mover>().endPosition.x - girl.transform.position.x) < 1e-9)
            girl.GetComponent<SpriteRenderer>().flipX = true;
    }
}
