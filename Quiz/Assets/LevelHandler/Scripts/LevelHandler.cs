using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class LevelHandler : MonoBehaviour
{
    private string _winCardIdentifier;
    private UnityEvent _levelComplete = new UnityEvent();

    public void GiveWinCardIdentifier(string winCardIdentifier)
    {
        _winCardIdentifier = winCardIdentifier;
        Debug.Log(_winCardIdentifier);
    }
    
    public void AddListenerToCompleteLevel(UnityEngine.Events.UnityAction Listener)
    {
        _levelComplete.AddListener(Listener);
    }

    public void CheckCardIdentifier(string identifier, Transform spripteTransform)
    {
        if(identifier == _winCardIdentifier)
        {
            _levelComplete.Invoke();
            _levelComplete.RemoveAllListeners();
        }
        else
        {
            spripteTransform.DOShakePosition(0.7f, 0.05f, 10, 90, false, false).SetEase(Ease.InBounce);
        }
    }
}
