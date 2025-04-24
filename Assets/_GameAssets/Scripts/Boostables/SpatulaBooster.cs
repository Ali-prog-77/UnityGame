using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBostables
{
    [Header("Referances")] 
    [SerializeField] private Animator _spatulaAnimator;

    [Header("Settings")] 

    [SerializeField] private float _jumpForce;

    private bool _isActivated;
    public void Boost(PlayerController playerController)
    {
        if (_isActivated)
        {
            return;
        }
        Rigidbody playerRigidbody = playerController._playerRigidbody;

        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f,playerRigidbody.linearVelocity.z);
        playerRigidbody.AddForce(transform.forward * _jumpForce,ForceMode.Impulse);

        PlayBoostAnimatoin();
        _isActivated=true;
        Invoke(nameof(ResetActivation),0.2f);
    }

    private void PlayBoostAnimatoin()
    {
        _spatulaAnimator.SetTrigger(Consts.OtherAnimations.IS_SPATULA_JUMPİNG);
    }

    private void ResetActivation()
    {
        _isActivated = false;
    }
}
