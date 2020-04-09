using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    public float speed;

    public void Move(Vector3 direction)
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + direction.normalized * Time.deltaTime * speed);
    }

    public void Move(Transform target, float magnitudeBeforeStop)
    {
        var direction = (target.transform.position - transform.position);
        if(direction.magnitude > magnitudeBeforeStop)
            GetComponent<Rigidbody>().MovePosition(transform.position + direction.normalized * Time.deltaTime * speed);
    }

    public void Look(Vector3 pointToLook)
    {
        pointToLook.y = transform.position.y;
        transform.LookAt(pointToLook);
    }

    public void LookSmooth(Transform target, float rotationSpeed)
    {
        var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void LookSmooth(Vector3 direction, float rotationSpeed)
    {
        var targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
