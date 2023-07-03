using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerfakefloor : MonoBehaviour
{
    public GameObject floorbreaking;
    /// <summary>
    /// simply enables another trigger script
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            floorbreaking.SetActive(true);
        }
    }


}
