using System;
using System.Collections;
using System.Transactions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    [Header("Referecnes")]

    [SerializeField] private PlayerController _playerController;

    [SerializeField] private RectTransform _playerWalkingTransform;

    [SerializeField] private RectTransform _playerSlideTransform;

    [SerializeField] private RectTransform _boosterSpeedTransform;
    [SerializeField] private RectTransform _boosterJumpTransform;

    [SerializeField] private RectTransform _boosterSlowTransform;

    [Header("Images")]

    [SerializeField] private Image _goldBoosterWheatImages;
    [SerializeField] private Image _holyBoosterWheatImages;
    [SerializeField] private Image _rottenBoosterWheatImages;



    [Header("Sprites")]

    [SerializeField] private Sprite _PlayerWalkingActiveSprite;

    [SerializeField] private Sprite _PlayerWalkingPassiveSprite;

    [SerializeField] private Sprite _PlayerSlidingActiveSprite;

    [SerializeField] private Sprite _PlayerSlidingPassiveSprite;

    [Header("Settings")]

    [SerializeField] private float _moveDuration;
    [SerializeField] private Ease _moveEase;

    public RectTransform GetBoosterSpeedTransform => _boosterSpeedTransform;
    public RectTransform GetBoosterJumpTransform => _boosterJumpTransform;
    public RectTransform GetBoosterSlowTransform => _boosterSlowTransform;

    public Image GetGoldBoosterWheatImage => _goldBoosterWheatImages;
    public Image GetHolyBoosterWheatImage => _holyBoosterWheatImages;
    public Image GetBoosterSlowImage => _rottenBoosterWheatImages;

    private Image _playerWalkingImage;
    private Image _playerSlideImage;

    private float _activeXPos = -120f;
    private float _passiveXPos = -90f;


    private void Awake()
    {

        _playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();
        _playerSlideImage = _playerSlideTransform.GetComponent<Image>();


    }


    private void Start()
    {
        _playerController.OnPlayerStateChanged += PlayerController_OnPlayerStateChanged;

        SetStateUserInterface(_PlayerWalkingActiveSprite, _PlayerSlidingPassiveSprite, _playerWalkingTransform, _playerSlideTransform);

    }

    private void PlayerController_OnPlayerStateChanged(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                SetStateUserInterface(_PlayerWalkingActiveSprite, _PlayerSlidingPassiveSprite, _playerWalkingTransform, _playerSlideTransform);
                //ÜSTEKİ KART AÇILACAK

                break;
            case PlayerState.SlideIdle:
            case PlayerState.Slide:
                SetStateUserInterface(_PlayerWalkingPassiveSprite, _PlayerSlidingActiveSprite, _playerSlideTransform, _playerWalkingTransform);

                //ALTTAKİ KART AÇILACAK

                break;

        }
    }

    private void SetStateUserInterface(Sprite playerWalkingSprite, Sprite playerSlidingSprite, RectTransform activeTransform, RectTransform passiveTransform)
    {
        _playerWalkingImage.sprite = playerWalkingSprite;
        _playerSlideImage.sprite = playerSlidingSprite;

        activeTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);
        passiveTransform.DOAnchorPosX(-90f, _moveDuration).SetEase(_moveEase);



    }


    private IEnumerator SetBoosterUserInterfaces(RectTransform activeTransform, Image boosterImage, Image wheatImage, Sprite activeSprite, Sprite passiveSprite,
     Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(_activeXPos, _moveDuration).SetEase(_moveEase);

        yield return new WaitForSeconds(duration);

        boosterImage.sprite = passiveSprite;
        wheatImage.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(_passiveXPos, _moveDuration).SetEase(_moveEase);

    }


    public void PlayerBoosterUIAnimations(RectTransform activeTransform, Image boosterImage, Image wheatImage, Sprite activeSprite, Sprite passiveSprite,
     Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        StartCoroutine(SetBoosterUserInterfaces(activeTransform, boosterImage, wheatImage, activeSprite, passiveSprite,
        activeWheatSprite, passiveWheatSprite, duration));
    }
}
