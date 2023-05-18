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
    [SerializeField] private AudioSource _sleepingSound;
    [SerializeField] private GameObject _sleepingTextPrefab;
    [SerializeField] private Transform _textParent;
    [SerializeField] private float _floatingSpeed = 1f;
    [SerializeField] private float _floatingHeight = 1f;
    [SerializeField] private float _floatingDuration = 4f;
    private GameObject _sleepingTextObject;
    private Coroutine _floatingCoroutine;

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
        _textParent = transform.Find("TextParent");
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
            _sleepingSound.Play();
            _sleepingTextObject = Instantiate(_sleepingTextPrefab, _textParent);
            _sleepingTextObject.transform.localPosition = Vector3.zero;

            _floatingCoroutine = StartCoroutine(FloatingTextRoutine());
        }
    }

    private void onGettingHit()
    {
        _sleepingSound.Stop();
        _angryMeowSound.Play();
        _currentPhase = 0;
        if (_character == 0) // Chef X
        {
            if (_sleepingTextObject != null)
            {
                if (_floatingCoroutine != null)
                {
                    StopCoroutine(_floatingCoroutine);
                }
                Destroy(_sleepingTextObject);
            }
            
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

        private IEnumerator FloatingTextRoutine()
    {
        while (true)
        {
            float elapsedTime = 0f;
            Vector3 initialPosition = _sleepingTextObject.transform.localPosition;

            while (elapsedTime < _floatingDuration)
            {
                float normalizedTime = elapsedTime / _floatingDuration;
                float yOffset = Mathf.Sin(normalizedTime * Mathf.PI) * _floatingHeight;
                Vector3 targetPosition = initialPosition + new Vector3(0f, yOffset, 0f);

                _sleepingTextObject.transform.localPosition = targetPosition;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Wait for a short delay before starting the next loop
            yield return new WaitForSeconds(0.5f);
        }
    }
}
