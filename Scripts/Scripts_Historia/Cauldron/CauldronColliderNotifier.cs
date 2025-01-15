using UnityEngine;

public class CauldronColliderNotifier : MonoBehaviour
{
    public delegate void CollisionHandler();
    public event CollisionHandler OnCharacterCollision;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCharacterCollision?.Invoke();
        }
    }
}