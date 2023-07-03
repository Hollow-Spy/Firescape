using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger4 : MonoBehaviour
{
    private bool onetime = true;
    private gamemaster checkpointset;
    //trigger will focus the camera back to the player, set checkpoint and lower music volume
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && onetime == true)
        {
            onetime = false;
            CameraControl camerascript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            AudioSource music = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
            music.volume = music.volume / 2;
            camerascript.target = player.transform;
            checkpointset = GameObject.FindGameObjectWithTag("gm").GetComponent<gamemaster>();
            checkpointset.checkpoint = new Vector3(32.902f, 7.558f, -2.1f);
            StartCoroutine(cameraresize());
        }
        IEnumerator cameraresize()
        {
            Camera camerazoom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            while (camerazoom.orthographicSize > 0.5665228)
            {
                camerazoom.orthographicSize -= 0.01f;
                yield return null;
            }
            camerazoom.orthographicSize = 0.5665228f;
            Destroy(gameObject);
        }
    }
}
