using System.Collections.Generic;
using UnityEngine;

public class SwordHitboxBehaviour : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    private bool canDamage;
    public bool CanDamage { get { return canDamage; } set { canDamage = value; } }


    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();

    private void OnEnable()
    {
        hitEnemies.Clear(); // reset every swing
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("SwordHitboxMiddleBehaviour OnTriggerStay triggerd");
        if (other.CompareTag("Enemy") && !hitEnemies.Contains(other.gameObject))
        {
            hitEnemies.Add(other.gameObject);

            HasHealth health = other.GetComponent<HasHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit");
    }
}
