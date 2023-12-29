using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomizer : MonoBehaviour
{
    [SerializeField] GameObject[] itemsToRandomize;

    private void Awake()
    {
        triggerItemSpawn();
    }

    public void triggerItemSpawn()
    {
        var randomizer = Random.Range(0, itemsToRandomize.Length);
        Instantiate(itemsToRandomize[randomizer], transform.position, transform.localRotation, transform);
    }
}
