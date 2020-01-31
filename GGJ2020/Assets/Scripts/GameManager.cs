using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameplaySettings Settings => settings;

    public GameState GameState => gameState;

    public bool HasGameState => gameState != null;

    public System.Random Random { get; private set; }

    [SerializeField]
    private GameState gameState;

    [SerializeField]
    private GameplaySettings settings;

    private void Awake()
    {
        Instance = this;
        Random = new System.Random();
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        gameState = ScriptableObject.CreateInstance<GameState>();
        gameState.Start();
    }

    private void Update()
    {
        if(gameState != null)
        {
            gameState.Update();
        }
    }

    public void EndGame()
    {
        gameState.End();
    }
}
