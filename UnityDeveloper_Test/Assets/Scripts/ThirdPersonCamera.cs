using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float rotationSpeed = 200f;

    public Transform orientation; // To rotate movement reference

    float mouseX, mouseY;
    public float minY = -60f, maxY = 60f;
    float xRotation, yRotation;

    void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, minY, maxY);

        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        transform.position = target.position + rotation * offset;
        transform.LookAt(target.position);

        orientation.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
