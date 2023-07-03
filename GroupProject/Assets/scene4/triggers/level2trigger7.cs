using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2trigger7 : MonoBehaviour
{
    public GameObject heart,boomsfx;
    bool once = true;
    camerashake shake;
    CameraControl cameratarget;
    Camera size;
    playercontrol player;
    public weapon shoty;
    footsteps footnoise;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && once== true)
        {
            once = false;
            // getting all the atributes from camera
            shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camerashake>();
            cameratarget = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
            size = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<playercontrol>();
             footnoise = GameObject.FindGameObjectWithTag("Player").GetComponent<footsteps>();


            //disables for cutscene
            player.enabled = false;
            shoty.enabled = false;
            footnoise.enabled = false;

            // start camera work
            
            StartCoroutine(camerainheart());





        }
    }

    IEnumerator camerainheart()
    {
        
        cameratarget.target = heart.transform;
        for(int i =0; i < 1;i++)
        {
            size.orthographicSize += 0.2638822f;
            yield return new WaitForSeconds(0.1f);
        }
        Instantiate(boomsfx, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        shake.StartCoroutine(shake.Shake(1.3f, 0.1f));
        Animator heartanim = heart.GetComponent<Animator>();
        heartanim.Play("boomanim");
        yield return new WaitForSeconds(1.7f);

        StartCoroutine(camerainplayer());
    }

    IEnumerator camerainplayer()
    {
        cameratarget.smoothTime = 0.3f;
        cameratarget.target = player.transform;
        for (int i = 0; i < 1; i++)
        {
            size.orthographicSize -= 0.2638822f;
            yield return new WaitForSeconds(0.1f);
        }
        player.enabled = true;
        shoty.enabled = true;
        footnoise.enabled = true;

        Destroy(heart);
    }

}
