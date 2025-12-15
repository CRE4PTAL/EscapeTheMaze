using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacked : MonoBehaviour
{
    public HealthBar healthBar;
    private float timer = 0;
    private bool checkTimer = false;
    public GameObject player;
    private bool isDone;
    private Vector3 targetPosition;
    private bool isMoving;
    public float moveSpeed = 100f;
    private bool isTrap2 = false;
    private bool isTrap1 = false;
    private bool isTrap3 = false;
    private Coroutine trap2Coroutine;

    private void Start()
    {
        isDone = true;
        isTrap2 = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap1" && isDone)
        {
            checkTimer = true;
            targetPosition = player.transform.position + new Vector3(50, 0, 0);
            isDone = false;
            isMoving = true;
            isTrap1 = true;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Trap2")
        {
            if (!isTrap2)
            {
                isTrap2 = true;
                trap2Coroutine = StartCoroutine(ApplyTrap2Damage());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Trap2")
        {
            isTrap2 = false;
            if (trap2Coroutine != null)
            {
                StopCoroutine(trap2Coroutine);
                trap2Coroutine = null;
            }
        }
    }

    private void Update()
    {
        if (isTrap1)
        {
            if (checkTimer)
            {
                timer += Time.deltaTime;
                healthBar.takeDamage(20);
                checkTimer = false;
            }
            if (isMoving)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            if (player.transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }

    private IEnumerator ApplyTrap2Damage()
    {
        while (isTrap2)
        {
            healthBar.takeDamage(10);
            yield return new WaitForSeconds(1f);
        }
    }

    // NOWA METODA - wywo³aj j¹ przy restarcie gry
    public void ResetTraps()
    {
        // Zatrzymaj korutynê Trap2 jeœli dzia³a
        if (trap2Coroutine != null)
        {
            StopCoroutine(trap2Coroutine);
            trap2Coroutine = null;
        }

        // Zresetuj wszystkie flagi
        isTrap2 = false;
        isTrap1 = false;
        isTrap3 = false;
        checkTimer = false;
        isMoving = false;
        isDone = true;
        timer = 0;
    }
}