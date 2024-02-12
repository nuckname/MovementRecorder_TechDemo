using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] 
    private GunData gunData;
    [SerializeField] 
    private Transform cam;
    [SerializeField]
    private GameObject bullet;

    public GameObject muzzleFlash, bulletHoleGraphic;

    [SerializeField]
    private Transform muzzle;

    float timeSinceLastShot;

    [SerializeField]
    private Transform playerRotation;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        Debug.Log("Gun script started");

        gunData.currentAmmo = gunData.magSize; 
    }

    private void OnDisable()
    {
        gunData.reloading = false;
        Debug.Log("Gun script disabled");
    }

    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
        {
            StartCoroutine(Reload());
            Debug.Log("Start reload coroutine");
        }
        else
        {
            Debug.Log("Cannot start reload: reloading in progress or object not active");
        }
    }

    private IEnumerator Reload()
    {
        Debug.Log("Starting reload");
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;

        Debug.Log("Reload finished");
    }

    private bool CanShoot()
    {
        bool canShoot = !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
        if (!canShoot)
        {
            Debug.Log("Cannot shoot: reloading in progress or fire rate limit exceeded");
        }
        return canShoot;
    }

    private void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                SpawnBullet();

                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        damageable.TakeDamage(gunData.damage);
                        Debug.Log("Hit: " + hitInfo.transform.name);

                        //Instantiate(bulletHoleGraphic, hitInfo.point, Quaternion.Euler(0, 180, 0));
                        //change attackPoint.position to the end of the gun point. 
                        //Instantiate(muzzle.position, attackPoint.position, Quaternion.identity);

                    }
                    else
                    {
                        Debug.Log("Hit: " + hitInfo.transform.name + ", but no IDamageable component found");
                    }
                }
                else
                {
                    Debug.Log("No hit detected");
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
        else
        {
            Debug.Log("Cannot shoot: out of ammo");
        }
    }

    private void SpawnBullet()
    {
       
       
        GameObject currentBullet = Instantiate(bullet, muzzle.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //Rotate bullet to shoot direction
        currentBullet.transform.rotation = playerRotation.transform.rotation;

        //Add forces to bullet

        currentBullet.GetComponent<Rigidbody>().AddForce(muzzle.transform.position, ForceMode.Impulse);
        //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * 5, ForceMode.Impulse);
        
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance, Color.green);
    }

    private void OnGunShot()
    {
        // No implementation needed for now
    }
}
