using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform player;      
    public Vector3 offset;         
    public float mouseSensitivity = 100.0f; 
    public float distanceFromPlayer = 5f;    
    public float moveSpeed = 5f;   

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

        HandlePlayerMovement(rotation);
    } 

    void HandlePlayerMovement(Quaternion cameraRotation)
    {
     
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            Vector3 forward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized; 
            Vector3 right = new Vector3(transform.right.x, 0, transform.right.z).normalized;      

            Vector3 moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

            player.position += moveDirection * moveSpeed * Time.deltaTime;

            player.rotation = Quaternion.LookRotation(moveDirection);
        }
    }
}
