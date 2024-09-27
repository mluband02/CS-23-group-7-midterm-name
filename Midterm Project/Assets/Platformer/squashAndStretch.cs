using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squashAndStretch : MonoBehaviour
{

    #region variables
    [Tooltip("The physics body this script is attached to")]
    public Rigidbody2D body;
    [Tooltip("The target scale when you land (wide and short)")]
    public Vector3 squash;
    [Tooltip("the target scale while falling (thin and long)")]
    public Vector3 stretch;

    [Tooltip("Scale factor (how fast the size changes)")]
    [Range(0, 1)]
    public float elasticity = 0.2f;

    //Stores some info to correct measurements on awake
    Vector3 init_transform;
    Vector3 actual_squash;
    Vector3 actual_stretch;

    //Regulates when the script knows to be stretched
    float y_threshold = 5; 
    float change_threshold = 10;

    //All in reference to just the y motion
    float velocity_diff = 0;
    float last_velocity = 0;
    float curr_velocity;

    //Stores what this script is currently scaling to
    Vector3 target;

    //Sprite to flip when moving backwards
    SpriteRenderer sprite;
    #endregion

    private void Awake()
    {
        //Makes these values default to nothing if 
            //you forget to set them
        if (squash == Vector3.zero)
        {
            squash = Vector3.one;
        }

        if (stretch == Vector3.zero)
        {
            stretch = Vector3.one;
        }

        //Stores some info on awake
        init_transform = transform.localScale;
        sprite = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate()
    {
        //Scales the input values to match the actual size
        //this prevents errors when rescaling the body
        actual_squash = Vector3.Scale(squash, init_transform);
        actual_stretch = Vector3.Scale(stretch, init_transform);


        determineTarget();
        setSpriteDirection();

        //This code traces a curve towards the target which
        //  asymptotically approaches it (picture a frog jumping halfway 
        //  across a pond forever, it approached the other side quickly at
        //  first and slowly at the end)
        transform.localScale = Vector3.Lerp(transform.localScale, target, elasticity);
    }

    // Decides whether to be going towards squash or stretch
    void determineTarget()
    {
        curr_velocity = body.velocity.y;
        velocity_diff = curr_velocity - last_velocity;
        last_velocity = curr_velocity;

        if (Mathf.Abs(curr_velocity) > y_threshold)
        {
            //If you're falling quickly, stretch
            target = actual_stretch;
        }
        else if (velocity_diff > change_threshold) 
        {
            //If you've just impacted, squash
            transform.localScale = actual_squash;
        }
        else
        {
            //Otherwise, revert to normal
            target = init_transform;
        }
    }

    //If going backwards, flip the sprite to reflect that
    void setSpriteDirection()
    {
        if (body.velocity.x < 0)
        {
            sprite.flipX = true;
        }
        else if (body.velocity.x > 0)
        {
            sprite.flipX = false;
        }
    }
}
