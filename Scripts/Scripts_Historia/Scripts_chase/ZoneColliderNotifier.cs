using UnityEngine;

public class ZoneColliderNotifier : MonoBehaviour
{
    public delegate void ZoneEventHandler(GameObject character);
    public event ZoneEventHandler OnEnterZone;
    public event ZoneEventHandler OnExitZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnEnterZone?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnExitZone?.Invoke(other.gameObject);
        }
    }
}