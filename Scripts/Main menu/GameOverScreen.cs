using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject player;
    public CheckpointManager checkpointManager; // <- zmiana typu na CheckpointManager
    public GameObject gameOver;
    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public GameController gameController;

    void Start()
    {
        gameOver.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        healthBar.gameObject.SetActive(false);
        staminaBar.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // 1) Teleport
        checkpointManager.TeleportToRespawn(player.transform);

        // 2) UI / statystyki
        gameOver.SetActive(false);
        healthBar.gameObject.SetActive(true);
        staminaBar.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        healthBar.health = healthBar.maxHealth;
        staminaBar.stamina = staminaBar.maxStamina;
        gameController.isGameOver = false;

        // 3) wznowienie czasu na koñcu
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}