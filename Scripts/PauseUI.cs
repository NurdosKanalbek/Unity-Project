using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Slider volumeSlider;

    private bool _isPaused = false;

    private void Start()
    {
        // Скрываем паузу при старте
        pausePanel.SetActive(false);

        // Настраиваем кнопки
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(LoadMenuWithSave);
        if (volumeSlider != null)
        {
            // Ставим ползунок в текущее положение громкости
            volumeSlider.value = AudioListener.volume;
            // Говорим: "Когда ползунок двигают, вызывай метод ChangeVolume"
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }
    }
    private void ChangeVolume(float value)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.SetVolume(value);
    }

    private void Update()
    {
        // Если нажали ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Не позволяем ставить на паузу, если игрок уже умер (там свое меню)
            if (Player.Instance.IsAlive()) 
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f; // ОСТАНАВЛИВАЕМ ВРЕМЯ
            if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMusic(AudioManager.Instance.menuMusic);
        }
        else
        {
            ResumeGame();
        }
    }

    private void ResumeGame()
    {
        _isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // ВОЗВРАЩАЕМ ВРЕМЯ
        if (AudioManager.Instance != null)
        AudioManager.Instance.PlayMusic(AudioManager.Instance.gameplayMusic);
    }

    private void RestartGame()
    {
        // При рестарте мы хотим начать С ЧИСТОГО ЛИСТА
        Time.timeScale = 1f; // Обязательно возвращаем время перед загрузкой!
        SaveManager.ResetSave(Player.Instance.maxHealth);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadMenuWithSave()
    {
        // !!! ГЛАВНОЕ: Сохраняем текущее состояние перед выходом
        SaveManager.SaveGame(Player.Instance);
        Debug.Log("Game Saved on Pause Exit");

        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Грузим меню (индекс 0)
    }
}