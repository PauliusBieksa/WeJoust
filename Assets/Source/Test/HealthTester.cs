using UnityEngine;
using UnityEngine.UI;

public class HealthTester : MonoBehaviour
{
    public PlayerCard pc;
    private bool isDamaging = false;

    public PowerupItem powerUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ToggleDamage()
    {
        isDamaging = !isDamaging;
    }
    
    public void PickupItem()
    {
        PlayerCard.OnActivatePickup(1, powerUp);
        Debug.Log("Picked up");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamaging)
        {
            PlayerCard.OnOutOfBounds(1);
            //Debug.Log("Damaging");
        }
        
    }
}
