using UnityEngine;

public class GravityManipulator : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public Transform orientation;

    private Vector3 targetGravity = Physics.gravity;

    void Update()
    {
        Vector3 newGravityDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            newGravityDirection = Vector3.forward;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            newGravityDirection = Vector3.back;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            newGravityDirection = Vector3.left;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            newGravityDirection = Vector3.right;

        if (newGravityDirection != Vector3.zero)
        {
            targetGravity = newGravityDirection * 9.81f;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Physics.gravity = targetGravity;

            // Rotate player upright based on new gravity
            Vector3 upDirection = -targetGravity.normalized;
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, upDirection) * transform.rotation;
            StartCoroutine(RotatePlayer(targetRotation));
        }
    }

    System.Collections.IEnumerator RotatePlayer(Quaternion targetRot)
    {
        float elapsed = 0f;
        Quaternion startRot = transform.rotation;

        while (elapsed < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsed);
            orientation.rotation = transform.rotation;
            elapsed += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        transform.rotation = targetRot;
        orientation.rotation = targetRot;
    }
}
