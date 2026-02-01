using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float strength = 2;
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            Vector2 dir = -collision.GetContact(0).normal;
            Vector2 point =  collision.GetContact(0).point;
            float magnitude = collision.GetContact(0).normalImpulse * strength;
            
            collision.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(dir * magnitude, point, ForceMode2D.Impulse);
            print($"Hit {collision.gameObject.name} with {magnitude} magnitude");
        }
    }
}
