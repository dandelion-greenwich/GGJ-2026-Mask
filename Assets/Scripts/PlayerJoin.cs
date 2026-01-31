using System;
using UnityEngine;

public class PlayerJoin : MonoBehaviour
{
    public Transform playerSpawn1, playerSpawn2;
    public GameObject playerPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instantiate(playerPrefab, playerSpawn1.position, playerSpawn1.rotation);
        Instantiate(playerPrefab, playerSpawn2.position, playerSpawn2.rotation);
    }
}
