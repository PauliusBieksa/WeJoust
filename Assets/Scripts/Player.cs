using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int playerID;
    public PlayerCard playerCard;
    public SpriteRenderer chair;
    
    public SpriteRenderer extinguisher;
    public ParticleSystem foam;
    
    public float moveSpeed = 1f;
    public float rotattionSpeed = 1f;
    public Vector2 spritetBroomFacing = new Vector2(-1, 0);
    public float extraRotationDamping = 10f; 
    public float HealthLossRate = 1.0f;
    
    InputAction moveAction;
    InputAction lookAction;
    InputAction useAction;
    Rigidbody2D rb;
    
    private bool losingHealth = false;
    [SerializeField]
    private float maxHealth = 1;
    private float currentHealth = 1;

    private Vector2 moveValue;
    private Vector2 lookValue;

    private Abilities abilitiesScript;
    private GameManager gmScript;

    bool dead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerLobbyManager.RegisterPlayerEvent(this);
        
        moveAction = GetComponent<PlayerInput>().actions.FindAction("Move");
        lookAction = GetComponent<PlayerInput>().actions.FindAction("Move");
        //lookAction = GetComponent<PlayerInput>().actions.FindAction("Look");
        useAction = GetComponent<PlayerInput>().actions.FindAction("Jump");
        rb = GetComponent<Rigidbody2D>();

        abilitiesScript = GetComponent<Abilities>();
        gmScript = GameObject.Find("Game Manager").GetComponent<GameManager>();

        transform.position = gmScript.GetSpawnPosition();
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
        else if (other.gameObject.tag == "Item")
        {
            if (abilitiesScript.currentMask == Abilities.MASKS.NONE)
            {
                abilitiesScript.equipMask(other.GetComponent<PowerupItem>().Mask);
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.tag == "Banana")
        {
            rb.linearVelocity = rb.linearVelocity + rb.linearVelocity.normalized * abilitiesScript.bananaSpeedAdd;
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
            return;

        moveValue = moveAction.ReadValue<Vector2>();
        lookValue = lookAction.ReadValue<Vector2>();
        
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

        ControlItemVisibility();


        if (useAction.IsPressed())
        {
            abilitiesScript.UseItem();
            if (abilitiesScript.itemUsageRemaining > 0 &&
                abilitiesScript.currentMask == Abilities.MASKS.FIRE_HELMET)
            {
                foam.Play();
            }else
            {
                foam.Stop();
            }
            
        }
        else
        {
            foam.Stop();
        }
        
        if(losingHealth)
        {
            EvaluateHealth();
        }
    }

    private void ControlItemVisibility()
    {
        
        
        playerCard.itemIcon.sprite = playerCard.iconSprites[(int)abilitiesScript.currentMask];
        
        if (Abilities.MASKS.NONE == abilitiesScript.currentMask)
        {
            extinguisher.enabled = false;
        }

        if (Abilities.MASKS.FIRE_HELMET == abilitiesScript.currentMask)
        {
            extinguisher.enabled = true;
            playerCard.itemIcon.fillAmount = abilitiesScript.itemUsageRemaining/abilitiesScript.fireExtinguisherDuration;
            //print($"fill {playerCard.itemIcon.gameObject.name} {abilitiesScript.itemUsageRemaining/abilitiesScript.fireExtinguisherDuration}");
        }
        else
        {
            playerCard.itemIcon.fillAmount = 1;
        }
    }

    private void EvaluateHealth()
    {
        currentHealth -= Time.deltaTime * HealthLossRate;
        float healthPercent = currentHealth / maxHealth;
        playerCard.ShowHealth(healthPercent);
        
        if(currentHealth <= 0)
        { 
            // /PlayerLobbyManager.UnregisterPlayerEvent(this);
            playerCard.ShowDead();
            dead = true;
            Destroy(this.gameObject);
        }
    }
}
