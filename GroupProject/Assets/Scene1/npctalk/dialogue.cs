using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dialogue : MonoBehaviour
{
    [Serializable]
    public class facials
    {
        public Texture[] face;               ///controll which faces we want to switch between during a certain line, can have more than one
        public facials()
        {

        }
    }


    public facials[] facialsmanager = new facials[2];     //declaration of ^^
    public Texture initialface;
    public AudioClip voice;
    [TextArea(3,10)]
    public string[] lines;

}
