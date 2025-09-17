using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCircle : MonoBehaviour
{
    [SerializeField] private Image _circleImage;
    [SerializeField] private float _maxFillAmount;

    private float _currentFill;
    
    public async UniTask RunImmitation(float immitationTime)
    {
        _circleImage.fillAmount = 0;
        
        await _circleImage.DOFillAmount(_maxFillAmount, immitationTime).AsyncWaitForCompletion();
        
        Debug.Log("Имитация закончилась");
    }
}
