using UnityEngine;
using UnityEngine.UI;

public class HealthTester : MonoBehaviour
{
    public PlayerCard pc;
    private bool isDamaging = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ToggleDamage()
    {
        isDamaging = !isDamaging;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamaging)
        {
            pc.OnOutOfBounds.Invoke();
            Debug.Log("Damaging");
        }
        
    }
}
