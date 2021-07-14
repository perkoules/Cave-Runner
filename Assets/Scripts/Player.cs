using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 5;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cam;

    private Vector3 rawInputMovement;

    private Animator anim;
    public float smoothTurnTime =0.1f;
    private float smoothTurnVelocity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }
    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Debug.Log("Attack");
        }
    }
    private void Update()
    {
        bool isWalking = rawInputMovement.x > 0.1f || rawInputMovement.x < - 0.1f || (rawInputMovement.z > 0.1f || rawInputMovement.z < -0.1f);
        Vector3 direction = rawInputMovement.normalized;
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


            characterController.SimpleMove(speed * Time.deltaTime * moveDir.normalized); 
        }
        anim.SetBool("isWalking", isWalking);
    }
}