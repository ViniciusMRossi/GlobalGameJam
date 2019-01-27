using System;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private enum GameState
    {
        PreStart,
        Playing,
        ShowingWinner
    }

    public GameObject startMenu;
    public GameObject endMenu;
    public GameObject inGameMenu;
    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;
    public TextMeshProUGUI player1ScoreFinal;
    public TextMeshProUGUI player2ScoreFinal;
    public TextMeshProUGUI messageFinal;
    public TextMeshProUGUI time;
    public int matchTimeSeconds = 60;
    public Player[] players;
    public int pointsPerHit = 1;

    private GameState _gameState;
    private float _remainingTime;
    private int _player1Score;
    private int _player2Score;
    private bool _paused;

    private void Start()
    {
        startMenu.SetActive(true);
        inGameMenu.SetActive(false);
    }

    private void Update()
    {
        HandleStartPressed();
        HandleTimePassing();
    }

    private void HandleTimePassing()
    {
        if (_gameState != GameState.Playing) return;

        _remainingTime -= Time.deltaTime;
        time.text = Mathf.CeilToInt(_remainingTime).ToString();

        if (!(_remainingTime <= 0)) return;

        FinishGame();
    }

    private void FinishGame()
    {
        _remainingTime = 0;
        _gameState = GameState.ShowingWinner;
        endMenu.SetActive(true);
        inGameMenu.SetActive(false);
        player1ScoreFinal.text = _player1Score.ToString();
        player2ScoreFinal.text = _player2Score.ToString();
        if (_player1Score > _player2Score)
        {
            messageFinal.text = "Player 1 wins!";
        }
        else if (_player2Score > _player1Score)
        {
            messageFinal.text = "Player 2 wins!";
        }
        else
        {
            messageFinal.text = "It's a draw!";
        }
        
        
        foreach (var player in players)
        {
            player.StopInputs();
        }
    }

    private void HandleStartPressed()
    {
        if (!Input.GetButton("Pause1") && !Input.GetButton("Pause2")) return;
        switch (_gameState)
        {
            case GameState.PreStart:
                StartGame();
                break;
            case GameState.Playing:
                SwitchPause();
                break;
            case GameState.ShowingWinner:
                Application.Quit();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SwitchPause()
    {
        _paused = !_paused;
    }

    private void StartGame()
    {
        startMenu.SetActive(false);
        inGameMenu.SetActive(true);

        _gameState = GameState.Playing;
        _remainingTime = matchTimeSeconds;

        foreach (var player in players)
        {
            player.StartInputs();
        }

        player1Score.text = _player1Score.ToString();
        player2Score.text = _player2Score.ToString();

        _paused = false;
    }

    public void OnPlayer1GotHit()
    {
        _player2Score += pointsPerHit;
        player2Score.text = _player2Score.ToString();
    }

    public void OnPlayer2GotHit()
    {
        _player1Score += pointsPerHit;
        player1Score.text = _player1Score.ToString();
    }
}