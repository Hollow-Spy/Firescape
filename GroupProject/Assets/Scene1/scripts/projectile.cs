using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public LayerMask solid;
    public GameObject particle;

    private void Start()
    {
        Invoke("Destroyprojectile",lifetime);
    }
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);        //move bullet towards the up 
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position,transform.up,distance,solid);
        if(hitinfo.collider != null)     //if projectile is close enough to ground it will delete itself
        {
            Instantiate(particle, new Vector3(transform.position.x,transform.position.y,transform.position.z - 0.2f), Quaternion.identity);
                Destroyprojectile();
 
        }
    }
    private void Destroyprojectile()
    {  
        Destroy(gameObject);
    }
    
}
