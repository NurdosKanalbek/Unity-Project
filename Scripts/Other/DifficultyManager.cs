using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    [Header("Настройки сложности")]
    [SerializeField] private float difficultyStepInterval = 30f; // Каждые 30 секунд игра усложняется
    [SerializeField] private float damageMultiplierPerStep = 0.2f; // +20% урона за шаг
    [SerializeField] private float healthMultiplierPerStep = 0.2f; // +20% здоровья за шаг
    [SerializeField] private int maxDamageCap = 50; // Максимальный урон чтобы не ваншотнули

    private float _timer;
    private int _difficultyLevel = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!Player.Instance.IsAlive()) return;

        _timer += Time.deltaTime;

        if (_timer >= difficultyStepInterval)
        {
            IncreaseDifficulty();
            _timer = 0f;
        }
    }

    private void IncreaseDifficulty()
    {
        _difficultyLevel++;
        Debug.Log($"<color=red>УРОВЕНЬ СЛОЖНОСТИ ПОВЫШЕН: {_difficultyLevel}</color>");
    }

    public int GetScaledHealth(int baseHealth)
    {
        float multiplier = 1 + (_difficultyLevel * healthMultiplierPerStep);
        return Mathf.RoundToInt(baseHealth * multiplier);
    }
    public int GetScaledDamage(int baseDamage)
    {
        float multiplier = 1 + (_difficultyLevel * damageMultiplierPerStep);
        int newDamage = Mathf.RoundToInt(baseDamage * multiplier);
        return Mathf.Min(newDamage, maxDamageCap);
    }
    
    public int GetDifficultyLevel() => _difficultyLevel;
}