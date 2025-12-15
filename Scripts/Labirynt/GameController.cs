using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public GameOverScreen gameOver;
    public PlayerMovement playerMovement;
    public PlayerAttacked playerAttacked;
    public bool isGameOver = false;

    void Start()
    {
        gameOver.RestartGame();
    }

    void Update()
    {
        if (healthBar.health <= 0 && !isGameOver)
        {
            // DODAJ TO: zatrzymaj wszystkie pu³apki przed Game Over
            playerAttacked.ResetTraps();

            gameOver.GameOver();
            isGameOver = true;
        }
        if (staminaBar.stamina <= 0)
        {
            playerMovement.runSpeed = playerMovement.walkSpeed;
        }
        else
        {
            playerMovement.runSpeed = 12f;
        }
    }
}