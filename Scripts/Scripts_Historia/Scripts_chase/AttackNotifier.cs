using UnityEngine;

public class AttackNotifier : MonoBehaviour
{
    public delegate void AttackHandler(GameObject character);
    public event AttackHandler OnPlayerEnterAttackRange;
    public event AttackHandler OnPlayerExitAttackRange;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {        
            OnPlayerEnterAttackRange?.Invoke(other.gameObject);

            PlayerCollision playerCollision = other.GetComponent<PlayerCollision>();
            if (playerCollision != null)
            {
                playerCollision.TakeDamage();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {       
            OnPlayerExitAttackRange?.Invoke(other.gameObject);
        }
    }
}
