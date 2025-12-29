using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour 
{
    [Header("Кнопки Меню")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton; // !!! НОВАЯ КНОПКА
    [SerializeField] private Button quitButton;

    [Header("Панель Настроек")]
    [SerializeField] private GameObject settingsPanel; // Сама панель
    [SerializeField] private Button backButton;        // Кнопка "Назад" в панели
    [SerializeField] private Slider volumeSlider;      // Слайдер громкости

    private void Awake() 
    {
        // Скрываем настройки при старте
        settingsPanel.SetActive(false);

        playButton.onClick.AddListener(() => {
            SceneManager.LoadScene("SampleScene"); 
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
            Debug.Log("Game Quit");
        });

        // !!! ЛОГИКА НАСТРОЕК
        settingsButton.onClick.AddListener(() => {
            settingsPanel.SetActive(true); // Показать панель
        });

        backButton.onClick.AddListener(() => {
            settingsPanel.SetActive(false); // Скрыть панель
        });

        // Настройка слайдера (то же самое, что мы делали в паузе)
        if (volumeSlider != null)
        {
            // 1. При загрузке меню выставляем ручку слайдера как надо
            volumeSlider.value = AudioListener.volume;
            
            // 2. Подписываемся: двигаешь ручку -> меняется звук
            volumeSlider.onValueChanged.AddListener((float value) => {
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.SetVolume(value);
                }
            });
        }
    }

    private void Start()
    {
        // Запускаем музыку меню
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.menuMusic);
        }
    }
}