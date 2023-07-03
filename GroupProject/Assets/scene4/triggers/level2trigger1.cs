using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2trigger1 : MonoBehaviour
{
    public eyetackle eyetackle1, eyetackle2;
    public CircleCollider2D eyetackle1collider;
    private bool once = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && once == true) //very first trigger, it will call on to 2 eye tackle's behaviours and make them show up, then auto destroys
        {
            once = false;
            eyetackle1collider.enabled = true;
            eyetackle1.range = true;
            eyetackle1.behaviour();
            eyetackle2.range = true;
            eyetackle2.behaviour();
            StartCoroutine(waitasecond());

        }
        IEnumerator waitasecond()
        {
            yield return new WaitForSeconds(.1f);
            eyetackle2.range = false;
            eyetackle1.range = false;
            Destroy(gameObject);
        }
    }
}
