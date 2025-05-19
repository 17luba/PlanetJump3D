using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravityForce = 9.8f;
    public Transform planet;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Gravité personnalisée vers la planète
        Vector3 gravityDir = (transform.position - planet.position).normalized;
        rb.AddForce(-gravityDir * gravityForce);

        // Aligner le joueur vers la planète
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, gravityDir) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);

        // Déplacement autour de la planète
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);

        // Jetpack Jump
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }
}
