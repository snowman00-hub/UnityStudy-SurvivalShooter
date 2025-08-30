using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;

    public AudioClip gunShotClip;
    
    private AudioSource audioSource;

    private float LastShootTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && LastShootTime + gun.shootInterval < Time.time)
        {
            LastShootTime = Time.time;
            gun.Shoot();
            audioSource.PlayOneShot(gunShotClip);
        }
    }
}
