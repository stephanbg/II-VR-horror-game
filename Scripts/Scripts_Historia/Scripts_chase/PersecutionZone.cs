using UnityEngine;
using System.Collections;

public class PersecutionZone : MonoBehaviour
{
    public GameObject enemy;
    public AudioClip chaseClip;
    public AudioClip attackClip;
    public float chaseSpeed = 3f;
    private ZoneColliderNotifier zoneNotifier;
    private AttackNotifier attackNotifier;
    private Animator enemyAnimator;
    private Rigidbody enemyRigidbody;
    private AudioSource audioSource;
    private Coroutine currentStateCoroutine;
    private bool amIinZone = false;

    private void Awake()
    {
        zoneNotifier = GetComponent<ZoneColliderNotifier>();
        attackNotifier = enemy.GetComponent<AttackNotifier>();
        enemyAnimator = enemy.GetComponentInChildren<Animator>();
        enemyRigidbody = enemy.GetComponent<Rigidbody>();
        audioSource = enemy.AddComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (zoneNotifier != null)
        {
            zoneNotifier.OnEnterZone += StartChase;
            zoneNotifier.OnExitZone += StopChase;
        }
        if (attackNotifier != null)
        {
            attackNotifier.OnPlayerEnterAttackRange += StartAttack;
            attackNotifier.OnPlayerExitAttackRange += StopAttack;
        }
    }

    private void OnDisable()
    {
        if (zoneNotifier != null)
        {
            zoneNotifier.OnEnterZone -= StartChase;
            zoneNotifier.OnExitZone -= StopChase;
        }
        if (attackNotifier != null)
        {
            attackNotifier.OnPlayerEnterAttackRange -= StartAttack;
            attackNotifier.OnPlayerExitAttackRange -= StopAttack;
        }
    }

    private void StartChase(GameObject character)
    {
        amIinZone = true;
        ChangeState(ChaseRoutine(character));
    }

    private void StopChase(GameObject character)
    {
        amIinZone = false;
        StopAllCoroutines();
        StartCoroutine(StopChaseSoundFadeOut());  // Gradualmente detener el sonido al salir de la zona
    }

    private void StartAttack(GameObject character)
    {
        if (amIinZone) {
            ChangeState(AttackRoutine(character));            
        }
    }

    private void StopAttack(GameObject character)
    {
        if (amIinZone) {
            StartCoroutine(WaitAndChangeState(character));
        }
    }

    private IEnumerator WaitAndChangeState(GameObject character)
    {
        yield return new WaitForSeconds(1f); // Espera medio segundo
        ChangeState(ChaseRoutine(character));
    }

    private IEnumerator ChaseRoutine(GameObject character)
    {
        PlaySound(chaseClip);
        while (amIinZone)
        {
            SetChaseState();
            yield return MoveTowardsPlayer(character);
        }
    }

    private IEnumerator AttackRoutine(GameObject character)
    {
        PlaySound(attackClip);
        SetAttackState();
        yield return RotateTowardsPlayer(character);
    }

    private IEnumerator MoveTowardsPlayer(GameObject character)
    {
        Vector3 playerPosition = character.transform.position;
        Vector3 targetPosition = new Vector3(playerPosition.x, enemyRigidbody.position.y, playerPosition.z);
        Vector3 newPosition = Vector3.MoveTowards(enemyRigidbody.position, targetPosition, chaseSpeed * Time.deltaTime);
        enemyRigidbody.MovePosition(newPosition);

        Vector3 directionToPlayer = (playerPosition - enemyRigidbody.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        enemyRigidbody.MoveRotation(Quaternion.Slerp(enemyRigidbody.rotation, targetRotation, Time.deltaTime * chaseSpeed));

        yield return null;
    }

    private IEnumerator RotateTowardsPlayer(GameObject character)
    {
        Vector3 playerPosition = character.transform.position;
        Vector3 directionToPlayer = (playerPosition - enemyRigidbody.position).normalized;
        float rotationSpeed = chaseSpeed;
        while (Vector3.Angle(enemyRigidbody.transform.forward, directionToPlayer) > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            enemyRigidbody.MoveRotation(Quaternion.Slerp(enemyRigidbody.rotation, targetRotation, Time.deltaTime * rotationSpeed));   
            yield return null;
        }
    }

    private void SetIdleState()
    {
        enemyAnimator.ResetTrigger("Run");
        enemyAnimator.ResetTrigger("Punch");
        enemyAnimator.SetTrigger("Idle");
    }

    private void SetChaseState()
    {
        enemyAnimator.ResetTrigger("Idle");
        enemyAnimator.ResetTrigger("Punch");
        enemyAnimator.SetTrigger("Run");
    }

    private void SetAttackState()
    {
        enemyAnimator.ResetTrigger("Idle");
        enemyAnimator.ResetTrigger("Run");
        enemyAnimator.SetTrigger("Punch");
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private IEnumerator FadeOutSound()
    {
        float fadeDuration = 1f;  // Duración del fade-out (puedes ajustarlo)
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;  // Restaurar volumen original para futuros sonidos
    }

    private IEnumerator StopChaseSoundFadeOut()
    {
        yield return FadeOutSound();
    }

    private void ChangeState(IEnumerator newState)
    {
        if (currentStateCoroutine != null)
            StopCoroutine(currentStateCoroutine);
        currentStateCoroutine = StartCoroutine(newState);
    }
}