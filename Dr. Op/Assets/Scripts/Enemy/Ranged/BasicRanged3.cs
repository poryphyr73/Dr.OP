using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRanged3 : MonoBehaviour
{
    public float fireRate = 1f;
    private float nextFireTime, nextFireTime1, zRot;
    public GameObject bullet;
    public GameObject[] bulletParents, parentAxes;
    private Transform player;
    [SerializeField] private SpriteRenderer[] armSprite;
    [SerializeField] private float speed, movementRange;
    //public GameObject deathEffect;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > movementRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        var differenceL = player.transform.position - parentAxes[0].transform.position;
        var differenceR = player.transform.position - parentAxes[1].transform.position;

        var rotZr = Mathf.Atan2(differenceR.y, differenceR.x) * Mathf.Rad2Deg;
        var rotZl = Mathf.Atan2(differenceL.y, differenceL.x) * Mathf.Rad2Deg;

        parentAxes[0].transform.rotation = Quaternion.Euler(0f, 0f, rotZl + 90);
        parentAxes[1].transform.rotation = Quaternion.Euler(0f, 0f, rotZr + 90);
        bulletParents[0].transform.rotation = Quaternion.Euler(0f, 0f, rotZl);
        bulletParents[1].transform.rotation = Quaternion.Euler(0f, 0f, rotZr);

        if (parentAxes[0].transform.localEulerAngles.z > 180 && parentAxes[0].transform.localEulerAngles.z < 360) armSprite[0].sortingOrder = -1;
        else armSprite[0].sortingOrder = 1;
        if (parentAxes[1].transform.localEulerAngles.z < 180 & parentAxes[1].transform.localEulerAngles.z > 0) armSprite[1].sortingOrder = -1;
        else armSprite[1].sortingOrder = 1;

        if (nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParents[0].transform.position, bulletParents[0].transform.rotation);
            nextFireTime = Time.time + fireRate;
        }        
        if (nextFireTime1 < Time.time)
        {
            Instantiate(bullet, bulletParents[1].transform.position, bulletParents[1].transform.rotation);
            nextFireTime1 = Time.time + fireRate + 0.1f;
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
