using UnityEngine;

[CreateAssetMenu(fileName ="WheatDesingSo",menuName = "ScriptableObjects/wheatDesingSO")]
public class WheatDesingSO : ScriptableObject
{
   [SerializeField] public float _increaseDecreaseMultiplier;

   [SerializeField] public float _resetBoostDuration;

   [SerializeField] public Sprite _activeSprite;
   [SerializeField] public Sprite _passiveSprite;

   [SerializeField] public Sprite _activeWheatSprite;
   [SerializeField] public Sprite _passiveWheatSprite;


   public float ResetBoostDuration => _resetBoostDuration;

   public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;

   public Sprite Activesprite => _activeSprite;
   public Sprite Passivesprite => _passiveSprite;
   public Sprite ActiveWheatSprite => _activeWheatSprite;
   public Sprite PassiveWheatSprite => _passiveWheatSprite;
}
