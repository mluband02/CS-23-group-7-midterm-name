using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampoline : MonoBehaviour
{
    
    #region variables

    [Tooltip("Speed the player travels upwards after colliding with trampoline")]
    public float trampoline_impulse;
    
    #endregion
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {    
        print ("uhhh");
        if (collision.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            print("ENTERED BOX");
            transform.localScale = Vector3.Lerp(actual_stretch, actual_stretch, elasticity);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, trampoline_impulse);
        }
    }
    
    #region variables
    [Tooltip("The target stretch after someone lands on you")]
    public Vector3 stretch;
    
    [Tooltip("The target squash when someone lands on you")]
    public Vector3 squash;

    [Tooltip("Scale factor (how fast the size changes)")]
    [Range(0, 1)]
    public float elasticity = 0.2f;

    //Stores some info to correct measurements on awake
    Vector3 init_transform;
    Vector3 actual_stretch;
    Vector3 actual_squash;

    //Stores what this script is currently scaling to
    Vector3 target;

    //Sprite to flip when moving backwards
    SpriteRenderer sprite;
    #endregion

    private void Awake()
    {
        //Makes these values default to nothing if 
            //you forget to set them
        if (stretch == Vector3.zero)
        {
            stretch = Vector3.one;
        }
        if (squash == Vector3.zero)
        {
            squash = Vector3.one;
        }

        //Stores some info on awake
        init_transform = transform.localScale;
        sprite = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate()
    {
        //Scales the input values to match the actual size
        //this prevents errors when rescaling the body
        actual_stretch = Vector3.Scale(stretch, init_transform);
        actual_squash = Vector3.Scale(squash, init_transform);


        determineToStretch();

        //This code traces a curve towards the target which
        //  asymptotically approaches it (picture a frog jumping halfway 
        //  across a pond forever, it approached the other side quickly at
        //  first and slowly at the end)
        transform.localScale = Vector3.Lerp(transform.localScale, target, elasticity);
    }

    public void squash_function()
    {
        transform.localScale = Vector3.Lerp(actual_squash, actual_squash, elasticity);
        
    }
    
    // Decides whether to be going towards squash or stretch
    void determineToStretch()
    {

            //Otherwise, revert to normal
            target = init_transform;
        
    }

    
}
