using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    //When code is in a region, you can minimize it (there should be a 
        //little minus sign on the left you can click)
    #region variables
    [Tooltip("Dust effect, you probably want this to be a particle system")]
    public GameObject dust;
    [Tooltip("Minimum collision velocity to cause dust")]
    public float dust_threshold;

    [Tooltip("Speed the player travels upwards while jumping")]
    public float jump_impulse;
    [Tooltip("Horizontal Speed")]
    public float speed;
    
    [Tooltip("Horizontal Air Accel")]
    public float air_accel;
    
    [Tooltip("Horizontal Air Friction")]
    public float air_fric;
    
    [Tooltip("Max Air Speed")]
    public float air_speed;
    
    [Tooltip("Gravity")]
    public float gravity;
    
    [Tooltip("Rigidbody that controls this object (serialized to save a call to GameObject.Find())")]
    public Rigidbody2D body;

    public GameObject winText;
    public GameObject loseText;
    public GameObject resetButton;

    bool jump_is_buffered;

    float jump_expiration;
    [Tooltip("Layer from which the character can jump from")]
    public LayerMask jumpable;

    float coyote_end;
    [Tooltip("Time during which the player can still jump after leaving the ground (named after Wil E Coyote)")]
    public float coyote_time = 0.1f;


    [Tooltip("An audiosource with the clip for jumping loaded")]
    public AudioSource jump_sound;

    [Tooltip("Collider on this player that will interact with the ground")]
    public CircleCollider2D body_collider;

    //Variables which track the state of the player (with regard to jumping)
    string state = "jumping";
    float jump_begin = 0;

    //Variables which store the positions from which to detect the ground
    Vector2 left_foot;
    Vector2 right_foot;
    #endregion

    private Animator anim;

    private void Awake()
    {
        //Gets the circle's radius, scaled to accurately represent the distance in world space
        float radius = body_collider.radius * Mathf.Min(transform.localScale.x, transform.localScale.y);

        //Subtracts a little off the edge so the "feet" don't detect walls as the ground
        float margin = 0.05f;
        float xDisplacement = radius - margin;

        //Initializes the vectors
        left_foot = new Vector2(-xDisplacement, 0);
        right_foot = new Vector2(xDisplacement, 0);

        anim = GetComponentInChildren<Animator>();
    }

    // In update we put checks which we want to run as often as possible, like
    // checking if we're on the ground and if we've input a jump
    void Update()
    {
        //translates positions to be in world space (if you didn't do this they'd be
            //in relation to the center of the player  
        Vector2 left_foot_worldspace = left_foot + (Vector2)transform.position;
        Vector2 right_foot_worldspace = right_foot + (Vector2)transform.position;

        //Checks if player is touching ground by approximating a box underneath the player
        if (Physics2D.OverlapArea(left_foot_worldspace, right_foot_worldspace, jumpable) != null)
        {
            state = "grounded";
            coyote_end = coyote_time + Time.time;
        }
        //Registers a jump input if either the up arrow or w key is pressed
        if (jumpPress() && inputState())
        {
            jump_is_buffered = true;
        }

        
    }


    //Input a jump, doesn't currently work with controller input
    bool jumpPress()
    {
        return Input.GetKeyDown(KeyCode.W);
    }

    //Is in a state where we want to buffer jump commands
    bool inputState()
    {
        return state == "grounded" ;
    }

    //In fixed update we put physics calculations which we want to be frame independent
    private void FixedUpdate()
    {   
        // float to keep track of horizontal movements
        float horizontal = 0;
        if (Input.GetKey(KeyCode.A)){
            // move left
            horizontal = -1;
            anim.SetBool("walk", true);
        } 
        else if (Input.GetKey(KeyCode.D)){
            // move right
            horizontal = 1;
            anim.SetBool("walk", true);
        } 

        else
        {
            anim.SetBool("walk", false);
        }

        //Sets the horizontal velocity to the input times the speed
        

        //Runs the state machine which determines where in the jump you are
        switch (state)
        {
            case "grounded": // in coyote time
            
                body.velocity = new Vector2(horizontal * speed, body.velocity.y);
                
                if (jump_is_buffered)
                    jumpstart();
                    

                break;

            case "jumpstart":  // going upward
            
                body.velocity = new Vector2(body.velocity.x, jump_impulse);
                jump_is_buffered = false;
                
                if (horizontal != 0) {
                    body.velocity = new Vector2(Mathf.Clamp(body.velocity.x + horizontal * air_accel, -air_speed, air_speed), body.velocity.y);
                }
                else if (horizontal == 0 && body.velocity.x > 0) {
                    body.velocity = new Vector2(Mathf.Clamp(body.velocity.x - air_fric, 0, body.velocity.x), body.velocity.y);
                }
                else if (horizontal == 0 && body.velocity.x < 0) {
                    body.velocity = new Vector2(Mathf.Clamp(body.velocity.x + air_fric, body.velocity.x, 0), body.velocity.y);
                }

                state = "jumping";
        
                break;

            case "jumping":  // set in freefall
            
                if (horizontal != 0) {
                    body.velocity = new Vector2(Mathf.Clamp(body.velocity.x + horizontal * air_accel, -air_speed, air_speed), body.velocity.y);
                }
                else if (horizontal == 0 && body.velocity.x > 0) {
                    body.velocity = new Vector2(Mathf.Clamp(body.velocity.x - air_fric, 0, body.velocity.x), body.velocity.y);
                }
                else if (horizontal == 0 && body.velocity.x < 0) {
                    body.velocity = new Vector2(Mathf.Clamp(body.velocity.x + air_fric, body.velocity.x, 0), body.velocity.y);
                }

                
                
                break;
        }
    }

    //Begins a jump
    void jumpstart()
    {
        //Audio and visuals
        jump_sound.Play();
        Instantiate(dust, transform);

        //Set the player moving upwards
        body.velocity = new Vector2(body.velocity.x, jump_impulse);

        //Handle state
        coyote_end = 0;
        jump_begin = Time.time;
        jump_is_buffered = false;
        state = "jumping";
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is the Sun
        if (other.gameObject.tag == "Sun")
        {
            // Debug.Log("TOUCHED THE SUN!");  // Debug log for collision
            other.gameObject.SetActive(false);  // Deactivate the sun
            this.gameObject.SetActive(false);
            winText.SetActive(true);            // Show win text
        }

        if (other.gameObject.tag == "Spikes")
        {
            // Debug.Log("TOUCHED THE SUN!");  // Debug log for collision
            this.gameObject.SetActive(false); 
            loseText.SetActive(true);
            resetButton.SetActive(true);
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Note to the reader - this is implemented with sqrMagnitude and dust_threshold squared
        //because square root is (used in regular magnitude) is an expensive operation
        
        
        
        if(collision.relativeVelocity.sqrMagnitude > dust_threshold * dust_threshold)
        {
            Instantiate(dust, transform);
        }

        // jump animator
        if (collision.gameObject.tag == "Ground")
        {

            anim.SetBool("jump", false);
        }
        else
        {
            anim.SetBool("jump", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

            anim.SetBool("jump", true);
        }
    }
}
