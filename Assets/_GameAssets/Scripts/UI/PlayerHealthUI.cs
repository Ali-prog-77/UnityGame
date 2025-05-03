using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Referances")]

    [SerializeField] private Image[] _playerHealthImages;

    [Header("Sprites")]

    [SerializeField] private Sprite _playerHealthySprite;

    [SerializeField] private Sprite _playerUnHealthySprite;


    [Header("Settings")]

    [SerializeField] private float _scaleDuration;

    private RectTransform[] _playerHealthTransforms;

    private void Awake()
    {
        _playerHealthTransforms = new RectTransform[_playerHealthImages.Length];
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            _playerHealthTransforms[i] = _playerHealthImages[i].gameObject.GetComponent<RectTransform>();
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            AnimateDamage();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            AnimateDamageAll();
        }

    }
    public void AnimateDamage()
    {
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            if (_playerHealthImages[i].sprite == _playerHealthySprite)
            {
                AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransforms[i]);
                break;
            }
        }
    }
    public void AnimateDamageAll()
    {
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransforms[i]);

        }
    }



    public void AnimateDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = _playerUnHealthySprite;

            activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }


}
