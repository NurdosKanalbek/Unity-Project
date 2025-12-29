using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(EnemyAI))]

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private EnemySO enemySO;

    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;
    
    private int _currentHealth;
    private int _currentDamage; 

    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private EnemyAI _enemyAI;
    [Header("Loot")]
    [SerializeField] private GameObject healthPickupPrefab; // Префаб сердечка
    [SerializeField] [Range(0, 1)] private float dropChance = 0.3f;

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _enemyAI = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        if (DifficultyManager.Instance != null)
        {
            _currentHealth = DifficultyManager.Instance.GetScaledHealth(enemySO.enemyHealth);
            _currentDamage = DifficultyManager.Instance.GetScaledDamage(enemySO.enemyDamageAmount);
        }
        else
        {
            _currentHealth = enemySO.enemyHealth;
            _currentDamage = enemySO.enemyDamageAmount;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.TakeDamage(transform, _currentDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }

    public void PolygonColliderTurnOff() { _polygonCollider2D.enabled = false; }
    public void PolygonColliderTurnOn() { _polygonCollider2D.enabled = true; }
    
    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            _boxCollider2D.enabled = false;
            _polygonCollider2D.enabled = false;
            _enemyAI.SetDeathState();

            // Сохранение убийств
            int currentKills = PlayerPrefs.GetInt("Kills", 0);
            PlayerPrefs.SetInt("Kills", currentKills + 1);
            PlayerPrefs.Save();

            if (UnityEngine.Random.value <= dropChance)
            {
                if (healthPickupPrefab != null)
                {
                    Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
                }
            }

            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }       
    
}