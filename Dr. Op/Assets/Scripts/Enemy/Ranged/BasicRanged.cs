using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRanged : MonoBehaviour
{
    public float fireRate = 1f;
    private float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;
    [SerializeField] private Transform bulletPointAxis, headRotateAxis;
    private SpriteRenderer render;
    public float movementRange;
    [SerializeField] private float speed;
    //public GameObject deathEffect;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > movementRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }



        Vector3 difference = player.position - transform.position;

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        bulletParent.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, bulletParent.transform.rotation);
            nextFireTime = Time.time + fireRate;


        }
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(-2, 2, 1);

            var differenceHead = headRotateAxis.position - player.transform.position;

            var rotZr = Mathf.Atan2(differenceHead.y, differenceHead.x) * Mathf.Rad2Deg;
            headRotateAxis.rotation = Quaternion.Euler(headRotateAxis.rotation.x, headRotateAxis.rotation.y, rotZr + 180);
        }
        if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(2, 2, 1);

            var differenceHead = headRotateAxis.position - player.transform.position;

            var rotZr = Mathf.Atan2(differenceHead.y, differenceHead.x) * Mathf.Rad2Deg;
            headRotateAxis.localRotation = Quaternion.Euler(headRotateAxis.localRotation.x, headRotateAxis.localRotation.y, rotZr);
        }
    }
}
