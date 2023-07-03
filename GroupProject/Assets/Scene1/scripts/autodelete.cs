using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autodelete : MonoBehaviour
{
    public float time;
    //deletes anything after certain time
 void Start()
    {
        Destroy(gameObject, time);
    }

  
}
