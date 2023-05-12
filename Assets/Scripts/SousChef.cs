using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SousChef : MonoBehaviour
{
    [SerializeField] private Character _characterSO;
    [SerializeField] private GameObject _pizzaSpawnPoint;
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private AudioSource[] _meowSounds;
    [SerializeField] private AudioSource _angryMeowSound;
    private Slider _progressBarSlider;
    private Image _progressBarFill;
    private Character.Characters _character;
    private float _completionTime;
    private int _orderIndex;
    private bool _isMakingPizza;
    private bool _isChefAwake;
    private bool _isCharacteristicUpdatable;
    private int _currentPhase;
    private Timer _timerInstance;
    private GameObject _objectCollided;

    private void OnValidate() 
    {
        if (_characterSO == null)
        {
            Debug.Log("Sous Chef is missing a Character scriptable object.", this.gameObject);
        }
        if (_pizzaSpawnPoint == null)
        {
            Debug.Log("Sous Chef is missing a pizza spawn point prefab.", this.gameObject);
        }
        if (_progressBar == null)
        {
            Debug.Log("Sous Chef is missing a progress bar prefab.", this.gameObject);
        }
    }

    private void Awake()
    {
        _progressBarSlider = _progressBar.GetComponentInChildren<Slider>();
        _progressBarSlider.gameObject.SetActive(false);

        _progressBarFill = _progressBar.transform.Find("Slider/Fill Area/Fill").gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        _character = _characterSO.character;
        _completionTime = _characterSO.completionTime;
        _orderIndex = 0;
        _isMakingPizza = false;
        _isChefAwake = true;
        _isCharacteristicUpdatable = true;
        _currentPhase = 0;
        _objectCollided = null;
        createPizzaAtStart();
    }

    private void Update()
    {
        onPizzaCreate();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pizza") | other.gameObject.CompareTag("Ingredient"))
        {
            if (_objectCollided == null | other.gameObject != _objectCollided)
            {
                _objectCollided = other.gameObject;
                onGettingHit();
            }
        }
    }

    private void onPizzaCreate()
    {
        if (!_isMakingPizza & _isChefAwake)
        {
            _isMakingPizza = true;
            _timerInstance = Timer.Create(onPizzaComplete, _completionTime, _progressBarSlider);
        }
    }

    private void createPizzaAtStart()
    {
        OrderManager.Instance.orderList(_character)[_orderIndex].instantiatePizza(_pizzaSpawnPoint.transform);
        _orderIndex++;
    }

    private void onPizzaComplete()
    {
        OrderManager.Instance.orderList(_character)[_orderIndex].instantiatePizza(_pizzaSpawnPoint.transform); // Spawn the pizza gameobject
        _meowSounds[Random.Range(0, _meowSounds.Length)].Play(); // Play SFX
        updateChefCharateristic(); // Update the unique characteristic of each Chef
        _isMakingPizza = false; 
        _orderIndex++;
        // Cycle through the same orderList to determine the type of pizza to make whent the reaching the end of orderList
        if (_orderIndex >= OrderManager.Instance.orderList(_character).Count)
        {
            _orderIndex = 0;
        }
    }

    private void updateChefCharateristic()
    {
        if (_isCharacteristicUpdatable)
        {
            if (_character == 0) // Chef X
            {
                lazinessMeter();
            }
            else // Chef Y
            {
                // Chef Y characteristic
            }
        }
        _isCharacteristicUpdatable = true;
    }

    private void lazinessMeter()
    {
        _currentPhase++; // Change to next phase of characteristic (getting sleepier)
        if (_currentPhase < _characterSO.numberOfPhases)
        {
            _progressBarFill.color = _characterSO.progressBarColors[_currentPhase];// Change progressBar color
            _completionTime += _completionTime * _characterSO.characteristicScaler;// set completionTime = completionTime x chracteristicScaler + completionTime
        }
        else // The Chef is sleeping
        {
            _isChefAwake = false;
        }
    }

    private void onGettingHit()
    {
        _angryMeowSound.Play();
        _currentPhase = 0;
        if (_character == 0) // Chef X
        {
            _progressBarFill.color = _characterSO.progressBarColors[_currentPhase];
            _completionTime = _characterSO.completionTime;
            if (_timerInstance != null)
            {
                _timerInstance.timeLimit = _completionTime;
            }
        }
        else // Chef Y
        {
            
        }
        _isChefAwake = true;
        _isCharacteristicUpdatable = false;
    }
}
