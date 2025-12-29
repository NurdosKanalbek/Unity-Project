using UnityEngine;

public static class SaveManager
{
    private const string HEALTH_KEY = "PlayerHealth";
    private const string KILLS_KEY = "Kills";

    private const string POS_X_KEY = "PosX";
    private const string POS_Y_KEY = "PosY";

    public static void SaveGame(Player player)
    {
        // Сохраняем, только если игрок жив
        if (player.GetHealth() > 0)
        {
            PlayerPrefs.SetInt(HEALTH_KEY, player.GetHealth());
            
            // Сохраняем позицию
            PlayerPrefs.SetFloat(POS_X_KEY, player.transform.position.x);
            PlayerPrefs.SetFloat(POS_Y_KEY, player.transform.position.y);
            
            PlayerPrefs.Save();
            Debug.Log("Game Saved: Health & Position");
        }
    }

    public static int LoadHealth(int defaultMaxHealth)
    {
        int loadedHealth = PlayerPrefs.GetInt(HEALTH_KEY, defaultMaxHealth);
        if (loadedHealth <= 0) return defaultMaxHealth;
        return loadedHealth;
    }

    // Загрузка позиции
    public static Vector3 LoadPosition(Vector3 defaultPosition)
    {
        float x = PlayerPrefs.GetFloat(POS_X_KEY, defaultPosition.x);
        float y = PlayerPrefs.GetFloat(POS_Y_KEY, defaultPosition.y);
        
        return new Vector3(x, y, defaultPosition.z);
    }

    public static void ResetSave(int maxHealth)
    {
        PlayerPrefs.SetInt(HEALTH_KEY, maxHealth);
        PlayerPrefs.SetInt(KILLS_KEY, 0);
        PlayerPrefs.DeleteKey(POS_X_KEY);
        PlayerPrefs.DeleteKey(POS_Y_KEY);

        PlayerPrefs.Save();
    }
}