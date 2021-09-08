using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    //taking damage from the Gun scripts and destroy the game objects after finishing the health
    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("health has decresed" + health);
        if (health <= 0f)
        {
            Die();
        }
    }
    
    //destroy the target 
    void Die()
    {
        Destroy(gameObject);
    }


}
