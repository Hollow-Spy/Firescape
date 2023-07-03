using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endgame : MonoBehaviour
{
    public GameObject floor,cleared,brush,impactsfx,brushsfx,waitingmusic,time;
    public Animator flooranim, clearedanime;
     
    
    
    void Start()
    {
        floor.transform.localScale = new Vector3(10, 10, 1);
        StartCoroutine(showtext());
    }
    //show texts on the screen, manage sounds effects  etc
    IEnumerator showtext()
    {
        while(floor.transform.localScale.x > 2) 
        {
            floor.transform.localScale = new Vector3(floor.transform.localScale.x -1, floor.transform.localScale.y -1, floor.transform.localScale.z);
            yield return new WaitForSeconds(0.008f);

        }
        Instantiate(impactsfx, transform.position, Quaternion.identity);

        brush.SetActive(true);
        yield return new WaitForSeconds(0.67f);
        cleared.SetActive(true);

        Instantiate(brushsfx, transform.position, Quaternion.identity);
        for(int i = 0;i < 30;i++)
        {
           
            brush.transform.Translate(10, 0, 0);
            yield return new WaitForSeconds(0.001f);

        }
        Instantiate(waitingmusic, transform.position, Quaternion.identity);
        Destroy(brush);
        yield return new WaitForSeconds(1f);
        flooranim.Play("movetoside");
        clearedanime.Play("clearmovetoside");
        time.SetActive(true);



    }
}
