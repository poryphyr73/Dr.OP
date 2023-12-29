using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRanged1 : MonoBehaviour
{
    public float fireRate = 1f;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;
    private SpriteRenderer render;
    [SerializeField] private float speed;
    private bool shot;
    private float zRot;
    [SerializeField] private float distanceToKeep;
    //public GameObject deathEffect;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        render = GetComponent<SpriteRenderer>();
        Debug.Log(transform.localEulerAngles.z);
    }

    void FixedUpdate()
    {
        var difference = Vector2.Distance(player.transform.position, transform.position);
        if(difference >= distanceToKeep + 0.5f) transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        else if(difference <= distanceToKeep - 0.5f) transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);

        if (render.sprite.name == "Printing_6" && !shot)
        {
            Instantiate(bullet, bulletParent.transform.position, bulletParent.transform.rotation);
            shot = true;
        }
        else if (render.sprite.name == "Printer_0") shot = false;
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
