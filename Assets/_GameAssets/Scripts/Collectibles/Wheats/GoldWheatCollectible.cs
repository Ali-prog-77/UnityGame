using UnityEngine.UI;
using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour, ICollectible
{

   [SerializeField] private WheatDesingSO _wheatDesingSO;
   [SerializeField] private PlayerController _playerController;

   [SerializeField] private PlayerStatUI _playerStateUI;

   private RectTransform _playerBoosterTransform;

   private Image _playerBoosterImage;

   private void Awake()
   {
      _playerBoosterTransform = _playerStateUI.GetBoosterSpeedTransform;
      _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
   }


   public void Collect()
   {
      _playerController.SetMovementSpeed(_wheatDesingSO.IncreaseDecreaseMultiplier, _wheatDesingSO.ResetBoostDuration);

      _playerStateUI.PlayerBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage, _playerStateUI.GetGoldBoosterWheatImage, _wheatDesingSO.Activesprite,
      _wheatDesingSO.Passivesprite, _wheatDesingSO.PassiveWheatSprite, _wheatDesingSO.PassiveWheatSprite, _wheatDesingSO.ResetBoostDuration);

      Destroy(gameObject);
   }
}
