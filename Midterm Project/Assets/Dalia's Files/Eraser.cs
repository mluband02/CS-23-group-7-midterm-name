using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isDragging = false;

    void Start()
    {
        // Store the original position of the eraser
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; 
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Start dragging the eraser
                isDragging = true;
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; 
            transform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Stop dragging and return to the original position
            isDragging = false;
            transform.position = originalPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the eraser collided with the Ground object
        if (other.gameObject.CompareTag("Ground"))
        {
            // Use the custom Ground script to check if it's the original ground
            Ground groundComponent = other.gameObject.GetComponent<Ground>();

            if (groundComponent != null && !groundComponent.isOriginal)
            {
                // If it's not the original, destroy it
                Destroy(other.gameObject);

                // Find the Cursor script and increase the sticker count
                Cursor cursor = FindObjectOfType<Cursor>();
                cursor.IncreaseStickerCount(); 
            }
        }

        // if (other.gameObject.CompareTag("Trampoline")) {
        //     Debug.Log("TRAMPOLINE!");
        //     Destroy(other.gameObject);
        //     Cursor cursor = FindObjectOfType<Cursor>();
        //     cursor.IncreaseStickerCount(); 
        // }
    }
}











