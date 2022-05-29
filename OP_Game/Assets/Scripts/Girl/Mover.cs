using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Vector2 startPosition;

    public Vector2 endPosition;

    public float step;

    private float progress;

    private GameObject girl;
    
    // Start is called before the first frame update
    void Start()
    {
        girl = GameObject.FindGameObjectWithTag("Girl");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, progress);
        progress += step;
        if (endPosition.x == girl.transform.position.x)
            girl.GetComponent<Animator>().Play("GirlIdle");
    }
}
