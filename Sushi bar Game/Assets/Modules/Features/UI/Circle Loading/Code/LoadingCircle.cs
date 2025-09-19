using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCircle : MonoBehaviour
{
    [SerializeField] private Image _circleImage;

    private float _maxFillAmount = 1f;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public async UniTask RunImmitation(float immitationTime)
    {
        gameObject.SetActive(true);
        
        _circleImage.fillAmount = 0;
        
        await _circleImage.DOFillAmount(_maxFillAmount, immitationTime);
        
        gameObject.SetActive(false);
    }
}
