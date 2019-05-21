using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class nodeControl : MonoBehaviour
{
    public int data, havel, haver;
    public int reversebit = 0;
    public Sprite l, r;
    public GameObject right;
    public GameObject left;
    public SpriteRenderer sr;
    public TMP_Text text;
    public bool yellow = false;
    private float timer = 1;
    private bool switching = false;
    private Vector3 target;
    private float speed = 3f;
    private float startTime;
    private Vector3 originalPos;
    public LineRenderer link;


    // Start is called before the first frame update
    void Start()
    {

        sr = gameObject.GetComponent<SpriteRenderer>();
        startTime = Time.time;
        originalPos = transform.position;

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
        if (!switching) startTime = Time.time;
        else
        {
            float journeyLen = Vector3.Distance(originalPos, target);
            float distcovered = (Time.time - startTime) * speed;
            float fracJourney = distcovered / journeyLen;
            transform.position = Vector3.Lerp(originalPos, target, fracJourney);
            if(Vector3.Distance(transform.position, target) < 0.1f)
            {
                //arrived
                switching = !switching;
                originalPos = target;
                

            }
        }

        if (havel == 1 && haver == 1)
        {
            link.numPositions = 3;
            link.SetPosition(0, left.transform.position);
            link.SetPosition(1, transform.position);
            link.SetPosition(2, right.transform.position);
        }

        else if (havel == 1)
        {
            link.numPositions = 2;
            link.SetPosition(0, transform.position);
            link.SetPosition(1, left.transform.position);

        }
        else if (haver == 1)
        {
            link.numPositions = 2;
            link.SetPosition(0, transform.position);
            link.SetPosition(1, right.transform.position);
        }
        else
        {
            link.numPositions = 1;
        }
    }

    void highlight()
    {
        yellow = true;
    }

    void reColor()
    {
        //Debug.Log("recoloring node" + data);
        if (reversebit == 1) { sr.color = Color.grey; }
        else if (gameObject.GetComponent<nodeControl>().reversebit == 0) { sr.color = Color.white; }
    }


    internal bool join(GameObject gameObject, GameObject other)
    {
        GameObject tempRight;
        
        int otherdata = other.GetComponent<nodeControl>().data;
        int selfdata = gameObject.GetComponent<nodeControl>().data;
        gameObject.GetComponent<nodeControl>().highlight();
        other.GetComponent<nodeControl>().highlight();
        Debug.Log("data = " + selfdata);
        Debug.Log(" other = " + otherdata);
        if (selfdata < otherdata)
        {
            int temp = selfdata;
            gameObject.GetComponent<nodeControl>().data = otherdata;
            other.GetComponent<nodeControl>().data = temp;
            Debug.Log("swapped");
            float delta;

            if (gameObject.GetComponent<nodeControl>().havel == 1)
            {
                delta = gameObject.GetComponent<nodeControl>().left.transform.position.x - gameObject.GetComponent<nodeControl>().transform.position.x;
                Debug.Log("assign value");
            }
            else if (gameObject.GetComponent<nodeControl>().haver == 1)
            {
                delta = gameObject.GetComponent<nodeControl>().right.transform.position.x - gameObject.GetComponent<nodeControl>().transform.position.x;
                Debug.Log("assign value");
            }
            else
            {
                delta = 0;
                Debug.Log("assign 0");
            }

            gameObject.GetComponent<nodeControl>().reversebit = 1 - gameObject.GetComponent<nodeControl>().reversebit;
            Debug.Log("finished??? now rev bit of node" + gameObject.GetComponent<nodeControl>().data + " is " + gameObject.GetComponent<nodeControl>().reversebit);

            int modifier = gameObject.GetComponent<nodeControl>().reversebit == 0 ? 1 : -1;


            if (gameObject.GetComponent<nodeControl>().havel == 1 && gameObject.GetComponent<nodeControl>().haver == 0)
            {

                Debug.Log("switching node" + gameObject.GetComponent<nodeControl>().data + "'s child(ren)" + ", delta = " + delta);
                gameObject.GetComponent<nodeControl>().left.GetComponent<nodeControl>().target = new Vector3(gameObject.GetComponent<nodeControl>().left.transform.position.x + 2 * delta * modifier, gameObject.GetComponent<nodeControl>().left.transform.position.y, gameObject.GetComponent<nodeControl>().left.transform.position.z);
                gameObject.GetComponent<nodeControl>().left.GetComponent<nodeControl>().switching = true;
                gameObject.GetComponent<nodeControl>().right = gameObject.GetComponent<nodeControl>().left;
                gameObject.GetComponent<nodeControl>().left = null;
                gameObject.GetComponent<nodeControl>().haver = 1;
                gameObject.GetComponent<nodeControl>().havel = 0;
            }
            else if (gameObject.GetComponent<nodeControl>().haver == 1 && gameObject.GetComponent<nodeControl>().havel == 0)
            {
                Debug.Log("switching node" + gameObject.GetComponent<nodeControl>().data + "'s child(ren)" + ", delta = " + delta);
                gameObject.GetComponent<nodeControl>().right.GetComponent<nodeControl>().target = new Vector3(gameObject.GetComponent<nodeControl>().right.transform.position.x - 2 * delta * modifier, gameObject.GetComponent<nodeControl>().right.transform.position.y, gameObject.GetComponent<nodeControl>().right.transform.position.z);
                gameObject.GetComponent<nodeControl>().right.GetComponent<nodeControl>().switching = true;

                gameObject.GetComponent<nodeControl>().left = gameObject.GetComponent<nodeControl>().right;
                gameObject.GetComponent<nodeControl>().right = null;
                gameObject.GetComponent<nodeControl>().haver = 0;
                gameObject.GetComponent<nodeControl>().havel = 1;
            }
            else if (gameObject.GetComponent<nodeControl>().haver == 1 && gameObject.GetComponent<nodeControl>().havel == 1) 
            {
                Debug.Log("switching node" + gameObject.GetComponent<nodeControl>().data + "'s child(ren)" + ", delta = " + delta);
                gameObject.GetComponent<nodeControl>().left.GetComponent<nodeControl>().target = new Vector3(gameObject.GetComponent<nodeControl>().left.transform.position.x + 2 * delta * modifier, gameObject.GetComponent<nodeControl>().left.transform.position.y, gameObject.GetComponent<nodeControl>().left.transform.position.z);
                gameObject.GetComponent<nodeControl>().right.GetComponent<nodeControl>().target = new Vector3(gameObject.GetComponent<nodeControl>().right.transform.position.x - 2 * delta * modifier, gameObject.GetComponent<nodeControl>().right.transform.position.y, gameObject.GetComponent<nodeControl>().right.transform.position.z);
                gameObject.GetComponent<nodeControl>().left.GetComponent<nodeControl>().switching = true;
                gameObject.GetComponent<nodeControl>().right.GetComponent<nodeControl>().switching = true;

                tempRight = gameObject.GetComponent<nodeControl>().right;
                gameObject.GetComponent<nodeControl>().right = gameObject.GetComponent<nodeControl>().left;
                gameObject.GetComponent<nodeControl>().left = tempRight;
            }

            Debug.Log("tring to color");
            reColor();
            Debug.Log("colored self");
            other.GetComponent<nodeControl>().reColor();
            return true;

        }

        else
        {
            Debug.Log("did not switch");
            reColor();
            Debug.Log("colored self");
            other.GetComponent<nodeControl>().reColor();

            return false;
        }

    }
}
