using UnityEngine;

public class TPSCameraLook : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    public float sensitivity = 3f;
    public float minPitch = -40f;
    public float maxPitch = 70f;

    float yaw;
    float pitch;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * sensitivity;
        pitch -= mouseY * sensitivity;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        playerController.Rotate(yaw);
    }
}
