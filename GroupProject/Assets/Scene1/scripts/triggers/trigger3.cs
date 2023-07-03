using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger3 : MonoBehaviour
{
    /// <summary>
    /// script also in charge of setting the camera on the right place and beggining the fire rise event, also 1 time use
    /// </summary>
   
    public CameraControl camerascript;
    public Camera camerasize;
    public GameObject camerachase;
    public firequick firerise;
    private gamemaster checkpointset;
    private bool onetime = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && onetime == true)
        {
            onetime = false;
        StartCoroutine(cameraresize());
        camerascript.target = camerachase.transform;          
        StartCoroutine(firerise.start());
        checkpointset = GameObject.FindGameObjectWithTag("gm").GetComponent<gamemaster>(); //also sets up the check point to its position
        checkpointset.checkpoint = new Vector3(30.902f, -1.139f, -2.1f);
        }
    }
    IEnumerator cameraresize()
    {
        while(camerasize.orthographicSize < 1)
        {
            camerasize.orthographicSize += 0.01f;
            yield return null;
        }
      // Destroy(gameObject);
    }
}
