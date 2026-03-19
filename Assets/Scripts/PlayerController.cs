using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset inputActions;

    private InputAction moveAction;
    private InputAction fireAction;

    public Vector2 moveVector;

    private Animator animator;
    //private Rigidbody2D rigidbody2D;

    [Header("Movement")]
    public float moveSpeed = 3f;
    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Attack");

        animator =  GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        Vector3 move = moveVector * moveSpeed * Time.deltaTime;

        transform.position += move;
    }

    private void Attack()
    {
        if (fireAction.WasPressedThisFrame())
        {
            
            animator.SetTrigger("isSlashing");
        }
    }
}
