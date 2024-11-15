using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;  
    private int currentHealth;   
    public Slider healthSlider;   

    void Start()
    {
        currentHealth = maxHealth;

       
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        healthSlider.value = currentHealth;

        Debug.Log("Current Health: " + currentHealth);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DamageObject"))
        {
            TakeDamage(20);  
        }
    }
}
