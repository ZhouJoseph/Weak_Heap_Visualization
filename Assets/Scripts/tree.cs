using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tree : MonoBehaviour
{
    private List<GameObject> treerep;
    private Vector3 nextpos;
    private GameObject newNode;
    private GameObject node;
    private float horiz_shift = 4.0f;
    private float vert_shift = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        treerep = new List<GameObject>();

        node = GameObject.Find("myNode");
        
    }

    internal void startinsert(int data)
    {
        StartCoroutine(insert(data));
    }


    // Update is called once per frame
    void Update()
    {
        //if(Time.timeSinceLevelLoad < 0.02) { insert(4); }
    }

    void caseRight(float x, float y, float z, double h)
    {
        nextpos = new Vector3(x + (float)(horiz_shift/(Math.Pow(2,h-1))), y - vert_shift, z);
    }

    void caseLeft(float x, float y, float z, double h) {
        nextpos = new Vector3(x - (float)(horiz_shift/(Math.Pow(2,h-1))), y - vert_shift, z);
    }

    int findDP(int currindex)
    {
        int curr = currindex;
        while ((curr % 2) == treerep[curr / 2].GetComponent<nodeControl>().reversebit && curr != 0)
        {
            curr /= 2;
        }
        curr /= 2;
        return curr;
    }



    internal IEnumerator insert(int data)
    {
        //Debug.Log("here");
        //get position of new node
        
        if (treerep.Count == 0)
        {
            nextpos = new Vector3(2f, 3.5f, -6.75f);
            newNode = Instantiate(node, nextpos, Quaternion.identity);
            newNode.GetComponent<nodeControl>().data = data;
            treerep.Add(newNode);
            newNode.GetComponent<nodeControl>().left = node;

            yield return new WaitForSeconds(1);
        }
        else {
            GameObject parent = treerep[treerep.Count / 2];
            float x = parent.transform.position.x;
            float y = parent.transform.position.y;
            float z = parent.transform.position.z;
            int haveRight = parent.GetComponent<nodeControl>().haver;
            int haveLeft = parent.GetComponent<nodeControl>().havel;
            double h = Math.Ceiling(Math.Log(treerep.Count + 1, 2));

            if (treerep.Count == 1)
            {
                caseRight(x, y, z, h);
                parent.GetComponent<nodeControl>().haver = 1;
                parent.GetComponent<nodeControl>().right = newNode;
            }

            else
            {
                if (parent.GetComponent<nodeControl>().reversebit == 1)
                {
                    if (haveRight == 1)
                    {
                        //new = left
                        parent.GetComponent<nodeControl>().havel = 1;
                        parent.GetComponent<nodeControl>().left = newNode;
                        caseLeft(x, y, z, h);
                    }
                    else
                    {
                        //new = right
                        parent.GetComponent<nodeControl>().haver = 1;
                        parent.GetComponent<nodeControl>().right = newNode;
                        caseRight(x, y, z, h);

                    }
                }
                else
                {
                    if (haveLeft == 1)
                    {
                        //new = right
                        parent.GetComponent<nodeControl>().haver = 1;
                        parent.GetComponent<nodeControl>().right = newNode;
                        caseRight(x, y, z, h);
                    }
                    else
                    {
                        parent.GetComponent<nodeControl>().havel = 1;
                        parent.GetComponent<nodeControl>().left = newNode;
                        //new = left
                        caseLeft(x, y, z, h);
                    }
                }

            }


            newNode = Instantiate(node, nextpos, Quaternion.identity);
            newNode.transform.parent = parent.transform;
            newNode.GetComponent<nodeControl>().data = data;
            treerep.Add(newNode);
            //Debug.Log(nextpos);
            if (treerep.Count == 2)
            {
                parent.GetComponent<nodeControl>().right = newNode;

            }
            else if (parent.GetComponent<nodeControl>().reversebit == 1)
            {
                if(haveRight == 1)
                {
                    parent.GetComponent<nodeControl>().left = newNode;
                }
                else
                {
                    parent.GetComponent<nodeControl>().right = newNode;
                }

            }
            else
            {
                if (haveLeft == 1)
                {
                    parent.GetComponent<nodeControl>().right = newNode;
                }
                else
                {
                    parent.GetComponent<nodeControl>().left = newNode;
                }
            }
            int currindex = 0;
            if (treerep.Count > 1)
            {
                currindex = treerep.Count - 1;
                while (currindex > 0)
                {

                    int dp = findDP(currindex);

                    if (!newNode.GetComponent<nodeControl>().join(treerep[currindex], treerep[dp]))
                    {
                        break;
                    }
                    else
                    {
                        
                        currindex = dp;
                        yield return new WaitForSeconds(1);
                    }
                }
            }

        }




    }
    internal void startdecrease(int dec1, int dec2)
    {
        StartCoroutine(decrease(dec1, dec2));
    }

     internal IEnumerator decrease(int dec1, int dec2)
    {
        for (int i = 0; i < treerep.Count; i++)
        {
            int currindex = i;
            int dp;
            if(treerep[currindex].GetComponent<nodeControl>().data == dec1)
            {
                treerep[currindex].GetComponent<nodeControl>().data = dec2;
                dp = findDP(currindex);
                while (treerep[currindex].GetComponent<nodeControl>().join(treerep[currindex], treerep[dp]))
                {
                    currindex = dp;
                    dp = findDP(currindex);
                    yield return new WaitForSeconds(1.5f);
                    if(currindex == 0)
                    {
                        break;
                    }
                }
            }
        }
    }

}
