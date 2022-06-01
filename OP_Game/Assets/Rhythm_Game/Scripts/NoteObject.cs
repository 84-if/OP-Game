using UnityEngine;

namespace Rhythm_Game.Scripts
{
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
        
            var hit1 = -0.3399999;
            var hit2 = -0.4199999;
            var good1 = -0.3699999;
            var good2 = -0.3899999;
        
            if (Input.GetKeyDown(keyToPress))
            {
                if (canBePressed)
                {
                    gameObject.SetActive(false);
                    
                    var objectPosition = transform.position.y;
                
                    if (Direction == 1)
                    {
                        hit1 = 13.97;
                        hit2 = 13.89;
                        good1 = 13.94;
                        good2 = 13.92;
                        objectPosition = transform.position.x;
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
}
