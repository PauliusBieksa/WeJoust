using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    public UnityEvent OnOutOfBounds;
    public float HealthLossRate = 1.0f;
    
    [SerializeField]
    private float maxHealth = 1;
    [SerializeField]
    private Image icon;
    
    private float currentHealth = 1;
    
    void OnEnable()
    {
        OnOutOfBounds.AddListener(DepleteHealth);
        currentHealth = maxHealth;
    }

    void OnDisable()
    {
        OnOutOfBounds.RemoveListener(DepleteHealth);
    }

    void DepleteHealth()
    {
        currentHealth -= Time.deltaTime * HealthLossRate;
        icon.fillAmount = currentHealth / maxHealth;
    }
}
