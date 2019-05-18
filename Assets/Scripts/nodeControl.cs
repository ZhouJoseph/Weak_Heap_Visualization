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

        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = data.ToString();

        /*
        if (havel == 1) { leftarrow.sprite = l; }
        else if (havel == 0) { leftarrow.sprite = null; }
        if (haver == 1) { rightarrow.sprite = r; }
        else if (haver == 0) { rightarrow.sprite = null; }
        */
    }

    void join(Node self, Node other)
    {
        SpriteRenderer othersr = other.GetComponent<SpriteRenderer>();
        sr.color = Color.yellow;
        othersr.color = Color.yellow;
    }
}
