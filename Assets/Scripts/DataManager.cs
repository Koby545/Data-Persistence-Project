using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static readonly string SaveFilePath = Application.persistentDataPath + "/PlayerData.json";

    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int score;

        public PlayerData(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

    public static void SavePlayerData(string playerName, int playerScore)
    {
        PlayerData data = new PlayerData(playerName, playerScore);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SaveFilePath, json);
        Debug.Log($"Player data saved: {json}");
    }

    public static PlayerData LoadPlayerData()
    {
        if (File.Exists(SaveFilePath))
        {
            string json = File.ReadAllText(SaveFilePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log($"Player data loaded: {json}");
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found.");
            return null;
        }
    }
}
