using UnityEngine;
using UnityEngine.U2D.Animation;

public class PowerupItem : MonoBehaviour
{
    public SpriteRenderer Icon;
    public Abilities.MASKS Mask;
    
    void Start()
    {
        int choice = Random.Range(0, 2);
        SpriteResolver sprite_resolver = GetComponentInChildren<SpriteResolver>();
        switch (choice)
        {
            case 0:
                Mask = Abilities.MASKS.FIRE_HELMET;
                sprite_resolver.SetCategoryAndLabel("Masks", "FireExtinguisher");
                break;

            case 1:
                Mask = Abilities.MASKS.STATIONARY;
                sprite_resolver.SetCategoryAndLabel("Masks", "Stationary");
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
