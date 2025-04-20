using UnityEngine;

[CreateAssetMenu(fileName ="WheatDesingSo",menuName = "ScriptableObjects/wheatDesingSO")]
public class WheatDesingSO : ScriptableObject
{
   [SerializeField] public float _increaseDecreaseMultiplier;

   [SerializeField] public float _resetBoostDuration;


   public float ResetBoostDuration => _resetBoostDuration;

   public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;
}
