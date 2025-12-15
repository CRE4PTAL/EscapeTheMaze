using UnityEngine;
using System.Collections;

public class ReturningBall : MonoBehaviour
{
    [Header("Obiekty gracza")]
    public HealthBar healthBar;
    public GameObject player;

    [Header("Parametry ruchu")]
    public float moveSpeed = 20f;
    public Vector3 startPoint = new Vector3(78.35f, 5.15f, -237.73f);
    public Vector3 endPoint = new Vector3(78.35f, 5.15f, -94.5f);

    [Header("Obra¿enia i knockback")]
    public float damage = 20f;
    public float knockbackForce = 50f;

    private Vector3 targetPosition;

    [SerializeField] private Vector3 pivotOffset = new Vector3(-38f, -12f, 0f);


    void Start()
    {
        // Na pocz¹tku ustawiamy kulê w punkcie startowym (z offsetem)
        transform.position = startPoint + pivotOffset;
        targetPosition = endPoint + pivotOffset;
    }

    void Update()
    {
        // Ruch w stronê targetPosition (z offsetem)
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        // Jeœli kula dotar³a do celu, zamieniamy target (z offsetem)
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = (targetPosition == startPoint + pivotOffset)
                ? endPoint + pivotOffset
                : startPoint + pivotOffset;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jackpot");
            healthBar.takeDamage(damage);

            // Odpal Coroutine knockbacku
            StartCoroutine(ApplyKnockback(other.transform));
        }
    }

    private IEnumerator ApplyKnockback(Transform playerTransform)
    {
        float knockbackTime = 0.2f; // czas trwania odrzutu
        float elapsed = 0f;

        Vector3 knockbackDirection = (playerTransform.position - transform.position).normalized;

        while (elapsed < knockbackTime)
        {
            playerTransform.position += knockbackDirection * knockbackForce * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
