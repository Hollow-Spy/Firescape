using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healhbarscript : MonoBehaviour
{
    Slider slider;
    //simple healthbar script, it slowly depleats the health according to how much damage the boss took
    private void Start()
    {
        slider = GetComponent<Slider>();
    }

   public IEnumerator changehealth(float damage)
    {
        for(int i = 0; i<15;i++)
        {
            slider.value = slider.value - damage / 15;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
