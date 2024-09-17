using UnityEngine;

public class CameraBehavior: MonoBehaviour
{
    public Transform player; 
    public Vector3 offset;   
    public float mouseSensitivity = 100.0f;  
    public float distanceFromPlayer = 5f;   

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (offset == Vector3.zero)
        {
            offset = new Vector3(0, 2, -distanceFromPlayer);  
        }
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX; 
        xRotation -= mouseY; 
        xRotation = Mathf.Clamp(xRotation, -30f, 60f); 

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        transform.position = player.position + rotation * offset;

        transform.LookAt(player);
    }
}
