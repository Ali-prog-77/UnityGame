using UnityEngine;
using UnityEngine.UI;

public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
  [SerializeField] private WheatDesingSO _wheatDesingSO;
  [SerializeField] private PlayerController _playerController;

  
    [SerializeField] private PlayerStatUI _playerStateUI;


    private RectTransform _playerBoosterTransform;

    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterSlowTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }



  public void Collect()
  {
    _playerController.SetMovementSpeed(_wheatDesingSO.IncreaseDecreaseMultiplier, _wheatDesingSO.ResetBoostDuration);

    _playerStateUI.PlayerBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage, _playerStateUI.GetBoosterSlowImage, _wheatDesingSO.Activesprite,
      _wheatDesingSO.Passivesprite, _wheatDesingSO.PassiveWheatSprite, _wheatDesingSO.PassiveWheatSprite, _wheatDesingSO.ResetBoostDuration);
    Destroy(gameObject);
  }
}
