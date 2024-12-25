#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    public UnityEngine.UI.InputField nameInputField;
    public Text ScoreboardText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreboard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateScoreboard()
    {
        if (ScoreboardText == null)
        {
            Debug.LogWarning("ScoreboardText reference is missing.");
            return;
        }

        List<ScoreBoardManager.PlayerScore> scores = ScoreBoardManager.Instance.GetScoreboard();

        // Sort scores in descending order
        scores.Sort((a, b) => b.score.CompareTo(a.score));

        // Update the Text UI with the player scores
        ScoreboardText.text = "Scoreboard:\n";
        foreach (var player in scores)
        {
            ScoreboardText.text += $"{player.name}: {player.score}\n";
        }
    }

    public void StartGame()
    {

        string playerName = nameInputField.text;

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Player name is empty. Please enter a name.");
            return;
        }

        // Save the player data with an initial score of 0
        DataManager.SavePlayerData(playerName, 0);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
