using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int health = 100;
    public int damage = 20;
    private float damageCooldown = .5f;
    private float damageTimer;
    public TextMeshProUGUI healthText;
    public static bool Dead = false;

    private void Update()
    {

        if (damageTimer > 0f)
        {
            damageTimer -= Time.deltaTime;
        }

        if (health <= 0f)
        {
            Dead = true;
            GameManager.GameActive = false;
            Destroy(gameObject);

        }
    }

    public void ChangeHPText()
    {
        healthText.SetText("Health: " + health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        ChangeHPText();
    }

}
