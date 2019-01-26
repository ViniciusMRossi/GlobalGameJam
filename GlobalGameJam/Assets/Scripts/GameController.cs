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
    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;
    public TextMeshProUGUI time;
    public AudioSource audioSource;
    public int matchTimeSeconds = 60;
    public Player[] players;
    public int pointsPerHit = 1;

    private GameState _gameState;
    private float _remainingTime;
    private int _player1Score;
    private int _player2Score;
    private bool _paused;
    
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
    }

    private void HandleStartPressed()
    {
        if (Input.GetButton("Pause1") || Input.GetButton("Pause2"))
        {
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
    }

    private void SwitchPause()
    {
        _paused = !_paused;

        Time.timeScale = _paused ? 0 : 1;
    }

    public void StartGame()
    {
        _gameState = GameState.Playing;
        _remainingTime = matchTimeSeconds;
        
        foreach (var player in players)
        {
            player.StartPlaying();
        }

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