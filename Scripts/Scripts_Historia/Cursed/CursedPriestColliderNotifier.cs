using UnityEngine;

public class CursedPriestColliderNotifier : MonoBehaviour
{
    public delegate void CollisionHandler(GameObject character);
    public event CollisionHandler OnCharacterCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject character = other.gameObject;
            OnCharacterCollision?.Invoke(character);
        }
    }
}