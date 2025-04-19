using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private float _ıncreasesForce;

    [SerializeField] private float _resetBoostDuration;

    public void Collect()
    {
        _playerController.SetJumpForce(_ıncreasesForce, _resetBoostDuration);
        Destroy(gameObject);
    }
}
