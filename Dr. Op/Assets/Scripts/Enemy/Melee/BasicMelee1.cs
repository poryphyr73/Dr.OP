using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMelee1 : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float rotMod, zRotCap;
    private float zRot;

    //public GameObject deathEffect;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(2, 2, 1);
            if (zRot > -zRotCap) zRot -= rotMod * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, zRot);
        }
        if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(-2, 2, 1);
            if (zRot < zRotCap) zRot += rotMod * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, zRot);
        }
    }
}
