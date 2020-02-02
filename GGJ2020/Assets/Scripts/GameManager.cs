using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Controls Controls => controls;

    public GameplaySettings Settings => settings;

    public GameState GameState => gameState;

    public bool HasGameState => gameState != null;

    public System.Random Random { get; private set; }

    public PrefabContainer Prefabs => prefabs;

    [SerializeField]
    private GameState gameState;

    [SerializeField]
    private GameplaySettings settings;

    [SerializeField]
    private PrefabContainer prefabs;

    [SerializeField]
    private Controls controls = new Controls();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Random = new System.Random();
        controls.Load();
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
