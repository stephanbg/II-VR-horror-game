using System.Collections;
using UnityEngine;

public class EnemyScares : MonoBehaviour
{
    public float runSpeed = 3f; 
    public float runDistance = 4f; 
    public ZoneColliderNotifier zoneNotifier; 
    public AudioClip scareClip; 
    public AudioClip growlClip; 
    public Transform player; 
    public float minGrowlDistance = 0.5f; 
    public float maxGrowlDistance = 1f; 
    public float growlFadeSpeed = 2f;
    private Animator enemyAnimator; 
    private AudioSource audioSource; 
    private bool hasRunOnce = false; 
    private Transform targetToLookAt; 
    private bool isPlayingScareClip = false; 

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        if (enemyAnimator == null)
        {
            enemyAnimator = GetComponentInChildren<Animator>();  
        }      
        audioSource = GetComponent<AudioSource>();
        enemyAnimator.SetTrigger("Idle");
    }

    private void OnEnable()
    {
        if (zoneNotifier != null)
        {
            zoneNotifier.OnEnterZone += HandleZoneEnter;
            zoneNotifier.OnExitZone += HandleZoneExit;
        }
    }

    private void OnDisable()
    {
        if (zoneNotifier != null)
        {
            zoneNotifier.OnEnterZone -= HandleZoneEnter;
            zoneNotifier.OnExitZone -= HandleZoneExit;
        }
    }

    private void HandleZoneEnter(GameObject character)
    {
        if (!hasRunOnce)
        {
            hasRunOnce = true;
            StartCoroutine(RunForward());
        }
    }

    private void HandleZoneExit(GameObject character)
    {
        targetToLookAt = character.transform;
    }

    private IEnumerator RunForward()
    {
        PlayScareClip();
        yield return MoveForward();
        StopMovingAndGrowl();
        enemyAnimator.ResetTrigger("Run");
        enemyAnimator.SetTrigger("Idle");
    }
    
    private IEnumerator MoveForward()
    {       
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + transform.forward * runDistance;
        enemyAnimator.ResetTrigger("Idle");
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            enemyAnimator.SetTrigger("Run"); 
            transform.position += transform.forward * runSpeed * Time.deltaTime;
            yield return null;
        }
    }
    
    private void StopMovingAndGrowl()
    {
        StopMoving();
        PlayGrowlClip();
    }

    private void StopMoving()
    {
        runSpeed = 0f;
    }
    
    private void PlayScareClip()
    {
        isPlayingScareClip = true;
        audioSource.clip = scareClip;
        audioSource.volume = 1.0f;
        audioSource.loop = false;
        audioSource.Play();
    }

    private void PlayGrowlClip()
    {
        audioSource.clip = growlClip;
        audioSource.loop = true;
        audioSource.Play();
    }
    
    private void Update()
    {
        AdjustGrowlVolumeBasedOnDistance();
        if (runSpeed == 0)
        {
            RotateTowardsPlayer();
        }
    }
    
    private void AdjustGrowlVolumeBasedOnDistance()
    {
        if (audioSource != null && audioSource.clip == growlClip)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= maxGrowlDistance)
            {
                if (!audioSource.isPlaying)
                {
                    PlayGrowlClip();
                }
                float normalizedDistance = Mathf.InverseLerp(maxGrowlDistance, minGrowlDistance, distanceToPlayer);
                audioSource.volume = Mathf.Lerp(audioSource.volume, 1f - normalizedDistance, Time.deltaTime * growlFadeSpeed);
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * growlFadeSpeed);
                    if (audioSource.volume <= 0.05f)
                    {
                        audioSource.Stop();
                    }
                }
            }
        }
    }
    
    private void RotateTowardsPlayer()
    {
        if (targetToLookAt != null)
        {
            Vector3 directionToTarget = (targetToLookAt.position - transform.position).normalized;
            directionToTarget.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}