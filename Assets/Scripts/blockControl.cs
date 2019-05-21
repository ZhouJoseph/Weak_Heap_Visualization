using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class blockControl : MonoBehaviour
{
    public int reversebit = 0;
    public int data;
    public TMP_Text text;
    public SpriteRenderer sr;
    public bool yellow = false;
    private float timer = 1;

    // Start is called before the first frame update

    void Start()
    {
        Application.targetFrameRate = 1;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = data.ToString();
        if (yellow && timer > 0)
        {
            sr.color = Color.yellow;
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 1;
                yellow = false;
            }
        }
        else reColor();
    }



    void reColor()
    {
        if (reversebit == 1) { sr.color = Color.grey; }
        else if (reversebit == 0) { sr.color = Color.white; }
    }

    internal bool join(GameObject gameObject, GameObject other)
    {

        int otherdata = other.GetComponent<blockControl>().data;
        int selfdata = gameObject.GetComponent<blockControl>().data;
        gameObject.GetComponent<blockControl>().highlight();
        other.GetComponent<blockControl>().highlight();
        //Debug.Log("data = " + data);
        //Debug.Log(" other = " + otherdata);
        if (selfdata < otherdata)
        {
            int temp = selfdata;
            gameObject.GetComponent<blockControl>().data = otherdata;
            other.GetComponent<blockControl>().data = temp;
            //Debug.Log("swapped");
            gameObject.GetComponent<blockControl>().reversebit = 1 - gameObject.GetComponent<blockControl>().reversebit;

            reColor();
            other.GetComponent<blockControl>().reColor();
            return true;

        }

        else
        {
            //Debug.Log("?????");
            reColor();
            other.GetComponent<blockControl>().reColor();

            return false;
        }

    }

    void highlight()
    {

        yellow = true;
    }
}


