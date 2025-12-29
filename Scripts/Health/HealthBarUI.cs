using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthFillImage;

    private void Start()
    {
        // Подписываемся на событие из Player.cs
        Player.Instance.OnHealthChanged += Player_OnHealthChanged;
    }

    private void Player_OnHealthChanged(object sender, float healthPercent)
    {
        healthFillImage.fillAmount = healthPercent;
    }
}