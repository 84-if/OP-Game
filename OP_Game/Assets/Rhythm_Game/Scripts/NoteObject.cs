using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public int Direction;
    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    
    
    void Start()
    {
        
    }

    void Update()
    {
        
        var objectPosition = transform.position.y;
        var hit1 = objectPosition + 0.04;
        var hit2 = objectPosition - 0.04;
        var good1 = objectPosition + 0.01;
        var good2 = objectPosition - 0.01;
        
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                
                if (Direction == 1)
                {
                    objectPosition = transform.position.x;
                    hit1 = objectPosition + 0.04;
                    hit2 = objectPosition - 0.04;
                    good1 = objectPosition + 0.01;
                    good2 = objectPosition - 0.01;
                }

                //if (transform.position.y > -0.3399999 || transform.position.y < -0.4199999) 0.08
                if (objectPosition > hit1 || objectPosition < hit2)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                //else if(transform.position.y > -0.3699999 || transform.position.y < -0.3899999) 0.02
                else if(objectPosition > good1 || objectPosition < good2)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeSelf)
        {
            if (other.tag == "Activator")
            {
                canBePressed = false;
            
                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }
    }
}
