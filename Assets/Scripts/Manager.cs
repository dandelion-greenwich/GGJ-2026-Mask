using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class Manager : MonoBehaviour
{
    [Header("Player related")] 
    [SerializeField] Transform playerSpawn1;
    [SerializeField] Transform playerSpawn2;
    [SerializeField] GameObject playerPrefab1;
    [SerializeField] GameObject playerPrefab2;
    
    [Header("Mask related")]
    [SerializeField] GameObject maskPrefab;
    [SerializeField] Transform maskSpawn;
    [SerializeField] int maskSpawnRange;
    [SerializeField] float maskTimer;
    [SerializeField] float timeToBreakMask;
    
    [Header("UI")]
    public TextMeshProUGUI gameStateText;
    
    public enum GameState{Start, Playing, GameOver};
    GameState gameState;
    GameObject player1, player2;
    GameObject currentMask;
    public event Action OnResetMask;
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

        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
        
        gameState = GameState.Start;
        UpdateUI();
    }

    private void Start()
    {
        StartCoroutine(SpawnMaskCoroutine(5f));
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

    private void SpawnMask()
    {
        Debug.Log("Spawn Mask");
        int randX = UnityEngine.Random.Range(-maskSpawnRange, maskSpawnRange);
        int randZ = UnityEngine.Random.Range(-maskSpawnRange, maskSpawnRange);
        Vector3 maskTransform = new Vector3(
            maskSpawn.position.x + randX, 
            maskSpawn.position.y, 
            maskSpawn.position.z + randZ);
        
        currentMask = Instantiate(maskPrefab, maskSpawn.position, maskSpawn.rotation);
        StartCoroutine(LiveDurationCoroutine(timeToBreakMask));
    }

    public void Respawn(GameObject player)
    {
        if (player.CompareTag("Player 1"))
        {
            Debug.Log("Respawning player 1");
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                player1.transform.position = playerSpawn1.position;
                cc.enabled = true;
            }
        }
        else
        {
            Debug.Log("Respawning player 2");
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                player2.transform.position = playerSpawn2.position;
                cc.enabled = true;
            }
        }
    }
    
    public IEnumerator SpawnMaskCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        SpawnMask();
    }
    public IEnumerator LiveDurationCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (currentMask != null) Destroy(currentMask);
        OnResetMask.Invoke();
        
        StartCoroutine(SpawnMaskCoroutine(5f));
    }
}
