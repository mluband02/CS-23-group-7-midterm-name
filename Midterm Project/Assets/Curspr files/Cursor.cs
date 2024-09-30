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
        if (Input.GetKeyDown("up") && transform.position.y != 4.5)
        {
           transform.position += Vector3.up * 1f;
        }
        if (Input.GetKeyDown("down") && transform.position.y != -4.5)
        {
           transform.position += Vector3.down * 1f;
        }
        if (Input.GetKeyDown("left") && transform.position.x != -7.5)
        {
           transform.position += Vector3.left * 1f;
        }
        if (Input.GetKeyDown("right") && transform.position.x != 7.5)
        {
           transform.position += Vector3.right * 1f;
        }


    }
}
