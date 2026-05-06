using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    #region Declarations

    

    public Vector2 moveVector;

    private Animator animator;
    private AnimatorStateInfo animatorStateInfo;
    //private Rigidbody2D rigidbody2D;

    //private bool isMoving;
    private bool isAttacking;
    private float attackHoldTime;
    private bool attackTriggered;

    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private GameObject SwordHitboxMiddle;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference attackAction;

    [Header("Movement")]
    public float moveSpeed = 3f;

    #endregion

    private void OnEnable()
    {
        //inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        //inputActions.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        moveAction.action.performed += OnMovePerformed;
        moveAction.action.canceled += OnMoveCanceled;

        attackAction.action.performed += OnAttackPerformed;
        attackAction.action.canceled += OnAttackCanceled;

        animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        moveAction.action.performed -= OnMovePerformed;
        moveAction.action.canceled -= OnMoveCanceled;

        attackAction.action.performed -= OnAttackPerformed;
        attackAction.action.canceled -= OnAttackCanceled;
    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {

    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {

    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        isAttacking = true;
        attackHoldTime = 0f;
        attackTriggered = false;
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        isAttacking = false;
        attackHoldTime = 0f;
    }


    private void Update()
    {
        MovementHandeler();
        Attack();
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

    private void MovementHandeler()
    {
            moveVector = moveAction.action.ReadValue<Vector2>();
            Vector3 move = moveVector * moveSpeed * Time.deltaTime;

            transform.position += move;
    }

    private void Attack()
    {

        if (isAttacking)
        {
            attackHoldTime += Time.deltaTime;

            if (!attackTriggered && attackHoldTime >= 0.3f)
            {
                attackTriggered = true;
                //OnAttackHeld();
            }
        }

        if (attackAction.action.WasPressedThisFrame())
        {
            if (moveVector.y > 0)
            {
                animator.Play("Slash3");
            }
            if (moveVector.y < 0)
            {
                animator.Play("Slash2");
            }
            if (moveVector.y == 0)
            {
                animator.Play("Slash1");
            }
        }
        AttackSlash1Animations();
    }
    private void AttackSlash1Animations()
    {
        if (animatorStateInfo.IsName("Slash1") && animatorStateInfo.normalizedTime >= 0.1f && animatorStateInfo.normalizedTime <= 0.9f)
        {
            SwordHitboxMiddle.SetActive(true);
            //return;
        }
        if (animatorStateInfo.IsName("Slash1") && animatorStateInfo.normalizedTime >= 0.9f)
        {
            SwordHitboxMiddle.SetActive(false);
        }
        if ((animatorStateInfo.IsName("Slash1") || animatorStateInfo.IsName("Slash2") || animatorStateInfo.IsName("Slash3")) && animatorStateInfo.normalizedTime >= 1f)
        {
            animator.Play("Idle");
        }
    }
    IEnumerator WaitFunction(float waitTime)
    {
        //Debug.Log("Before wait");

        yield return new WaitForSeconds(waitTime);

        //Debug.Log("After " + waitTime + " seconds");
    }
}
