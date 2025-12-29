using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel; // Ссылка на панель
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        // Скрываем панель при старте
        gameOverPanel.SetActive(false);

        // Подписываемся на смерть игрока
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;

        // Настраиваем кнопки
        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(LoadMenu);
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        // Показываем панель и курсор
        gameOverPanel.SetActive(true);
        // Если у тебя скрыт курсор в игре, раскомментируй это:
        // Cursor.visible = true; 
        // Cursor.lockState = CursorLockMode.None;
    }

    private void RestartGame()
    {
        // 1. Сбрасываем здоровье в сохранении на максимум
        SaveManager.ResetSave(Player.Instance.maxHealth);

        // 2. Перезагружаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

private void LoadMenu()
{
    // !!! ДОБАВИТЬ ЭТУ СТРОКУ: Сбрасываем здоровье перед выходом в меню
    SaveManager.ResetSave(Player.Instance.maxHealth); // (Если поле maxHealth не видно, сделайте его public в Player.cs)

    Time.timeScale = 1f; 
    SceneManager.LoadScene(0); 
}
}