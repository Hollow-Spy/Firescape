using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class skullytext : MonoBehaviour
{
    public int nbad, ngood; //definition of bad and good in terms of deaths
    public string[] qbad, qok, qgood; //quote for bad, ok and good
    public string[] moneybad, moneyok, moneygood; // quote for good money, ok money and bad
    private Text text;
    private AudioSource voice;


    public void funnymoneyquote(int money, int maxmoney) //money quote
    {
        text = GetComponent<Text>();
        voice = GetComponent<AudioSource>();
        

        if (money >= maxmoney)    //if money is max money, good quote
        {
            int qnumber = Random.Range(0, moneygood.Length - 1);
            StartCoroutine(talk(moneygood[qnumber]));

        }
        else
        {
            if (money > 0 && money < maxmoney) //if money is ok money, ok quote
            {

                int qnumber = Random.Range(0, moneyok.Length - 1);
                StartCoroutine(talk(moneyok[qnumber]));

            }
            else
            {
                if (money == 0) //if money is bad money, bad quote
                {
                    int qnumber = Random.Range(0, moneybad.Length - 1);
                    StartCoroutine(talk(moneybad[qnumber]));
                }
            }
        }

    }

    public void funnyquote(int ndeaths) //funny quote about number of deaths
    {
        text = GetComponent<Text>();
        voice = GetComponent<AudioSource>();

        if (ndeaths <= ngood)
        {
            int qnumber = Random.Range(0, qgood.Length - 1);
            StartCoroutine(talk(qgood[qnumber]));

        }
        else
        {
            if (ndeaths > ngood && ndeaths < nbad)
            {

                int qnumber = Random.Range(0, qok.Length - 1);
                StartCoroutine(talk(qok[qnumber]));

            }
            else
            {
                if (ndeaths >= nbad)
                {
                    int qnumber = Random.Range(0, qbad.Length - 1);
                    StartCoroutine(talk(qbad[qnumber]));
                }
            }
        }

    }

        IEnumerator talk(string quote) //talk couritine with the quote that's gonna be used
        {
            int i = 0;
        text.text = "";
            while (text.text.Length < quote.Length)
            {
                voice.pitch = Random.Range(0.8f, 1);
                voice.Play();
                i += 3;
                if (i > quote.Length)
                {
                    i = quote.Length;
                }
                text.text = quote.Substring(0, i);
                yield return new WaitForSeconds(0.15f);
            }
        }


 }




