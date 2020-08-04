/* Snowboarding1 - Logan Shehane
 * 
 * This script is for the board, it Clamps the speed to a float you configure in the inspector.
 */
using UnityEngine;

public class Board2 : MonoBehaviour
{
    public float maxSpeed = 60f;
    public float minSpeed = 2f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Get the Component so we can read abd modify it.
    }

    // Update is called once per frame
    void Update()
    {
        clampSpeed();
    }
    void clampSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        if (rb.velocity.magnitude < minSpeed)
        {
            rb.velocity = rb.velocity.normalized * minSpeed;
        }
    }
}
