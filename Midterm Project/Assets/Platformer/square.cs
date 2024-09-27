using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class square : MonoBehaviour
{
    public CircleCollider2D circle;

    public bool left;
    public bool right;

    float rad;
    float xDisplacement;

    public float margin;

    Vector3 center;

    private void Awake()
    {
        rad = circle.radius;
        xDisplacement = rad - margin;
        center = circle.transform.position + (Vector3)circle.offset;

        if (left)
        {
            transform.position = center - (Vector3.up * rad) - (Vector3.right * xDisplacement);
        } else if (right)
        {
            transform.position = center - (Vector3.up * rad) + (Vector3.right * xDisplacement);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
