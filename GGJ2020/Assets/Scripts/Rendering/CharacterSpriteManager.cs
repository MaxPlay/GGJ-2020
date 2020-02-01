using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteManager : MonoBehaviour
{
    public enum CharacterState
    {
        Forward,
        Right,
        Backward,
        Left,
        BackwardHeadUp
    }

    [Serializable]
    struct State
    {
        public CharacterState CharacterState;
        public GameObject Orientation;
    }

    [SerializeField]
    State[] spriteOrientations;

    Dictionary<CharacterState, GameObject> spriteOrientationsDictionary;

    private void Awake()
    {
        spriteOrientationsDictionary = new Dictionary<CharacterState, GameObject>();
        for (int i = 0; i < spriteOrientations.Length; i++)
        {
            if (!spriteOrientationsDictionary.ContainsKey(spriteOrientations[i].CharacterState))
                spriteOrientationsDictionary.Add(spriteOrientations[i].CharacterState, spriteOrientations[i].Orientation);
        }

        SetState(CharacterState.Forward);
    }

    public void SetState(CharacterState direction)
    {
        foreach (var item in spriteOrientationsDictionary)
        {
            item.Value.SetActive(item.Key == direction);
        }
    }
}