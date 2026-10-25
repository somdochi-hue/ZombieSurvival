using System.Collections;
using System.Security.AccessControl;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State // enum 열거형 
    {
        Ready,            //상태이며 상태 패턴이라고 한다.
        Empty,
        Reloading,
    }

    public State CurrentState {get; private set;} 

    public Transform fireTransform;

    public ParticleSystem muzzleFlashEffect;
    public ParticleSystem shellEjcetEffect;

    private LineRenderer bulletLineRenderer;

    private AudioSource gunAudioPlayer;

    public GunData gunData;

    private float fireDistance = 50f;

    private int ammoRemain;

    private int magAmmo;

    private float lastFireTime;

    public UIManager uimanager;

    private void Awake()
    {
        bulletLineRenderer = GetComponent<LineRenderer>();
        gunAudioPlayer = GetComponent<AudioSource>();

        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    private void OnEnable()
    {
        ammoRemain = gunData.startAmmoRemain;
        magAmmo = gunData.magCapacity;
        uimanager.SetAmmoText(magAmmo, ammoRemain);
        CurrentState = State.Ready;
        lastFireTime = 0f;
    }

    public void Fire()
    {
        if(CurrentState == State.Ready && Time.time >= lastFireTime + gunData.timeBetFire)
        {
            lastFireTime = Time.time;
            Shoot();
        }
    }

    public LayerMask targetLayer; // 일부 타켓 레이어만 선택해서 데이터를 불러 올 수 있다. (Tag와 비슷한 형식)
    private void Shoot()
    {
        Vector3 hitPosition;
        
        Ray ray = new Ray(fireTransform.position, fireTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, fireDistance, targetLayer))
        {
            hitPosition = hit.point;
            IDamegeable target = hit.collider.GetComponent<IDamegeable>();
            if (target != null)
            {
                target.OnDamege(gunData.damage, hit.point, hit.normal);
            }
        }
        else
        {
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }
        
        StartCoroutine(ShotEffect(hitPosition));
        
        magAmmo--;
        uimanager.SetAmmoText(magAmmo, ammoRemain);

        if (magAmmo == 0)
        {
            CurrentState = State.Empty;
        }
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        muzzleFlashEffect.Play();
        shellEjcetEffect.Play();
        gunAudioPlayer.PlayOneShot(gunData.shotClip);
        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);
        bulletLineRenderer.enabled = true;
        yield return new WaitForSeconds(0.03f);
        bulletLineRenderer.enabled = false;
    }
    public bool Reload()
    {
        if (magAmmo < gunData.magCapacity && ammoRemain > 0 )
        {
           StartCoroutine(Reloading());
           return true;
        }
        return false;
    }

    private IEnumerator Reloading()
    {    
        gunAudioPlayer.PlayOneShot(gunData.reloadClip);
        yield return new WaitForSeconds (gunData.reloadTime);
        if (ammoRemain >= gunData.magCapacity - magAmmo)
        {
            ammoRemain -= gunData.magCapacity - magAmmo;
            magAmmo += gunData.magCapacity - magAmmo;
        }
        else if (ammoRemain < gunData.magCapacity - magAmmo)
        {
            magAmmo += ammoRemain;
            ammoRemain -= ammoRemain;
        }
        CurrentState = State.Ready;
        uimanager.SetAmmoText(magAmmo, ammoRemain);
    }
}