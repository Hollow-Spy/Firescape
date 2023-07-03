using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorbreak : MonoBehaviour
{
    /// <summary>
    /// this script is mainly focus on a single scripted event, not a whole not to take not except it uses the knockdown script on the player which is another script that might have more
    /// uses in the future
    /// </summary>
    private bool onetime = true;
    public Rigidbody2D block1, block2,body2;
    public Animator animator;
    public weapon shoty;
    public GameObject fallingsfx,playerbody,impactsfx,woodsfx,fadein;
    public CameraControl camerascript;
    public camerashake shake;
    public playercontrol playercontroller;
    public knockdown knockdownscript;
    public musicmanagerscript music;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && onetime == true)
        {
            onetime = false;
            try
            {
                music.permanentsong();
            }
           
            catch { }
            block1.bodyType = RigidbodyType2D.Dynamic;
        block2.bodyType = RigidbodyType2D.Dynamic;
        block1.AddTorque(Random.Range(0.5f, 1), ForceMode2D.Force);
        block2.AddTorque(Random.Range(-0.5f, -1), ForceMode2D.Force);
        block2.AddForce(new Vector2(0, -.3f),ForceMode2D.Impulse);
            block1.AddForce(new Vector2(0, -.3f), ForceMode2D.Impulse);
            Instantiate(woodsfx, transform.position, Quaternion.identity);
          
            animator.enabled = false;
           
            shoty.enabled = false;
            body2.gravityScale = 0.3f;

            Instantiate(fallingsfx, transform.position, Quaternion.identity);
            StartCoroutine(falling());
            StartCoroutine( camerawork());

        }

    }
  
      IEnumerator camerawork()
    {
        yield return new WaitForSeconds(1.4f);
        camerascript.enabled = false;
        yield return new WaitForSeconds(2.3f);
        camerascript.enabled = true;
    }
        
         
        
    
   
  
    IEnumerator falling()
    {
        float i = 0;
        while (i < 400)
        {
            playerbody.transform.rotation = Quaternion.Euler(0, 0, i);
            i += 2f;
            yield return new WaitForSeconds(0.001f);
        }
        yield return new WaitForSeconds(1);
        Instantiate(impactsfx, transform.position, Quaternion.identity);
        StartCoroutine(shake.Shake(.2f, 0.02f));
        yield return new WaitForSeconds(0.5f);
        Instantiate(fadein, transform.position, Quaternion.identity);
        playerbody.transform.position = new Vector3(30.925f, -1.111f, 0);
        playerbody.transform.rotation = new Quaternion(0, 0, 0, 0);
        shoty.enabled = true;
        body2.gravityScale = .7f;
        knockdownscript.enabled = true;
      
        
       
    }

}
