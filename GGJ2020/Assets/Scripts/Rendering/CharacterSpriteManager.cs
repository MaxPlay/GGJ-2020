using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteManager : MonoBehaviour
{
    public enum CharacterDirection
    {
        Forward,
        Right,
        Backward,
        Left
    }

    [Serializable]
    struct Direction
    {
        public CharacterDirection CharacterDirection;
        public SpriteOrientation Orientation;
    }

    [SerializeField]
    Direction[] spriteOrientations;

    Dictionary<CharacterDirection, SpriteOrientation> spriteOrientationsDictionary;

    private void Awake()
    {
        spriteOrientationsDictionary = new Dictionary<CharacterDirection, SpriteOrientation>();
        for (int i = 0; i < spriteOrientations.Length; i++)
        {
            if (!spriteOrientationsDictionary.ContainsKey(spriteOrientations[i].CharacterDirection))
                spriteOrientationsDictionary.Add(spriteOrientations[i].CharacterDirection, spriteOrientations[i].Orientation);
        }

        SetDirection(CharacterDirection.Forward);
    }

    public void SetDirection(CharacterDirection direction)
    {
        foreach (var item in spriteOrientationsDictionary)
        {
            item.Value.gameObject.SetActive(item.Key == direction);
        }
    }
}