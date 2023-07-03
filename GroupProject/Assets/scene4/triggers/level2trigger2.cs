using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2trigger2 : MonoBehaviour
{
    public CircleCollider2D eyecollider;
    private void OnDestroy()
    {
        //simple script for easter egg to enable radius detect on eyetackle
        try
        {
            eyecollider.enabled = true;

        }
        catch
        {

        }
    }
}
