using UnityEngine;

public class Banana : MonoBehaviour
{
    public Vector3 targetPos = Vector3.zero;
    public Vector3 startPos = Vector3.zero;

    [SerializeField]
    float timeToGetToFloor = 1.5f;

    private float lerpTimer = 0;
    private CircleCollider2D cd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cd = GetComponent<CircleCollider2D>();
        cd.enabled = false;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpTimer < 1f)
        {
            lerpTimer += Time.deltaTime / timeToGetToFloor;
        }
        else
        {
            cd.enabled = true;
        }
        transform.position = Vector3.Lerp(startPos, targetPos, lerpTimer);
    }
}
