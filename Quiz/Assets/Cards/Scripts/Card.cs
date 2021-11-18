using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private Transform _spripteTransform;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private LevelHandler _levelHandler;
    private string _identifier;

    public void DeleteCard()
    {
        Destroy(gameObject);
    }

    public void LoadCardData(string identifier, Sprite sprite, LevelHandler levelHandler)
    {
        _identifier = identifier;
        _spriteRenderer.sprite = sprite;
        _levelHandler = levelHandler;
    }

    private void OnMouseDown() 
    {
        _levelHandler.CheckCardIdentifier(_identifier, _spripteTransform);
    }
}
