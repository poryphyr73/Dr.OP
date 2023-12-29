using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShop : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private void FixedUpdate()
    {
        if(target != null)
        {
            if(transform.position.y < 0)
            {
                var difference = 0 - transform.position.y;
                transform.position += new Vector3(0f, difference *9/10 * Time.deltaTime, 0f);
            }
        }
        else
        {
            var difference = 1 + transform.position.y;
            transform.position += new Vector3(0f, difference * 2f * Time.deltaTime, 0f);
        }
        if (transform.position.y >= 50f) Destroy(this.gameObject);
    }
}
