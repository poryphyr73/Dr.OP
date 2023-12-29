using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage = 1;
    public GameObject gun;

    private void Awake()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Debug.Log("DESTROY");
        Destroy(this.gameObject);
    }
}
