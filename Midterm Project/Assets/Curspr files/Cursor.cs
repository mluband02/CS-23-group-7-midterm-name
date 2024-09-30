using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up") && transform.position.y != 4)
        {
           transform.position += Vector3.up * 2f;
        }
        if (Input.GetKeyDown("down") && transform.position.y != -4)
        {
           transform.position += Vector3.down * 2f;
        }
        if (Input.GetKeyDown("left") && transform.position.x != -7)
        {
           transform.position += Vector3.left * 2f;
        }
        if (Input.GetKeyDown("right") && transform.position.x != 7)
        {
           transform.position += Vector3.right * 2f;
        }


    }
}
