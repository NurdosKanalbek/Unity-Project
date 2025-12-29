using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 2; // Сколько жизней восстановит

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, наступил ли на нас игрок
        if (collision.TryGetComponent(out Player player))
        {
            // Лечим игрока
            player.Heal(healAmount);
            
            // Уничтожаем сердечко (оно "подобралось")
            Destroy(gameObject);
        }
    }
}