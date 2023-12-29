using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndEvents : MonoBehaviour
{
    // THIS SCRIPT IS FOR ENEMIES

    [SerializeField] private GameObject[] toSpawn;
    [SerializeField] private int MeleeDamage;
    [SerializeField] private int maxHealth;
    private int health;
    [SerializeField] private float dropRate;
    [SerializeField] private GameObject scrap;
    [SerializeField] bool spawnsOnDeath;

    private void Awake()
    {
        health = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().takeDamage(MeleeDamage);
            Vector3 difference = collision.gameObject.GetComponent<Transform>().position - transform.position;
            collision.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(difference*900);
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            takeDamage(collision.gameObject.GetComponent<PlayerBullet>().damage);
            Vector3 difference = collision.gameObject.GetComponent<Transform>().position - transform.position;
            this.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(-difference * 10000);
            Destroy(collision.gameObject);
        }
    }

    private void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //run death stuff
            var drop = Random.Range(0, 100);
            if (drop <= dropRate) Instantiate(scrap, transform.position, transform.localRotation);
            if (spawnsOnDeath) SpawnOnDeath();
            Destroy(this.gameObject);
        }
    }

    private void SpawnOnDeath()
    {
        if(toSpawn != null) foreach (GameObject obj in toSpawn) Instantiate(obj, transform.position, Quaternion.Euler(0f, 0f, 0f));
    }
}
