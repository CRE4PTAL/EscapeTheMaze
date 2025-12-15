using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [Header("Startowy spawn (opcjonalny)")]
    public Transform defaultSpawn;

    [Header("Odczyt (debug)")]
    public Vector3 currentRespawn;

    CharacterController cc;
    Rigidbody rb;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Ustaw punkt startowy: najpierw defaultSpawn (jeœli przypisany), inaczej aktualna pozycja gracza
        currentRespawn = defaultSpawn ? defaultSpawn.position : transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Gracz wchodzi w trigger checkpointu
        if (other.CompareTag("Checkpoint"))
        {
            // Jeœli checkpoint ma dziecko "RespawnPoint", to u¿yj jego pozycji (wygodne do precyzyjnego ustawienia)
            Transform respawnPoint = other.transform.Find("RespawnPoint");
            currentRespawn = respawnPoint ? respawnPoint.position : other.transform.position;

            // (opcjonalnie) dezaktywuj checkpoint po zebraniu
            other.gameObject.SetActive(false);
        }
    }

    public void TeleportToRespawn(Transform player)
    {
        if (cc) cc.enabled = false; // wy³¹cz CC

        Vector3 safePos = currentRespawn + Vector3.up * 0.5f; // podnieœ gracza lekko nad ziemiê
        player.position = safePos;
        player.rotation = Quaternion.identity; // opcjonalnie reset rotacji

        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (cc) cc.enabled = true; // w³¹cz CC
    }
}
