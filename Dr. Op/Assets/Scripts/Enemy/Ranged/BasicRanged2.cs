using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRanged2 : MonoBehaviour
{
    public float fireRate = 1f;
    private float nextFireTime, zRot;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;
    [SerializeField] private float speed;
    //public GameObject deathEffect;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, bulletParent.transform.rotation);
            nextFireTime = Time.time + fireRate;
        }

        if (transform.position.x < player.position.x)
        {
            if (zRot > -5) zRot -= 10f * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, zRot);
        }
        if (transform.position.x > player.position.x)
        {
            if (zRot < 5) zRot += 10f * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, zRot);
        }
    }
}
