using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class redscript : MonoBehaviour
{
    // this script will zoom in the camera once knocked out, and it will alter the alpha of the image accordingly to fade in, and out aka red screen
    Camera camerazoom;
    public RawImage imagealpha;
    private void Start()
    {
        // camerazoom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); scrapped the idea for zoom
        StartCoroutine(appear());

    }
    IEnumerator appear() //quickly fade in red
    {
        for (int i = 31; i > 1; i--)
        {
            imagealpha.color = new Color(1, 0, 0, 0 + .5f / i);
        
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void fadeout() //fade out red
    {
       
        StartCoroutine(dissapear());
    }
    IEnumerator dissapear()//fade out red
    {
        for (int i = 1; i < 31; i++)
        {
            
            imagealpha.color = new Color(1, 0, 0, .5f / i );
          
            yield return new WaitForSeconds(0.01f);

        }
        Destroy(gameObject);
    }
}
