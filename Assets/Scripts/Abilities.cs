using UnityEngine;
using UnityEngine.U2D.Animation;

public class Abilities : MonoBehaviour
{
    Player playerScript;
    Rigidbody2D rb;

    [SerializeField]
    private SpriteResolver maskSpriteResolver;

    [SerializeField]
    float fireExtinguisherThrust = 50f;
    [SerializeField]
    public float fireExtinguisherDuration = 3f;
    [SerializeField]
    int stationaryCount = 5;
    [SerializeField]
    float stationaryVelocity = 30f;
    [SerializeField]
    float stationaryMass = 10f;
    [SerializeField]
    float stationaryCooldown = 0.6f;
    [SerializeField]
    float lunchBoxCooldown = 0.8f;
    [SerializeField]
    int lunchBoxbananas = 4;
    public float bananaSpeedAdd = 10f;

    [SerializeField]
    GameObject highlighterPrefab;
    [SerializeField]
    GameObject bananaPrefab;

    public float itemUsageRemaining = 0f;
    float cooldown = 0f;
    
    public MASKS currentMask = MASKS.NONE;

    public enum MASKS
    {
        FIRE_HELMET,
        STATIONARY,
        LUNCHBOX,
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
        {
            currentMask = MASKS.NONE;
            maskSpriteResolver.SetCategoryAndLabel("Masks", "None");
        }
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
                maskSpriteResolver.SetCategoryAndLabel("Masks", "FireExtinguisher");
                break;
            case MASKS.STATIONARY:
                itemUsageRemaining = stationaryCount - 0.000001f;
                maskSpriteResolver.SetCategoryAndLabel("Masks", "Stationary");
                break;
            case MASKS.LUNCHBOX:
                itemUsageRemaining = lunchBoxbananas - 0.000001f;
                maskSpriteResolver.SetCategoryAndLabel("Masks", "LunchBox");
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
            case MASKS.LUNCHBOX:
                useLunchBox();
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

    private void useLunchBox()
    {
        if (cooldown <= 0)
        {
            GameObject banana = Instantiate(bananaPrefab);
            banana.transform.position = transform.position;
            Vector3 targetPos = (transform.rotation * new Vector3(-playerScript.spritetBroomFacing.x * 5f, -playerScript.spritetBroomFacing.y * 5f)) + transform.position;
            banana.GetComponent<Banana>().targetPos = targetPos;

            itemUsageRemaining -= 1;
            cooldown += lunchBoxCooldown;
        }
    }
}
