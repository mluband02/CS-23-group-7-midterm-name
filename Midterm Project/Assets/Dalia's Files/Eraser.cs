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
        // Handle dragging
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Start dragging if the eraser is clicked
                isDragging = true;
            }
        }

        if (Input.GetMouseButton(0) && isDragging) // While holding the mouse button down
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; // Set Z position to 0 so it stays in the 2D plane
            transform.position = mousePos; // Move eraser with the mouse
        }

        if (Input.GetMouseButtonUp(0)) // When the mouse button is released
        {
            // Stop dragging and return to original position
            isDragging = false;
            transform.position = originalPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the eraser collided with the Ground object
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(other.gameObject); // Erase the ground object
            Cursor cursor = FindObjectOfType<Cursor>(); // Find the Cursor script
            cursor.IncreaseStickerCount(); // Increase the sticker count when ground is erased
        }
    }
}



