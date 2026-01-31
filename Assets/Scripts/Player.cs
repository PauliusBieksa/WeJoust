using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int PlayerID {get; private set;}
    public PlayerCard playerCard;
    
    public float moveSpeed = 1f;
    public float rotattionSpeed = 1f;
    public Vector2 spritetBroomFacing = new Vector2(-1, 0);
    public float extraRotationDamping = 10f; 
    public float HealthLossRate = 1.0f;
    
    InputAction moveAction;
    InputAction lookAction;
    Rigidbody2D rb;
    
    private bool losingHealth = false;
    [SerializeField]
    private float maxHealth = 1;
    private float currentHealth = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerLobbyManager.RegisterPlayerEvent(this);
        
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        rb = GetComponent<Rigidbody2D>();
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "arena")
        {
            losingHealth = true;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "arena")
        {
            losingHealth = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(losingHealth)
        {
            EvaluateHealth();
        }
        
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

    private void EvaluateHealth()
    {
        currentHealth -= Time.deltaTime * HealthLossRate;
        float healthPercent = currentHealth / maxHealth;
        playerCard.ShowHealth(healthPercent);
        
        if(currentHealth <= 0)
        { 
            PlayerLobbyManager.UnregisterPlayerEvent(this);
            playerCard.ShowDead();
        }
    }
}
