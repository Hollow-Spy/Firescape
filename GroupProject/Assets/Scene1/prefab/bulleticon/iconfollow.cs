using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconfollow : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private float hover=0.003f;
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        transform.localPosition = new Vector3 (player.transform.position.x,player.transform.position.y + 0.2f,player.transform.position.z);
    }

   //simple script to that makes shotgun feedback hover the players head
    void Update()

    {
        
        transform.position = new Vector3(transform.position.x, player.transform.position.y + hover + 0.2f, transform.position.z); ;
        transform.localPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        hover = hover + 0.003f;
    }
}
