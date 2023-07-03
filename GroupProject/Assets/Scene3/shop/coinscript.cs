using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinscript : MonoBehaviour
{
    public Text text;
    void Start() //simply displays when you first enter the level the amout of coins on the text
    {
        updatecoin();
    }

  public void updatecoin()
    {
        text.text = "x" + PlayerPrefs.GetInt("coins").ToString();
    }
}
