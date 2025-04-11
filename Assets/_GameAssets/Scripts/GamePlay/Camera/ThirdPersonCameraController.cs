using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransfrom;
    [SerializeField] private Transform _orientationTransform;

    [SerializeField] private Transform _playerVisualTransfrom;

    [Header("Settings")]

    [SerializeField] private float _rotationSpeed;


    private void Update()
    {
        Vector3 viewDirection = _playerVisualTransfrom.position - new Vector3(transform.position.x, _playerTransfrom.position.y, transform.position.z);

        _orientationTransform.forward = viewDirection.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = _orientationTransform.forward * verticalInput + _orientationTransform.right * horizontalInput;

        if (inputDirection != Vector3.zero)
        {
            _playerVisualTransfrom.forward = Vector3.Slerp(_playerVisualTransfrom.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed);
        }

    }
}
