using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public Slider slider;
    public Color Low;
    public Color High;
    public float currentHealth, maxHealth;

    public float invincibleLength;
    private float invincibleCounter;
    private SpriteRenderer theSR;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        SetHealth(currentHealth, maxHealth);
        theSR = GetComponent<SpriteRenderer>();
        //SetHealth(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;


            if (invincibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1);
            }
        }
    }
    public void SetHealth(float health, float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = health;
        //slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);
    }
    public void TakeDamage(int damage)
    {
        if (invincibleCounter <= 0)
        {
            currentHealth -= damage;

            SetHealth(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                gameObject.SetActive(false);
                //LevelManager.instance.RespawnPlayer();
                Debug.Log("GG");
            }
            else
            {
                invincibleCounter = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
                CharacterController2D.instance.KnockBack();
                //PlayerMovement.instance.HitAnim();
            }
        }
    }
}
