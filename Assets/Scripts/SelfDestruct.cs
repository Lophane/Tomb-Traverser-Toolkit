using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, .1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats stats = other.gameObject.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.TakeDamage(damage);
        }
    }
}
