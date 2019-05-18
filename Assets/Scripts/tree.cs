using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{
    private List<GameObject> treerep;
    private Vector3 nextpos;
    private GameObject newNode;
    private GameObject node;

    // Start is called before the first frame update
    void Start()
    {
        treerep = new List<GameObject>();
        nextpos = transform.position;
        node = GameObject.Find("myNode");
        //insert(3);
        
        
    }
    

    // Update is called once per frame
    void Update()
    {
        //if(Time.timeSinceLevelLoad < 0.02) { insert(4); }
    }
    void insert(int data)
    {
        Debug.Log("here");
        newNode = Instantiate(node, nextpos, Quaternion.identity);
        newNode.GetComponent<nodeControl>().data = data;
        treerep.Add(newNode);
        Debug.Log(nextpos);

        if (treerep.Count == 1) { nextpos += new Vector3(1.3f, -1.62f, 0); }
        else if (treerep.Count == 2) { nextpos += new Vector3(-1.3f, -1.62f, 0); }
        //else if (treerep.Count == 4) { nextpos += new Vector3()}
        //Debug.Log(treerep.Count + "hi");
        Debug.Log(nextpos);
    }
    
}
