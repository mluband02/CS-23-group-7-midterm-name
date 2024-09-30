using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Grid grid;
    // Start is called before the first frame update
    private void Start() {
        grid = new Grid(10, 5, 2f, new Vector3(-10, -5)); //change f for smaller grid 
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            //change value inside grid with click:
            grid.SetValue(GetMouseWorldPosition(), 56);
        }

        //reading values on right click
        if (Input.GetMouseButtonDown(1)) {
            //change value inside grid with click:
            Debug.Log(grid.GetValue(GetMouseWorldPosition()));
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
