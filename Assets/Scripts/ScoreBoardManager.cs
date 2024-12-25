using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ScoreBoardManager : MonoBehaviour
{
    public static ScoreBoardManager Instance;

    [System.Serializable]
    public class PlayerScore
    {
        public string name;
        public int score;

        public PlayerScore(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

    [System.Serializable]
    private class PlayerScoreList
    {
        public List<PlayerScore> playerScores;
    }

    private List<PlayerScore> playerScores = new List<PlayerScore>();
    private string filePath;

    private void Awake()
    {
        // Singleton pattern to ensure a single instance of ScoreboardManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Set the file path for saving the scoreboard data
        filePath = Application.persistentDataPath + "/scoreboard.json";

        // Load player scores from the saved file
        LoadScoreboard();
    }

    // Add a new player or update an existing player's score
    public void AddOrUpdatePlayer(string playerName, int score)
    {
        PlayerScore existingPlayer = playerScores.Find(player => player.name == playerName);

        if (existingPlayer != null)
        {
            if (score > existingPlayer.score)
            {
                existingPlayer.score = score;
            }
        }
        else
        {
            playerScores.Add(new PlayerScore(playerName, score));
        }

        // Save the updated scoreboard after adding/updating a player
        SaveScoreboard();
    }

    // Get the list of all players and scores
    public List<PlayerScore> GetScoreboard()
    {
        return new List<PlayerScore>(playerScores);
    }

    // Save the scoreboard to a file
    private void SaveScoreboard()
    {
        PlayerScoreList scoreList = new PlayerScoreList();
        scoreList.playerScores = playerScores;

        string json = JsonUtility.ToJson(scoreList, true);
        File.WriteAllText(filePath, json);
    }

    // Load the scoreboard from a file
    private void LoadScoreboard()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerScoreList scoreList = JsonUtility.FromJson<PlayerScoreList>(json);

            playerScores = scoreList.playerScores;
        }
    }

    // For debugging: Print the scoreboard
    public void PrintScoreboard()
    {
        foreach (var player in playerScores)
        {
            Debug.Log($"Player: {player.name}, Score: {player.score}");
        }
    }
}
