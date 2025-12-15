using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public Slider staminaSlider;
    public Slider easeStaminaSlider;
    public float maxStamina = 100f;
    public float stamina;
    private float lerpSpeed = 0.02f;
    private float timer = 0f;  // Timer dla sprintu
    private float regenTimer = 0f;  // Timer dla regeneracji
    private bool isSprinting = false;  // Sprawdza, czy sprintujesz
    private bool isRegenerating = false;  // Sprawdza, czy regeneracja jest aktywna
    private float regenDelay = 3f;  // Czas oczekiwania przed rozpoczêciem regeneracji
    private float regenRate = 0.1f;  // Czas potrzebny do regeneracji 1 punktu staminy
    private float timeSinceLastRegeneration = 0f; // Czas od ostatniej regeneracji

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
    }

    void Update()
    {
        timer += Time.deltaTime;
        regenTimer += Time.deltaTime;

        // Sprint: Zmniejsza staminê, jeœli trzymasz shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            if (timer >= 0.1f)
            {
                takeStamina(1);  // Zmniejszamy staminê o 1 co 0.1 sekundy
                timer = 0f;
            }

            // Resetowanie regeneracji, gdy sprintujesz
            regenTimer = 0f;
            isRegenerating = false; // Jeœli sprintujesz, nie regeneruj staminy
        }
        else
        {
            isSprinting = false;

            // Po 3 sekundach bez sprintu zaczynamy regenerowaæ
            if (stamina < maxStamina && regenTimer >= regenDelay)
            {
                isRegenerating = true; // Rozpoczynamy regeneracjê
            }
        }

        if (stamina >= 0)
        {
            playerMovement.canJump = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                takeStamina(10);
            }
        }
        else
        {
            playerMovement.moveDirection.y = 0;
        }

        // Jeœli regeneracja jest w³¹czona, przywracamy 1 punkt staminy co 0.5 sekundy
        if (isRegenerating && timeSinceLastRegeneration >= regenRate)
        {
            regenerateStamina(5);
            timeSinceLastRegeneration = 0f; // Resetujemy czas od ostatniej regeneracji
        }
        else
        {
            timeSinceLastRegeneration += Time.deltaTime; // Zwiêkszamy czas od ostatniej regeneracji
        }

        // Aktualizowanie paska staminy
        if (staminaSlider.value != stamina)
        {
            staminaSlider.value = stamina;
        }

        // P³ynna zmiana wartoœci paska
        if (staminaSlider.value != easeStaminaSlider.value)
        {
            easeStaminaSlider.value = Mathf.Lerp(easeStaminaSlider.value, stamina, lerpSpeed);
        }
    }

    void takeStamina(float take)
    {
        stamina -= take;
        if (stamina < 0) stamina = 0; // Zapewnienie, ¿e stamina nie spadnie poni¿ej 0
    }

    void regenerateStamina(float restore)
    {
        if (stamina < maxStamina)
        {
            stamina += restore;
            if (stamina > maxStamina) stamina = maxStamina; // Zapewnienie, ¿e stamina nie przekroczy max
        }
    }
}