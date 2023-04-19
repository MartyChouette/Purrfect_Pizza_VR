using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSpawner : MonoBehaviour
{
    public Pizza[] pizzas;
    public float spawnInterval = 5f;

    private void Start()
    {
        StartCoroutine(SpawnPizzas());
    }

    private IEnumerator SpawnPizzas()
    {
        while (true)
        {
            Pizza randomPizza = pizzas[Random.Range(0, pizzas.Length)];
            GameObject pizzaObject = Instantiate(randomPizza.model, transform.position, Quaternion.identity);
            PizzaController pizzaController = pizzaObject.GetComponent<PizzaController>();
            pizzaController.pizza = randomPizza;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
