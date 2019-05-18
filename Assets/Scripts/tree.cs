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

    internal void insert(int data)
    {
        Debug.Log("here");
        //get position of new node
        
        if (treerep.Count == 0)
        {
            nextpos = new Vector3(2f, 3.5f, -6.75f);
            newNode = Instantiate(node, nextpos, Quaternion.identity);
            newNode.GetComponent<nodeControl>().data = data;
            treerep.Add(newNode);

            return;
        }

        GameObject parent = treerep[treerep.Count / 2];
        float x = parent.transform.position.x;
        float y = parent.transform.position.y;
        float z = parent.transform.position.z;
        int haveRight = parent.GetComponent<nodeControl>().haver;
        int haveLeft = parent.GetComponent<nodeControl>().havel;
        double h = Math.Ceiling(Math.Log(treerep.Count + 1, 2));

        if (treerep.Count == 1)
        {
            caseRight(x,y,z,h);
            parent.GetComponent<nodeControl>().haver = 1;
        }

        else
        {
            if (parent.GetComponent<nodeControl>().reversebit == 1)
            {
                if (haveRight == 1)
                {
                    //new = left
                    parent.GetComponent<nodeControl>().havel = 1;
                    caseLeft(x,y,z,h);
                }
                else
                {
                    //new = right
                    parent.GetComponent<nodeControl>().haver = 1;
                    caseRight(x,y,z,h);
                    
                }
            }
            else
            {
                if (haveLeft == 1)
                {
                    //new = left
                    parent.GetComponent<nodeControl>().haver = 1;
                    caseRight(x,y,z,h);
                }
                else
                {
                    parent.GetComponent<nodeControl>().havel = 1;
                    //new = right
                    caseLeft(x,y,z,h);
                }
            }

        }

        
        newNode = Instantiate(node, nextpos, Quaternion.identity);
        newNode.GetComponent<nodeControl>().data = data;
        treerep.Add(newNode);
        Debug.Log(nextpos);

        

    }
    
}
