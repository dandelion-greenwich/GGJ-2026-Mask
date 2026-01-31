using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Manager : MonoBehaviour
{
    [Header("Player related")] 
    public Transform playerSpawn1;
    public Transform playerSpawn2;
    public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    
    [Header("UI")]
    public TextMeshProUGUI gameStateText;
    
    public enum GameState{Start, Playing, GameOver};
    GameState gameState;
    Abilities player1, player2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public static Manager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        
        Instantiate(playerPrefab1, playerSpawn1.position, playerSpawn1.rotation);
        Instantiate(playerPrefab2, playerSpawn2.position, playerSpawn2.rotation);

        player1 = GameObject.FindGameObjectWithTag("Player 1").GetComponent<Abilities>();
        player2 = GameObject.FindGameObjectWithTag("Player 2").GetComponent<Abilities>();
        
        gameState = GameState.Start;
        UpdateUI();
    }

    public void EndGame()
    {
        gameState = GameState.GameOver;
        UpdateUI();
    }

    public void UpdateUI()
    {
        switch (gameState)
        {
            case GameState.Start:
                gameStateText.text = "Start";
                break;
            case GameState.Playing:
                gameStateText.text = "";
                break;
            case GameState.GameOver:
                gameStateText.text = "Game Over";
                break;
        }
    }
}
