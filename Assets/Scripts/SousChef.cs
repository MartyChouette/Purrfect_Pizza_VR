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
    private Slider _progressBarSlider;
    private Character.Characters _character;
    private float _completionTime;
    private int _orderIndex;
    private bool _isMakingPizza;

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
    }

    private void Start()
    {
        _character = _characterSO.character;
        _completionTime = _characterSO.completionTime;
        _orderIndex = 0;
        _isMakingPizza = false;
        OrderManager.Instance.orderList(_character)[_orderIndex].instantiatePizza(_pizzaSpawnPoint.transform);

    }

    private void Update()
    {
        onPizzaCreate();
    }

    private void onPizzaCreate()
    {
        if (!_isMakingPizza)
        {
            _isMakingPizza = true;
            Timer.Create(onPizzaComplete, _completionTime, _progressBarSlider);
        }
    }

    private void onPizzaComplete()
    {
        OrderManager.Instance.orderList(_character)[_orderIndex].instantiatePizza(_pizzaSpawnPoint.transform);
        _meowSounds[Random.Range(0, _meowSounds.Length)].Play();
        _isMakingPizza = false;
        _orderIndex++;
        if (_orderIndex >= OrderManager.Instance.orderList(_character).Count)
        {
            _orderIndex = 0;
        }
    }
}
