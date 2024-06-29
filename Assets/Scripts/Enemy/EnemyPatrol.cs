using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    
    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initialScale;
    private bool movingLeft;

    [Header("Enemy animator")]
    [SerializeField] private Animator anim;

    [Header("Idle behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    private void Awake()
    {
        initialScale = enemy.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                ChangeDirection();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                ChangeDirection();
            }
        }
    }

    private void ChangeDirection()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * direction, initialScale.y, initialScale.z); 
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }
}
