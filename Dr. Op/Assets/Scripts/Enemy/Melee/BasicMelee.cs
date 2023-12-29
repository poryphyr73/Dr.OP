using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMelee : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private float speed;

    //public GameObject deathEffect;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        render = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
        if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
    }
}
