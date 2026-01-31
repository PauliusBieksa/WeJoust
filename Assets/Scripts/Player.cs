using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int PlayerID {get; private set;}
    public float moveSpeed = 1f;
    public float rotattionSpeed = 1f;
    public Vector2 spritetBroomFacing = new Vector2(-1, 0);
    public float extraRotationDamping = 10f;
    
    InputAction moveAction;
    InputAction lookAction;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector2 lookValue = lookAction.ReadValue<Vector2>();

        rb.AddForce(moveValue * moveSpeed);

        if (lookValue.sqrMagnitude > 0)
        {
            Vector2 currentFacing = transform.rotation * spritetBroomFacing;
            float torqueVal = rotattionSpeed;
            bool leftOfFacing = Vector3.Cross(new Vector3(currentFacing.x, currentFacing.y, 0f), new Vector3(lookValue.x, lookValue.y, 0f)).z > 0;
            float facingDotProd = Vector2.Dot(currentFacing, lookValue);
            if (leftOfFacing)
            {
                //
            }
            else
            {
                torqueVal = -torqueVal;
            }
            rb.angularDamping = extraRotationDamping * (1f + facingDotProd);
            rb.AddTorque(torqueVal);
        }
    }
}
