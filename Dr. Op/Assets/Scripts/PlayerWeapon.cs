using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject[] projectile;
    public Transform shotPos;
    public Transform rotator;
    public GameObject gunSprite;
    //public AudioSource shot;

    private float timeBtwShots;
    public float startTimeBtwShots;

    [SerializeField] Animator barrelAnim, modAnim, modStateAnim;
    private SpriteRenderer barrelSprite;
    public float barrelState, modState;
    private bool modStateState;
    private bool hasFired, firingRecoil;
    private float rofMod;

    private void Awake()
    {
        barrelSprite = barrelAnim.gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        UpdateAnims();

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rotator.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        rotator.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (timeBtwShots <= 0)
        {
            if (Input.GetAxisRaw("Fire1")==1)
            {
                barrelAnim.SetTrigger("Firing");
                hasFired = true;
            }

            if (barrelSprite.sprite.name.Contains("FIRED") && hasFired)
            {
                Instantiate(projectile[((int)barrelState)], shotPos.position, rotator.transform.rotation);
                timeBtwShots = startTimeBtwShots * rofMod;
                barrelAnim.ResetTrigger("Firing");
                //shot.Play();
                Vector3 diff = gunSprite.transform.position - transform.position;

                firingRecoil = true;
                hasFired = false;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (firingRecoil)
        {
            if (gunSprite.transform.localPosition.x > 1.75f)
            {
                gunSprite.transform.localPosition -= new Vector3(5f*Time.deltaTime, 0f, 0f);
            }
            else
            {
                gunSprite.transform.localPosition = new Vector3(2f, gunSprite.transform.localPosition.y, gunSprite.transform.localPosition.z);
                firingRecoil = false;
            }
        }
    }

    private void UpdateAnims()
    {
        barrelAnim.SetFloat("whichBarrel", barrelState);
        modAnim.SetFloat("modState", modState);
        modStateAnim.SetBool("hadMod", modStateState);

        switch (barrelState)
        {

            case 1:
                rofMod = 0.75f;
                break;
            case 2:
                rofMod = 1.25f;
                break;
            default:
                rofMod = 1;
                break;
        }
    }
}
