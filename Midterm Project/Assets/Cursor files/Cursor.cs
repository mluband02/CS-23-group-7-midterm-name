using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Cursor : MonoBehaviour
{
   public bool canMove = true;
   public Color safe = Color.green;
   public Color bad = Color.red;
   public bool canPlace = true;
   public GameObject Ground;
   public GameObject Trampoline;
   // int = 0 is ground
   // int = 1 is trampoline
   public int blockType;

   // UI text component to display amount of stickers left
   public TextMeshProUGUI countText;
   // variable keeping track of sticker count
   private int count;

    // Start is called before the first frame update
    void Start()
    {
      safe.a = 0.25f;
      bad.a = 0.25f;
      gameObject.GetComponent<Renderer>().material.color = safe;
      blockType = 0;
      canMove = true;

    //   // initialize count
    //   count = 7;
    //   // update count display
    //   SetCountText();


      // Set initial sticker count based on the level
      SetInitialStickerCount();
    }

    // Update is called once per frame
    void Update()
    {

      if (canPlace)
      {
         // when spaced is pressed and count is larger than 0
         if (Input.GetKeyDown(KeyCode.Space) && count > 0) 
         {
            if (blockType == 0)
            {
               GameObject newGround = Instantiate(Ground, transform.position, Quaternion.identity);
               // Set 'isOriginal' to false for the newly instantiated ground
               Ground groundComponent = newGround.GetComponent<Ground>();
               if (groundComponent != null)
               {
                   groundComponent.isOriginal = false;
               }
            }
            if (blockType == 1)
            {
              // Vector3 adjust = transform.position;
              // adjust.y-=0.5f;
               Instantiate(Trampoline, transform.position, Quaternion.identity);
            }

            // update count 
            count--;
            SetCountText();

         } else if(Input.GetKeyDown(KeyCode.Space) && count <= 0){
            Debug.Log("Oof you ran out of blocks to place :(");
         }
      }

      // changes block type
      if (Input.GetKeyDown("z"))
      {
         if (blockType == 0)
         {
            blockType = 1;
            gameObject.transform.localScale = new Vector3 (1.9f, 0.9f, 1f);

         }
         else
         {
            blockType = 0;
            gameObject.transform.localScale = new Vector3 (2.9f, 0.9f, 1f);
         }
      }

      //movement code
      if (canMove)
      {
        if (Input.GetKey("up") && transform.position.y != 4.5)
        {
           transform.position += Vector3.up * 1f;
           StopCoroutine(MoveDelay());
           StartCoroutine(MoveDelay());
        }
        if (Input.GetKey("down") && transform.position.y != -4.5)
        {
           transform.position += Vector3.down * 1f;
           StopCoroutine(MoveDelay());
           StartCoroutine(MoveDelay());
        }
        if (Input.GetKey("left") && transform.position.x != -7.5)
        {
           transform.position += Vector3.left * 1f;
           StopCoroutine(MoveDelay());
           StartCoroutine(MoveDelay());
        }
        if (Input.GetKey("right") && transform.position.x != 7.5)
        {
           transform.position += Vector3.right * 1f;
           StopCoroutine(MoveDelay());
           StartCoroutine(MoveDelay());
        }
      }
    }

   IEnumerator MoveDelay()
   {
      canMove = false;
      yield return new WaitForSeconds(0.15f);
      canMove = true;
   }

   //stops block placement when over other object
   void OnTriggerEnter2D(Collider2D other)
   {
      gameObject.GetComponent<Renderer>().material.color = bad;
      canPlace = false;
   }

   void OnTriggerExit2D(Collider2D other)
   {
      gameObject.GetComponent<Renderer>().material.color = safe;
      canPlace = true;
   }

   void SetCountText(){
      countText.text = "Available stickers: " + count.ToString();
   }


    // Add more stickers for different levels 
   public void SetInitialStickerCount()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Main Scene") 
        {
            count = 1; // Only allow 1 sticker in the Main Scene
        }
        else if (currentScene == "Level 3") 
        {
            count = 1; // Allow 2 stickers in Level 3
        }
        else if (currentScene == "Level 4")
        {
            count = 1;
        }
        else 
        {
            count = 7; // Default value or another level-specific value
        }
        
        SetCountText();
    }

   public void IncreaseStickerCount() {

            count++;
            SetCountText();
    }


}