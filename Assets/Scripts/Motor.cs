using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    public float speed;

    public void Move(Vector3 direction)
    {
        transform.Translate(direction.normalized * Time.deltaTime * speed, Space.World);
    }

    public void Look(Vector3 pointToLook)
    {
        pointToLook.y = transform.position.y;
        transform.LookAt(pointToLook);
    }

    public void LookAtMouse()
    {
        Camera camera = Camera.main;
        Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        Vector3 pointToLook = Vector3.zero;
        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            pointToLook = cameraRay.GetPoint(rayLength);
        }
        Look(pointToLook);
    }

}
