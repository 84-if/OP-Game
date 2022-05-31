using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;

    public int Direction;

    public bool hasStarted;
    
    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKeyDown)
            {
                hasStarted = true;
            } 
        }
        else
        {
            if (Direction == 0)
            {
                transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
            }
            else
            {
                transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
            }
        }
    }
}
