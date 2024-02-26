using UnityEngine;
using TMPro; // Import TextMeshPro

public class LeapfrogTrigger : MonoBehaviour
{
    public string opponentTag;
    public float cooldown = 1f;
    public int winningScore = 4; // Specify the score needed to win

    private Collider player1Collider;
    private Collider player2Collider;
    private float nextTriggerTime = 0f;
    private int player1Score = 0;
    private int player2Score = 0;
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI element
    public TextMeshProUGUI winText; // Reference to the TextMeshProUGUI element for win message

    void Start()
    {
        // Find colliders with specified tags
        player1Collider = GameObject.FindGameObjectWithTag("Player1").GetComponent<Collider>();
        player2Collider = GameObject.FindGameObjectWithTag("Player2").GetComponent<Collider>();

        // Initialize score text
        UpdateScoreText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (Time.time >= nextTriggerTime)
        {
            if (other == player2Collider)
            {
                player1Score++;
                Debug.Log("Player 1 jumped player 2 " + player1Score + " times");
                UpdateScoreText();
                CheckWin();
                nextTriggerTime = Time.time + cooldown;
            }
            else if (other == player1Collider)
            {
                player2Score++;
                Debug.Log("Player 2 jumped player 1 " + player2Score + " times");
                UpdateScoreText();
                CheckWin();
                nextTriggerTime = Time.time + cooldown;
            }
        }
    }

    void UpdateScoreText()
    {
        // Update score text UI
        scoreText.text = "Player 1: " + player1Score + "\nPlayer 2: " + player2Score;
    }

    void CheckWin()
    {
        // Check if either player has reached the winning score
        if (player1Score >= winningScore)
        {
            ShowWinMessage("Player 1");
        }
        else if (player2Score >= winningScore)
        {
            ShowWinMessage("Player 2");
        }
    }

    void ShowWinMessage(string player)
    {
        // Display win message in the middle of the screen
        winText.text = player + " Won!";
    }
}
