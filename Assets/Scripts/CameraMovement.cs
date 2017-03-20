using UnityEngine;
using System.Collections;

public class Boundary
{
    public float minX, manX, minZ, manZ;
}


public class CameraMovement : MonoBehaviour
{

    public  float speed = 2.0f;
    public  float zoomSpeed = 2.0f;

    public float minX = -360.0f;
    public float maxX = 360.0f;

    public float minY = -45.0f;
    public float maxY = 45.0f;

    public float sensX = 100.0f;
    public float sensY = 100.0f;

    private Boundary bound;

    void Update()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(0, -scroll * zoomSpeed, scroll * zoomSpeed, Space.World);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        moveHorizontal *= Time.deltaTime * speed;

        moveVertical *= Time.deltaTime * speed;

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        transform.position += movement;

        

        //暂时停止旋转
        //if (Input.GetMouseButton (0)) {
        //	rotationX += Input.GetAxis ("Mouse X") * sensX * Time.deltaTime;
        //	rotationY += Input.GetAxis ("Mouse Y") * sensY * Time.deltaTime;
        //	rotationY = Mathf.Clamp (rotationY, minY, maxY);
        //	transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
        //}
    }
}
