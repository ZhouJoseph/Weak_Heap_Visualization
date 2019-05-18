using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class nodeControl : MonoBehaviour
{
    public int data, reversebit, havel, haver;
    public Sprite l, r;
    private GameObject right;
    private GameObject left;
    private SpriteRenderer sr;
    public TMP_Text text;


    // Start is called before the first frame update
    void Start()
    {
        right = GameObject.Find("rightArrow");
        left = GameObject.Find("leftArrow");
        sr = gameObject.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = data.ToString();
        if (havel == 1) { left.transform.position = new Vector3(left.transform.position.x, left.transform.position.y, -7); }
        else if (havel == 0) { left.transform.position = new Vector3(left.transform.position.x, left.transform.position.y, 5); }
        if (haver == 1) { right.transform.position = new Vector3(right.transform.position.x, right.transform.position.y, -7); }
        else if (haver == 0) {  right.transform.position = new Vector3(right.transform.position.x, right.transform.position.y, 5); }
    }

    void join(Node self, Node other)
    {
        SpriteRenderer othersr = other.GetComponent<SpriteRenderer>();
        sr.color = Color.yellow;
        othersr.color = Color.yellow;


    }
}
