using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private LevelHandler _levelHandler;
    [SerializeField] private GameObject _card;
    [SerializeField] private float _cardSize;
    [SerializeField] private float _cardSpacing;
    [SerializeField] private Transform _generateZonePoint;
    private List<CardData> _levelCardsData;

    private void Start() 
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        _levelCardsData = _levelGenerator.GenerateLevelData();
        float perCardOffset = _cardSize + _cardSpacing;
        float horizontalOffset = perCardOffset / 2 * _levelGenerator.Level.Columns - perCardOffset / 2;
        float verticalOffset = perCardOffset / 2 * _levelGenerator.Level.Lines - perCardOffset / 2;

        Vector3[] cardPositions = new Vector3[_levelGenerator.Level.CardCount];
        int index = 0;
        for(float x = -horizontalOffset; x <= horizontalOffset; x += perCardOffset)
        {
            for(float y = -verticalOffset; y <= verticalOffset; y += perCardOffset)
            {
                cardPositions[index] = _generateZonePoint.position;
                cardPositions[index].x += x;
                cardPositions[index].y += y;
                index++;
            }
        }
            
        foreach(Vector3 cardPosition in cardPositions)
        {
            LoadCard(cardPosition, _levelCardsData[0]);
            _levelCardsData.Remove(_levelCardsData[0]);
        }
    }

    private void LoadCard(Vector3 cardPosition, CardData cardData)
    {
        Quaternion defaultRotate = Quaternion.Euler(0, 0, cardData.DefaultZRotate);
        Card card = Instantiate(_card, cardPosition, defaultRotate).GetComponent<Card>();
        card.LoadCardData(cardData.Identifier, cardData.Sprite, _levelHandler);
        card.transform.SetParent(_generateZonePoint);
        _levelHandler.AddListenerToCompleteLevel(card.DeleteCard);
    }
}