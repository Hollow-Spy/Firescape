using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windowscript : MonoBehaviour
{

    public bool inside;
    private Rigidbody2D playerrigidbody,thisrigidbody;
    public Vector3 leftbounds, rightbounds;
    private void Start()
    {
        playerrigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        thisrigidbody = GetComponent<Rigidbody2D>();
    }
  
    
    void Update()
    {
        if(inside == true)
        {
           if(playerrigidbody.velocity.x > 0 || playerrigidbody.velocity.x < 0)
            {                
                thisrigidbody.velocity = new Vector3(playerrigidbody.velocity.x * -1 /7 ,0,0);
            }
           else
            {
                thisrigidbody.velocity = new Vector3(0, 0, 0);
            }

         
        }
        else
        {
            thisrigidbody.velocity = new Vector3(0, 0, 0);

        }
    }
   
}
