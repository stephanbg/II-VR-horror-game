using UnityEngine;

public class EnemyRandomMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotationSpeed = 2f;   
    public float obstacleCheckDistance = 1f;
    public float directionChangeInterval = 5f;
    
    public GameObject zoneObject;  // Zona que define el límite
    public float margin = 0.5f;  // Margen de zona donde no se permite moverse
    public float expandedZoneMargin = 1f;  // Margen adicional para expandir la zona
    
    public Transform playerTransform;  // El Transform del personaje a asignar desde el inspector

    private Rigidbody rb;
    private Vector3 moveDirection;
    private float directionChangeTimer;
    private bool isRotating = false;
    private Animator enemyAnimator;

    private SphereCollider zoneCollider;
    private Vector3 zoneCenter;
    private float zoneRadius;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyAnimator = gameObject.GetComponentInChildren<Animator>();
        zoneCollider = zoneObject.GetComponent<SphereCollider>();
        zoneCenter = zoneCollider.transform.position;
        zoneRadius = zoneCollider.radius;
        SetRandomDirection();
        directionChangeTimer = directionChangeInterval;
    }

    private void FixedUpdate()
    {
        // Verificar si el jugador está dentro de la zona, si lo está, no realizamos el movimiento aleatorio
        if (IsInsideZone(playerTransform.position))
        {
            // Si el jugador está dentro de la zona, no realizamos nada o hacemos solo un pequeño movimiento para evitar quedar estáticos
            return;  // Salir de la función si estamos dentro de la zona
        }

        // Si estamos fuera de la zona, realizamos el movimiento aleatorio
        enemyAnimator.ResetTrigger("Idle");
        enemyAnimator.ResetTrigger("Punch");
        enemyAnimator.ResetTrigger("Run");
        enemyAnimator.SetTrigger("Walk");

        directionChangeTimer -= Time.deltaTime;
        if (directionChangeTimer <= 0f || IsObstacleAhead()) 
        {
            SetRandomDirection();
            directionChangeTimer = directionChangeInterval;
        }

        if (isRotating)
        {
            RotateTowardsTarget();
        }
        MoveEnemy();
    }

    private bool IsObstacleAhead()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, transform.forward);
        return Physics.Raycast(ray, obstacleCheckDistance);
    }

    private void SetRandomDirection()
    {
        // Movimiento aleatorio solo cuando el jugador está fuera de la zona expandida
        if (!IsInsideExpandedZone(transform.position))
        {
            moveDirection = (zoneCenter - transform.position).normalized;
        }
        else
        {
            float randomAngle = Random.Range(0f, 360f);
            Quaternion randomRotation = Quaternion.Euler(0, randomAngle, 0);
            moveDirection = randomRotation * Vector3.forward;

            // Asegurarnos de no salirse de la zona
            if (!IsInsideExpandedZone(transform.position + moveDirection))
            {
                moveDirection = AdjustDirectionToStayInsideExpandedZone(transform.position, moveDirection);
            }
        }
        isRotating = true;
    }

    private bool IsInsideZone(Vector3 position)
    {
        // Verificar si el jugador está dentro de la zona
        float distanceToCenter = Vector3.Distance(position, zoneCenter);
        return distanceToCenter < zoneRadius;
    }

    private bool IsInsideExpandedZone(Vector3 position)
    {
        // Verificar si el enemigo está dentro de la zona expandida
        float distanceToCenter = Vector3.Distance(position, zoneCenter);
        return distanceToCenter < (zoneRadius + expandedZoneMargin - margin);
    }

    private Vector3 AdjustDirectionToStayInsideExpandedZone(Vector3 position, Vector3 direction)
    {
        Vector3 newPosition = position + direction;
        float distanceToCenter = Vector3.Distance(newPosition, zoneCenter);
        if (distanceToCenter > (zoneRadius + expandedZoneMargin - margin))
        {
            Vector3 directionToCenter = (zoneCenter - newPosition).normalized;
            return directionToCenter * 0.8f + direction * 0.2f;
        }
        return direction;
    }

    private void RotateTowardsTarget()
    {
        Vector3 targetDirection = moveDirection;
        float step = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(rb.transform.forward, targetDirection, step, 0f);
        rb.MoveRotation(Quaternion.LookRotation(newDirection));
        if (Vector3.Angle(rb.transform.forward, moveDirection) < 5f)
        {
            isRotating = false;
        }
    }

    private void MoveEnemy()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }
}
