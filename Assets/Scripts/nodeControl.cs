using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class nodeControl : MonoBehaviour
{
    public int data, reversebit, havel, haver;
    public Sprite l, r;
    public GameObject right;
    public GameObject left;
    private SpriteRenderer sr;
    public TMP_Text text;
    public bool yellow = false;
    private float timer = 1;


    // Start is called before the first frame update
    void Start()
    {

        sr = gameObject.GetComponent<SpriteRenderer>();
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

    void highlight()
    {
        yellow = true;
    }

    void reColor()
    {
        if (reversebit == 1) { sr.color = Color.grey; }
        else if (reversebit == 0) { sr.color = Color.white; }
    }


    internal bool join(GameObject gameObject, GameObject other)
    {

        int otherdata = other.GetComponent<nodeControl>().data;
        int selfdata = gameObject.GetComponent<nodeControl>().data;
        gameObject.GetComponent<nodeControl>().highlight();
        other.GetComponent<nodeControl>().highlight();
        Debug.Log("data = " + data);
        Debug.Log(" other = " + otherdata);
        if (selfdata < otherdata)
        {
            int temp = selfdata;
            gameObject.GetComponent<nodeControl>().data = otherdata;
            other.GetComponent<nodeControl>().data = temp;
            Debug.Log("swapped");
            gameObject.GetComponent<nodeControl>().reversebit = 1 - gameObject.GetComponent<nodeControl>().reversebit;

            reColor();
            other.GetComponent<nodeControl>().reColor();
            return true;

        }

        else
        {
            Debug.Log("?????");
            reColor();
            other.GetComponent<nodeControl>().reColor();

            return false;
        }

    }
}
