using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class inputManager : MonoBehaviour
{
    public Vector3 currposition;
    public Button addNode, extractMin, decreaseBy;
    public TMP_InputField nodeInput, decInput1, decInput2;
    public int input, dec1, dec2;
    System.String inputstr, dec1str, dec2str;
    public GameObject list;
    public GameObject tree;
    // Start is called before the first frame update
    void Start()
    {
        addNode.onClick.AddListener(() => AddNode(input));
        decreaseBy.onClick.AddListener(() => decrease(dec1, dec2));
        extractMin.onClick.AddListener(() => ExtractMin());
        currposition = new Vector3(-9.5f, -2f, -0.1f);
        
    }

    // Update is called once per frame
    void Update()
    {
        inputstr = nodeInput.text;
        dec1str = decInput1.text;
        dec2str = decInput2.text;
    }

    void AddNode(int input)
    {
        input = Convert.ToInt32(inputstr);
        //Debug.Log("Adding node " + input);
        list.GetComponent<list>().startinsert(input, currposition);
        tree.GetComponent<tree>().startinsert(input);

        currposition += new Vector3(1f, 0, 0);
    }

    void decrease(int dec1, int dec2)
    {
        dec1 = Convert.ToInt32(dec1str);
        dec2 = Convert.ToInt32(dec2str);
        list.GetComponent<list>().startdecrease(dec1, dec2);
    }

    void ExtractMin() {
        list.GetComponent<list>().startextractMin();
        currposition -= new Vector3(1f, 0, 0);
    }
}
