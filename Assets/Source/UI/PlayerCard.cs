using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    /// <summary>
    /// Event for trigering card behaviour when player is out of bounds
    /// </summary>
    public static Action<int> OnOutOfBounds;

    /// <summary>
    /// Event for Showing Pickup
    /// </summary>
    public static Action<int, PowerupItem> OnActivatePickup;

    public int PlayerId;
    
    
    public float HealthLossRate = 1.0f;
    
    [SerializeField]
    private float maxHealth = 1;
    [SerializeField]
    private Image playerIcon;
    [SerializeField]
    private Image itemIcon;
    
    private float currentHealth = 1;
    
    void OnEnable()
    {
        OnOutOfBounds+=DepleteHealth;
        OnActivatePickup+=HandlePickup;
        
        currentHealth = maxHealth;
    }

    void OnDisable()
    {
        OnOutOfBounds+=DepleteHealth;
        OnActivatePickup+=HandlePickup;
    }

    void DepleteHealth(int outOfBoundsPlayerId)
    {
        if (outOfBoundsPlayerId != PlayerId)
        {
            return;
        }
        
        currentHealth -= Time.deltaTime * HealthLossRate;
        playerIcon.fillAmount = currentHealth / maxHealth;
    }

    void HandlePickup(int pickupPlayerId, PowerupItem item)
    {
        if (pickupPlayerId != PlayerId)
        {
            return;
        }

        itemIcon.sprite = item.Icon.sprite;

    }
}
