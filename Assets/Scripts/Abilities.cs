using UnityEngine;

public class Abilities : MonoBehaviour
{
    Player playerScript;
    Rigidbody2D rb;

    [SerializeField]
    float fireExtinguisherThrust = 50f;
    [SerializeField]
    float fireExtinguisherDuration = 3f;
    [SerializeField]
    int stationaryCount = 5;
    [SerializeField]
    float stationaryVelocity = 30f;
    [SerializeField]
    float stationaryMass = 10f;
    [SerializeField]
    float stationaryCooldown = 0.8f;

    [SerializeField]
    GameObject highlighterPrefab;

    public float itemUsageRemaining = 0f;
    float cooldown = 0f;
    
    public MASKS currentMask = MASKS.NONE;

    public enum MASKS
    {
        FIRE_HELMET,
        STATIONARY,
        NONE
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMask != MASKS.NONE && itemUsageRemaining <= 0)
            currentMask = MASKS.NONE;
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    public void equipMask(MASKS m)
    {
        currentMask = m;
        switch (currentMask)
        {
            case MASKS.FIRE_HELMET:
                itemUsageRemaining = fireExtinguisherDuration;
                break;
            case MASKS.STATIONARY:
                itemUsageRemaining = stationaryCount - 0.000001f;
                break;
        }
    }

    public void UseItem()
    {
        switch (currentMask)
        {
            case MASKS.FIRE_HELMET:
                useFireExtinguisher();
                break;
            case MASKS.STATIONARY:
                useStationary();
                break;
        }
    }

    // Firefighter helmet
    private void useFireExtinguisher()
    {
        rb.AddForce(transform.rotation * playerScript.spritetBroomFacing * fireExtinguisherThrust);
        
        itemUsageRemaining -= Time.deltaTime;
        
    }

    private void useStationary()
    {
        if (cooldown <= 0)
        {
            GameObject hl = Instantiate(highlighterPrefab);
            hl.transform.position = transform.rotation * playerScript.spritetBroomFacing + transform.position;
            Rigidbody2D rb = hl.GetComponent<Rigidbody2D>();
            rb.mass = stationaryMass;
            rb.angularVelocity = Random.Range(0.2f, 5f);
            rb.linearVelocity = transform.rotation * playerScript.spritetBroomFacing * stationaryVelocity;

            itemUsageRemaining -= 1;
            cooldown += stationaryCooldown;
        }
    }
}
