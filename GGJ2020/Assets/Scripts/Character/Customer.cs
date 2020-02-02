using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private LinearPath path;

    private BoxCollider trigger;

    public Objective Objective { get; private set; }

    public int Index { get; set; }

    public LinearPath Path { get => path; }

    [SerializeField]
    private CharacterSpriteManager spritesWithSword;

    [SerializeField]
    private CharacterSpriteManager spritesWithoutSword;

    private bool hasSword = true;

    private bool waiting;
    private float timer;

    private Player player;

    private void Start()
    {
        path.Owner = transform.position;
        trigger = GetComponent<BoxCollider>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (!waiting)
            return;

        if (timer <= 0)
        {
            ObjectiveCompleted();
            return;
        }

        timer -= Time.deltaTime;

        UpdateAnimation(
            Vector3.Distance(player.transform.position, transform.position) < GameManager.Instance.Settings.DuckReactRange ? 
            CharacterSpriteManager.CharacterState.BackwardHeadUp : 
            CharacterSpriteManager.CharacterState.Backward
            );
    }

    public IEnumerator Move(bool forward)
    {
        float time = 0.0f;
        float totalTime = GameManager.Instance.Settings.CustomerMoveTime;

        if (totalTime != 0.0f)
        {
            while (time < totalTime)
            {
                float interpolate = (forward ? time / totalTime : 1 - time / totalTime);
                transform.position = path.Lerp(interpolate);
                yield return null;
                time += Time.deltaTime;
            }
        }
        else
        {
            transform.position = path.Lerp(forward ? 1 : -1);
        }

        if (forward)
        {
            InitializeWaiting();
        }
        else
        {
            Objective = null;
            GameManager.Instance.GameState.ObjectiveQueue.FreeSlot(Index);
        }
    }

    private void InitializeWaiting()
    {
        Sword sword = Instantiate(GameManager.Instance.Prefabs.Sword, transform.position + trigger.center, Quaternion.identity);
        sword.Initialize(Objective);
        hasSword = false;
        UpdateAnimation(CharacterSpriteManager.CharacterState.Backward);
        waiting = true;
        timer = GameManager.Instance.Settings.WaitTimer;
    }

    public void Initialize(Objective objective)
    {
        Objective = objective;
        StartCoroutine(Move(true));
        hasSword = true;
        UpdateAnimation(CharacterSpriteManager.CharacterState.BackwardHeadUp);
    }

    private void UpdateAnimation(CharacterSpriteManager.CharacterState state)
    {
        spritesWithoutSword.gameObject.SetActive(!hasSword);
        spritesWithoutSword.SetState(state);
        spritesWithSword.gameObject.SetActive(hasSword);
        spritesWithSword.SetState(state);
    }

    public void ObjectiveCompleted()
    {
        StartCoroutine(Move(false));
        waiting = false;
        UpdateAnimation(CharacterSpriteManager.CharacterState.Forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        Sword sword = other.GetComponent<Sword>();
        if (sword != null)
        {
            if (Objective.DoesMatch(sword))
            {
                Destroy(sword.gameObject);
                hasSword = true;
                ObjectiveCompleted();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(path.Start + path.Owner, path.End + path.Owner);
        Gizmos.DrawCube(path.Start + path.Owner, Vector3.one * 0.2f);
        Gizmos.DrawCube(path.End + path.Owner, Vector3.one * 0.2f);

        if (Objective == null)
            return;

        Gizmos.color = Objective.Grind ? Color.white : Color.black;
        Gizmos.DrawCube(transform.position + Vector3.up * 1.9f, Vector3.one * 0.3f);
        Gizmos.color = Objective.Grip ? Color.white : Color.black;
        Gizmos.DrawCube(transform.position + Vector3.up * 1.6f, Vector3.one * 0.3f);
        Gizmos.color = Objective.Smith ? Color.white : Color.black;
        Gizmos.DrawCube(transform.position + Vector3.up * 1.3f, Vector3.one * 0.3f);
    }
}
