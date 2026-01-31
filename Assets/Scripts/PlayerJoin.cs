using System;
using UnityEngine;

public class PlayerJoin : MonoBehaviour
{
    public Transform playerSpawn1, playerSpawn2;
    public GameObject playerPrefab1, playerPrefab2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instantiate(playerPrefab1, playerSpawn1.position, playerSpawn1.rotation);
        Instantiate(playerPrefab2, playerSpawn2.position, playerSpawn2.rotation);
    }
}
