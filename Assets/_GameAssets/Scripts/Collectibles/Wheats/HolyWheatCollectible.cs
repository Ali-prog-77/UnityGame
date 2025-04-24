using UnityEngine;
using UnityEngine.UI;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesingSO _wheatDesingSO;
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private PlayerStatUI _playerStateUI;


    private RectTransform _playerBoosterTransform;

    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterJumpTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }



    public void Collect()
    {
        _playerController.SetJumpForce(_wheatDesingSO.IncreaseDecreaseMultiplier, _wheatDesingSO.ResetBoostDuration);

         _playerStateUI.PlayerBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage, _playerStateUI.GetHolyBoosterWheatImage, _wheatDesingSO.Activesprite,
      _wheatDesingSO.Passivesprite, _wheatDesingSO.PassiveWheatSprite, _wheatDesingSO.PassiveWheatSprite, _wheatDesingSO.ResetBoostDuration);

      Destroy(gameObject);
    }
}
