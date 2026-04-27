using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    [Header("Velocidad de movimiento")]
    public float moveSpeed = 8f;          // Velocidad normal
    public float fastMoveSpeed = 15f;     // Velocidad al mantener Shift

    [Header("Velocidad del mouse")]
    public float mouseSensitivity = 2f;   // Sensibilidad para mirar

    // Variables para guardar la rotación
    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        // Bloquea el cursor en el centro de la pantalla
        // para que el mouse controle la cámara cómodamente
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Guardamos la rotación inicial de la cámara
        Vector3 currentRotation = transform.eulerAngles;
        rotationX = currentRotation.y;
        rotationY = currentRotation.x;
    }

    void Update()
    {
        MoverCamara();
        RotarCamara();

        // Presiona Escape para liberar el mouse
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void MoverCamara()
    {
        // Elegimos velocidad normal o rápida
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? fastMoveSpeed : moveSpeed;

        // Movimiento horizontal y vertical
        float moveX = Input.GetAxis("Horizontal"); // A / D
        float moveZ = Input.GetAxis("Vertical");   // W / S

        // Movimiento arriba y abajo
        float moveY = 0f;

        if (Input.GetKey(KeyCode.E))
        {
            moveY = 1f; // Subir
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            moveY = -1f; // Bajar
        }

        // Dirección final en espacio local de la cámara
        Vector3 moveDirection = transform.right * moveX
                              + transform.forward * moveZ
                              + transform.up * moveY;

        // Aplicamos el movimiento
        transform.position += moveDirection * currentSpeed * Time.deltaTime;
    }

    void RotarCamara()
    {
        // Movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotación acumulada
        rotationX += mouseX;
        rotationY -= mouseY;

        // Limitamos la rotación vertical para no girar raro
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        // Aplicamos la rotación
        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);
    }
}