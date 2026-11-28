using UnityEngine;
using System.Collections;

public class footStepController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform footTarget;
    [SerializeField] private Transform footHome;

    [Header("ground Detection")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float rayStartHeight = 1f;
    [SerializeField] private float rayDistance = 2.5f;
    [SerializeField] private float footGroundOffset = 0.02f;

    [Header("Step Settings")]
    [SerializeField] private float stepTriggerDistance = 0.35f;
    [SerializeField] private float stepDuration = 0.2f;
    [SerializeField] private float stepHeight = 0.1f;
    [SerializeField] private float fowardOvershoot = 0.1f;

    public bool isMoving { get; private set; }
    public bool wantToStep
    {
        get
        {
            if (footTarget == null || footHome == null) return false;
            
            Vector3 desiredPosition;
            Quaternion desiredRotation;
            GetDesiredFootPose(out desiredPosition, out desiredRotation);

            float distance = Vector3.Distance(footTarget.position, desiredPosition);
            return distance > stepTriggerDistance;

        }
    }

    private void Reset()
    {
        footTarget = transform;
    }
    public void TryStep()
    {
        if (isMoving || !wantToStep) return;

        StartCoroutine(StepCoroutine());
    }

    private void GetDesiredFootPose(out Vector3 position, out Quaternion rotation)
    {
        Vector3 rayOrigin = footHome.position + Vector3.up * rayStartHeight;
        
        if(Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, rayDistance, groundMask, QueryTriggerInteraction.Ignore))
        {
            position = hit.point + hit.normal * footGroundOffset;
            Vector3 projectedForward = Vector3.ProjectOnPlane(footHome.forward, hit.normal).normalized;
            
            if(projectedForward.sqrMagnitude > 0.001f)
            {
                projectedForward = Vector3.forward;
            }

            rotation = Quaternion.LookRotation(projectedForward, hit.normal);
        }
        else
        {
            position = footHome.position;
            rotation = footHome.rotation;
        }
    }
    private IEnumerator StepCoroutine()
    {
        isMoving = true;

        Vector3 startPosition = footTarget.position;
        Quaternion startRotation = footTarget.rotation;

        Vector3 endPosition;
        Quaternion endRotation;

        GetDesiredFootPose(out endPosition, out endRotation);

        Vector3 travelDirection = endPosition - startPosition;
        Vector3 HorizontalDirection = Vector3.ProjectOnPlane(travelDirection, Vector3.up);

        if(HorizontalDirection.sqrMagnitude > 0.001f)
        {
            endPosition += HorizontalDirection.normalized * fowardOvershoot;
        }

        float elapsedTime = 0f;

        while(elapsedTime < stepDuration)
        {
            elapsedTime += Time.deltaTime;

            float normalizedTime = Mathf.Clamp01(elapsedTime / stepDuration);

            float easedTime = normalizedTime * normalizedTime * (3f - 2f * normalizedTime);

            Vector3 position = Vector3.Lerp(startPosition, endPosition, easedTime);

            float lift = 4f * stepHeight * normalizedTime * (1f - normalizedTime);
            position += Vector3.up * lift;
            footTarget.position = position;
            footTarget.rotation = Quaternion.Slerp(startRotation, endRotation, easedTime);

            yield return null;
        }

        footTarget.position = endPosition;
        footTarget.rotation = endRotation;

        isMoving = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (footHome == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(footHome.position, stepTriggerDistance);
        
        Gizmos.color = Color.cyan;
        Vector3 rayOrigin = footHome.position + Vector3.up * rayStartHeight;
        Gizmos.DrawLine(rayOrigin, rayOrigin + Vector3.down * rayDistance);
        }

}