using System.Collections;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireDistance = 7f;
    public float damage = 10f;

    public ParticleSystem shotEffect;

    public Transform firePosition;

    private LineRenderer lineRenderer;
    public float shootInterval = 0.2f;
    public float effectWaitTime = 0.1f;

    private bool isShooting = false;
    Vector3 hitPosition = Vector3.zero;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (isShooting)
        {
            lineRenderer.SetPosition(0, firePosition.position);
            lineRenderer.SetPosition(1, hitPosition);
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePosition.position, firePosition.forward,
            out hit, fireDistance))
        {
            hitPosition = hit.point;

            var target = hit.collider.GetComponent<IDamagable>();
            if(target != null)
            {
                target.OnDamage(damage, hitPosition, hit.normal);
            }
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

        isShooting = true;

        yield return new WaitForSeconds(effectWaitTime);

        lineRenderer.enabled = false;
        isShooting = false;
    }
}
