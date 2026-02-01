using System;
using System.Collections.Generic;
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
    public static Action<int, PowerupItem> OnGetPickup;
    
    /// <summary>
    /// Event for Showing Pickup
    /// </summary>
    public static Action<int> OnUsePickup;
    
    
    
    public float HealthLossRate = 1.0f;
    public Sprite defaultItemSprite;
    public Sprite deadItemSprite;
    public Image borderImage;
    public Image itemIcon;
    public List<Sprite> iconSprites;

    
    [SerializeField]
    private float maxHealth = 1;
    [SerializeField]
    private Image playerIcon;
    
    private float currentHealth = 1;
    
    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    void OnDisable()
    {
    }

    public void ShowHealth(float healthPercent)
    {
        playerIcon.fillAmount = healthPercent;
    }

    public void HandlePickup(PowerupItem item)
    {
        itemIcon.sprite = item.Icon.sprite;
    }
    
    public void RemovePickup()
    {
        itemIcon.sprite = defaultItemSprite;
    }

    public void ShowDead()
    {
        itemIcon.sprite = deadItemSprite;
    }
}
