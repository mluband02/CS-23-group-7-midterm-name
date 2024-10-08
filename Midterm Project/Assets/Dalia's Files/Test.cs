using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    private Grid grid;
    public GameObject Cursor;
    public GameObject Ground;
    // public GameObject Sun;
    public GameObject winText;
    public GameObject loseText;
    // Start is called before the first frame update
    private void Start() {
        winText.SetActive(false);
        loseText.SetActive(false);
        grid = new Grid(16, 10, 1f, new Vector3(-8f, -5f)); //change f for smaller grid 
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            // Instantiate(Ground, GameObject.Find("Cursor").transform.position, Quaternion.identity);
            //change value inside grid with click:
            grid.SetValue(GameObject.Find("Cursor").transform.position, Instantiate(Ground, GameObject.Find("Cursor").transform.position, Quaternion.identity));
        }

        
    }


    public static Vector3 GetMouseWorldPosition() {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ() {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    

}
