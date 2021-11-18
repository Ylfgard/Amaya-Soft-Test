using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private CardDataKeeper _cardDataKeeper;
    [SerializeField] private LevelHandler _levelHandler;
    [SerializeField] private Level[] _levels;
    private int _currentLevelIndex = 0;
    private List<CardData> _choosedWinCards = new List<CardData>();

    public Level Level => _levels[_currentLevelIndex];

    public void ResetLevelIndex()
    {
        _currentLevelIndex = 0;
        _choosedWinCards.Clear();
    } 

    public void ChangeLevelIndexOnNext()
    {
        _currentLevelIndex++;
    } 

    public List<CardData> GenerateLevelData()
    {
        List<CardData> randomCardsData = ChooseRandomCardsData();
        int winCardIndex = Random.Range(0, _levels[_currentLevelIndex].CardCount);
        _choosedWinCards.Add(randomCardsData[winCardIndex]);

        _levelHandler.GiveWinCardIdentifier(randomCardsData[winCardIndex].Identifier);
        return randomCardsData;
    }

    private List<CardData> ChooseRandomCardsData()
    {
        List<CardData> cardsData = new List<CardData>();
        foreach(CardData cardData in _cardDataKeeper.CardsData)
            cardsData.Add(cardData);

        foreach(CardData choosedWinCard in _choosedWinCards)
            cardsData.Remove(choosedWinCard);

        List<CardData> randomCardsData = new List<CardData>();
        for(int i = 0; i < _levels[_currentLevelIndex].CardCount; i++)
        {
            int index = Random.Range(0, cardsData.Count);
            randomCardsData.Add(cardsData[index]);
            cardsData.Remove(cardsData[index]);
        }
        
        return randomCardsData;
    }
}

[System.Serializable]
public class Level
{
    [SerializeField] private int _columns;
    [SerializeField] private int _lines;

    public int Columns => _columns;
    
    public int Lines => _lines;
    
    public int CardCount => _columns * _lines;
}