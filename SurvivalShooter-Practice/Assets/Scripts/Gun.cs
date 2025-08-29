using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireDistance = 7f;

    public ParticleSystem shotEffect;

    public Transform firePosition;

    private LineRenderer lineRenderer;
    private float LastShootTime;
    public float shootInterval = 0.1f;
    public float effectWaitTime = 0.1f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && LastShootTime + shootInterval < Time.time)
        {
            LastShootTime = Time.time;
            Shoot();
        }   
    }

    public void Shoot()
    {
        Vector3 hitPosition = Vector3.zero;

        RaycastHit hit;
        if (Physics.Raycast(firePosition.position, firePosition.forward,
            out hit, fireDistance))
        {
            hitPosition = hit.point;
        }
        else
        {
            hitPosition = firePosition.position + firePosition.forward * fireDistance;
        }

        StartCoroutine(CoShotEffect(hitPosition));
    }

    private IEnumerator CoShotEffect(Vector3 hitPosition)
    {
        shotEffect.Play();
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePosition.position);
        lineRenderer.SetPosition(1, hitPosition);

        yield return new WaitForSeconds(effectWaitTime);

        lineRenderer.enabled = false;
    }
}
