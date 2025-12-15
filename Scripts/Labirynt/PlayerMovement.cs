using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    public bool isRunning;
    public bool canJump = true;

    public Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    public GetKey getKey;

    private bool canMove = true;

    public GameController gameController;

    public float interactionRange = 3f;
    public LayerMask doorLayer;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Interakcje (E)
        if (getKey.haveKey1 && getKey.haveKey2)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit hit;
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionRange, doorLayer))
                {
                    Door door = hit.collider.GetComponent<Door>();
                    if (door == null)
                        door = hit.collider.GetComponentInParent<Door>();

                    if (door != null)
                    {
                        door.Activate();
                    }
                    else
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }

        if (gameController.isGameOver) return;

        // ---- OBRÓT KAMERY I GRACZA (PRZENIEŒ NA POCZ¥TEK) ----
        if (canMove)
        {
            // Yaw gracza (lewo/prawo) - NAJPIERW
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            transform.Rotate(Vector3.up * mouseX);

            // Pitch kamery (góra/dó³) - POTEM
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }

        // ---- RUCH ----
        isRunning = Input.GetKey(KeyCode.LeftShift) && canMove && !gameController.isGameOver;

        // Pobieramy input (RAW = bez wyg³adzania Unity)
        float inputX = canMove ? Input.GetAxisRaw("Horizontal") : 0f;
        float inputZ = canMove ? Input.GetAxisRaw("Vertical") : 0f;

        // Jeœli nie ma ¿adnego inputu, natychmiast zatrzymaj ruch poziomy
        if (inputX == 0f && inputZ == 0f)
        {
            moveDirection.x = 0f;
            moveDirection.z = 0f;
        }

        // Prêdkoœæ
        float speed = isRunning ? runSpeed : walkSpeed;

        // Tworzymy wektor ruchu - u¿ywamy TYLKO rotacji Y gracza (ignorujemy pitch kamery)
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Upewniamy siê, ¿e wektory s¹ na p³aszczyŸnie XZ (bez sk³adowej Y)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Obliczamy ruch poziomy
        Vector3 desiredMove = forward * inputZ + right * inputX;

        // Ograniczamy d³ugoœæ wektora do 1 (zapobiega szybszemu ruchowi na ukos)
        if (desiredMove.magnitude > 1f)
        {
            desiredMove.Normalize();
        }

        Vector3 horizontalMove = desiredMove * speed;

        // BEZPOŒREDNIE przypisanie zamiast zachowywania starej wartoœci
        moveDirection.x = horizontalMove.x;
        moveDirection.z = horizontalMove.z;

        // Grawitacja i skok (PRZED sprawdzaniem skoku)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else if (!Input.GetButton("Jump"))
        {
            // Resetuj Y gdy jesteœmy na ziemi i nie skaczemy
            moveDirection.y = -0.5f; // Ma³a wartoœæ ujemna trzyma nas na ziemi
        }

        // Skok
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && canJump)
        {
            moveDirection.y = jumpPower;
        }

        // ---- Kucanie ----
        if (Input.GetKey(KeyCode.LeftControl) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        // Wykonaj ruch
        characterController.Move(moveDirection * Time.deltaTime);
    }
}