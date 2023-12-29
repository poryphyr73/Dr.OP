using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    [SerializeField] private float parallaxEffect;

    void Start()
    {
        length = GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        startPos = transform.position.y;
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(0f, 1 * parallaxEffect * Time.deltaTime, 0f);
        if (transform.position.y >= startPos + length) transform.position = new Vector3(transform.position.x, startPos, transform.position.z);
    }
}
