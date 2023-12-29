using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Positioning
    private Transform playerTransform;

    //Physics
    public float speedModifier;
    [SerializeField] private float speed;
    [SerializeField] private float speedcap;
    [SerializeField] private float decelerationMod;
    private Rigidbody2D rb;

    //Script Access
    public ScrapCounter scrapcounter;

    //Health
    public float maxHealth;
    public float health;
    public float tempHealth;
    public Image[] hearts;
    public Animator[] heartsAnims;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Image[] shields;
    public Animator[] shieldsAnims;


    public int scrapVal;

    [SerializeField] private string[] itemsPossible;

    [SerializeField] private PlayerWeapon plWeapon;

    private bool gliderIsGot, onPills;

    private float speedBonusTimer;
    [SerializeField] float maxSpeedTimer;

    [SerializeField] GameObject glider;

    private void Awake()
    {
        playerTransform = GetComponent<Transform>();
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var movementX = Input.GetAxisRaw("Horizontal");
        var movementY = Input.GetAxisRaw("Vertical");

        if (movementY < 0)
        {
            movementY = -0.6f;
        }

        if (movementX == 0 && movementY == 0)
        {
            speed = 0;
        }
        else
        {
            if (!gliderIsGot)
            {
                if (speed < speedcap)
                {
                    speed += speedcap / 20;
                }
            }
            else
            {
                speed = speedcap;
            }
        }

        runPills();

        Vector3 v = new Vector3(movementX, movementY, 0);
        playerTransform.position += v * Time.deltaTime * speed * speedModifier;

        updateHealthUI(hearts, fullHeart, emptyHeart, heartsAnims, health, maxHealth);
        updateHealthUI(shields, null, null, shieldsAnims, tempHealth, tempHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Scrap"))
        {
            Destroy(collision.gameObject);
            scrapVal++;
            scrapcounter.updateText(scrapVal);
        }
    }

    public void takeDamage(int damage)
    {
        if (tempHealth < 1) health -= damage;
        else tempHealth -= damage;
        if (health <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void findItemWith(string itemName)
    {
        for (int i = 0; i < itemsPossible.Length; i++) {
            if (itemName == itemsPossible[i]) runItems(i);
        }

    }

    private void runItems(int itemToRun)
    {
        switch (itemToRun)
        {
            case 0:
                plWeapon.barrelState = 1;
                break;
            case 1:
                plWeapon.barrelState = 2;
                break;
            case 2:
                health = maxHealth;
                Debug.Log("health is at max!");
                break;
            case 3:
                gliderIsGot = true;
                glider.SetActive(true);
                Debug.Log("got the glider");
                break;
            case 4:
                speedBonusTimer = maxSpeedTimer+Time.time;
                onPills = true;
                break;
            case 5:
                tempHealth++;
                Debug.Log("you have " + tempHealth + "shields");
                break;
            case 6:
                plWeapon.modState = 1;
                break;
            case 7:
                plWeapon.modState = 2;
                break;
            case 8:
                maxHealth++;
                health = maxHealth;
                break;
            default:
                Debug.Log("OK Buddy (Retard)");
                break;
        }
    }

    private void runPills()
    {
        if (speedBonusTimer >= Time.time && onPills) speedModifier = 1.5f;
        else if (speedBonusTimer >= Time.time - maxSpeedTimer / 2 && onPills)
        {
            speedModifier = 0.75f;
            Debug.Log("crashing");
        }
        else
        {
            speedModifier = 1;
            onPills = false;
        }
    }

    private void updateHealthUI(Image[] toUpdate, Sprite full, Sprite empty, Animator[] animator, float statToCheck, float max)
    {
        for (int i = 0; i < toUpdate.Length; i++)
        {
            if (full != null && i < statToCheck)
            {
                animator[i].ResetTrigger("TakeDamage");
                animator[i].SetTrigger("Healed");
            }
            else if (empty != null)
            {
                animator[i].ResetTrigger("Healed");
                animator[i].SetTrigger("TakeDamage");
            }

            if (i < max) toUpdate[i].gameObject.SetActive(true);
            else toUpdate[i].gameObject.SetActive(false);
        }
    }
}
