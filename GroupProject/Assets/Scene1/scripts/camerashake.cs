using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerashake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
      
        Vector3 pos = transform.localPosition;
        float elapsed = 0;
        int reverse=1; //added reverse so it maintains the screen in the middle, found out later that screen shake because of the way it saves the last position of the screen would go off too far, this way it keeps it centered
       //shake came along random y and x axis for duration
        while (elapsed < duration)
        {
            pos = transform.localPosition;
            float x = pos.x + Random.Range(-1, 1) * magnitude * reverse;
            float y = pos.y + Random.Range(-1, 1) * magnitude * reverse;
            transform.localPosition = new Vector3(x, y, pos.z);
            elapsed += Time.deltaTime;
            reverse = reverse * -1;
            yield return null;
        }
        transform.localPosition = pos; //set camera back to last position before started shaking
    }

}
