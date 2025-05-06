using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Referances")]

    [SerializeField] private EggCounterUI _eggCounterUI;


    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;

    private int _currengEggCount;

    private void Awake()
    {
        Instance = this;
    }


    public void OnEggCollect()
    {
        _currengEggCount++;
        _eggCounterUI.SetEggCounterText(_currengEggCount, _maxEggCount);

        if (_currengEggCount == _maxEggCount)
        {
            Debug.Log("Game Win!");
            _eggCounterUI.SetEggCompleted();
        }

        Debug.Log("Egg Count " + _currengEggCount);
    }

}
