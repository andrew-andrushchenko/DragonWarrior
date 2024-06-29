using System.Collections;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private bool triggered; // When the trap gets triggered
    private bool active; // When the trap is active and can hurt the player

    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerHealth != null && active)
        {
            playerHealth.TakeDamage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth = collision.GetComponent<Health>();
            if (!triggered)
            {
                StartCoroutine(ActivateFiretrap());
            }

            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth = null;
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        // Turn the sprite red to notify the player and trigger the trap
        triggered = true;
        spriteRenderer.color = Color.red;

        // Wait for delay, activate trap, turn on animation
        yield return new WaitForSeconds(activationDelay);
        spriteRenderer.color = Color.white;
        active = true;

        anim.SetBool("activated", true);

        // Wait for activeTime seconds, deactivate trap
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
