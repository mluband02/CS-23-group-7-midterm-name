using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cursor : MonoBehaviour
{
   public bool canMove = true;
   public Color safe = Color.green;
   public Color bad = Color.red;
   public bool canPlace = true;
   public GameObject Ground;

   // UI text component to display amount of stickers left
   public TextMeshProUGUI countText;
   // variable keeping track of sticker count
   private int count;

    // Start is called before the first frame update
    void Start()
    {
      safe.a = 0.5f;
      bad.a = 0.5f;
      gameObject.GetComponent<Renderer>().material.color = safe;

      // initialize count
      count = 7;
      // update count display
      SetCountText();
    }

    // Update is called once per frame
    void Update()
    {

      if (canPlace)
      {
         // when spaced is pressed and count is larger than 0
         if (Input.GetKeyDown(KeyCode.Space) && count > 0) 
         {
            print(transform.position);
            Instantiate(Ground, transform.position, Quaternion.identity);

            // update count 
            count--;
            SetCountText();
         } else if(Input.GetKeyDown(KeyCode.Space) && count <= 0){
            Debug.Log("Oof you ran out of blocks to place :(");
         }
      }
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
}