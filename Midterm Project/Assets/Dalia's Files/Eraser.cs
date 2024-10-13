using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isDragging = false;

    // Static reference to the first instance of Ground
    private static GameObject firstGroundInstance;

    void Start()
    {
        // Store the original position of the eraser
        originalPosition = transform.position;
    }

    void Update()
    {
        // Handle dragging of the eraser only
        if (Input.GetMouseButtonDown(0)) // When the mouse button is clicked
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; // Set Z position to 0 so it stays in the 2D plane
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            // Ensure only the eraser is draggable, not other objects like the ground
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
            // Check if this is the first instance of the ground
            if (firstGroundInstance == null)
            {
                firstGroundInstance = other.gameObject; // Set the first instance
                return; // Skip destruction for the first instance
            }

            // If not the first instance, destroy the ground object
            if (other.gameObject != firstGroundInstance)
            {
                Destroy(other.gameObject);

                // Find the Cursor script and increase the sticker count
                Cursor cursor = FindObjectOfType<Cursor>();
                cursor.IncreaseStickerCount(); 
            }
        }
    }
}










