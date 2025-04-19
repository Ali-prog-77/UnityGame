using UnityEditor;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;

    private PlayerController _playerController;

    private StateController _stateController;


    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _stateController = GetComponent<StateController>();

    }

    private void Start()
    {
        _playerController.OnPlayerJumped += PlayerController_OnPlayerJumped;

    }

    private void Update()
    {
        SetPlayerAnimations();

    }

    private void PlayerController_OnPlayerJumped()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMGİNG, true);
        Invoke(nameof(ResetJumping),0.5f);

    }

    private void ResetJumping()
    {

        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMGİNG, false);
    }

    private void SetPlayerAnimations()
    {
        var currentState = _stateController.GetCurrentState();

        switch (currentState)
        {
            case PlayerState.Idle:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVİNG, false);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLİDİNG, false);

                break;

            case PlayerState.Move:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVİNG, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLİDİNG, false);

                break;

            case PlayerState.SlideIdle:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLİDİNG, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLİDİNG_ACTIVE, false);


                break;


            case PlayerState.Slide:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLİDİNG, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLİDİNG_ACTIVE, true);


                break;
        }
    }
}
