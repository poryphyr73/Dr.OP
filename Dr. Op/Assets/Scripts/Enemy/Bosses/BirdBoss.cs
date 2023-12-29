using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBoss : MonoBehaviour
{
    public float fireRate = 1f;
    public GameObject bullet, laser;
    public GameObject bulletParent;
    private Transform player;
    private SpriteRenderer render;
    [SerializeField] private float speed;

    [SerializeField] private GameObject[] pointsToMove;

    private bool hasRandomized;
    private int randomVal;
    [SerializeField] private int toShoot = 3;
    //public GameObject deathEffect;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        render = GetComponent<SpriteRenderer>();
        Debug.Log(transform.localEulerAngles.z);
    }

    void FixedUpdate()
    {

    }
}
