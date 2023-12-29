using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private float rotVal, spinDir;
    private int spinDirVal;
    [SerializeField] private bool isDebris;

    private void Awake()
    {
        var animState = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[animState];
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 2.942997f);
        spinDirVal = Random.Range(-1, 1);
        spinDir = 1 * Mathf.Sign(spinDirVal);
        Debug.Log(spinDir);
        Destroy(this.gameObject, 30);
    }

    private void FixedUpdate()
    {
        rotVal += 20 * Time.deltaTime * spinDir;
        
        transform.rotation = Quaternion.Euler(0f, 0f, rotVal);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDebris)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //explode
                Destroy(this.gameObject);
            }
        }
    }
}
