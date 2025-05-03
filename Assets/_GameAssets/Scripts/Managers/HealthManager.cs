using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int _maxhealth = 3;

    private int _currenthealth;

    private void Start()
{
 _currenthealth = _maxhealth;   
}

private void Damage(int damage)
{
    if (_currenthealth > 0)
    {
        _currenthealth -= damage;
        //anımate damage

        if (_currenthealth < 0)
        {
            //player dead
        }
    }
}





}

