using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class list : MonoBehaviour
{
    private List<GameObject> listrep;
    public GameObject block;
    Vector3 currposition;
    // Start is called before the first frame update
    void Start()
    {
        listrep = new List<GameObject>();
        currposition = new Vector3(transform.position.x, transform.position.y, transform.position.z); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    internal void startinsert(int data, Vector3 position)
    {
        StartCoroutine(insert(data, position));
    }
    
    internal void startdecrease(int dec1, int dec2)
    {
        StartCoroutine(decrease(dec1, dec2));
    }

    internal void startextractMin()
    {
        StartCoroutine(extractMin());
    }

    internal IEnumerator insert(int data, Vector3 position)
    {
        int currindex;
        int dp;
        GameObject curr = Instantiate(block, position, Quaternion.identity);
        curr.GetComponent<blockControl>().data = data;
        Debug.Log("new node at " + position);
        
        listrep.Add(curr);


        if (listrep.Count > 1)
        {
            currindex = listrep.Count - 1;
            while (currindex > 0)
            {

                dp = findDP(currindex);
                Debug.Log("curr = " + currindex + " dp = " + dp);
                
                if (!curr.GetComponent<blockControl>().join(listrep[currindex], listrep[dp]))
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

    int findDP(int currindex)
    {
        int curr = currindex;
        GameObject dp;
        while ((curr % 2) == listrep[curr/2].GetComponent<blockControl>().reversebit && curr != 0)
        {
            curr /= 2;
        }
        curr /= 2;
        return curr;
    }

    internal IEnumerator decrease(int dec1, int dec2)
    {
        for (int i = 0; i < listrep.Count; i++)
        {
            int currindex = i;
            int dp;
            if(listrep[currindex].GetComponent<blockControl>().data == dec1)
            {
                listrep[currindex].GetComponent<blockControl>().data = dec2;
                dp = findDP(currindex);
                while (listrep[currindex].GetComponent<blockControl>().join(listrep[currindex], listrep[dp]))
                {
                    currindex = dp;
                    dp = findDP(currindex);
                    yield return new WaitForSeconds(1);
                    if(currindex == 0)
                    {
                        break;
                    }
                }
            }
        }
    }

    int findRootSibling() {
        int index = 1;
        while(index < listrep.Count) {
            index = listrep[index].GetComponent<blockControl>().reversebit + 2 * index;
        }
        return index/2;
    }


    internal IEnumerator extractMin() {
        /* 
         * 1.remove min 
         * 2.replace min
         * 3.sift down
         */

        // remove & replace min
        int toReplace = listrep.Count - 1;
        int temp = listrep[0].GetComponent<blockControl>().data;
        listrep[0].GetComponent<blockControl>().data = listrep[toReplace].GetComponent<blockControl>().data;
        listrep[toReplace].GetComponent<blockControl>().data = temp;
        yield return new WaitForSeconds(1);
        Destroy(listrep[toReplace]);
        listrep.RemoveAt(toReplace);

        // sift down
        toReplace = findRootSibling();
        int dp = findDP(toReplace);

        while (toReplace / 2 != dp)
        {
            dp = findDP(toReplace);
            listrep[dp].GetComponent<blockControl>().join(listrep[toReplace], listrep[dp]);
            yield return new WaitForSeconds(1);
            toReplace /= 2;
        }

        listrep[dp].GetComponent<blockControl>().join(listrep[toReplace], listrep[dp]);
        yield return new WaitForSeconds(1);

    }

}
