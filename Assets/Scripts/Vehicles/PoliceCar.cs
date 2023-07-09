using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class PoliceCar : MonoBehaviour
{
    public static event EventHandler onPoliceCarDestroyed;

    [SerializeField]
    private SoundEffectsSO policemanShoutsSFXSO;

    [SerializeField]
    private SoundEffectsSO policeGunSFXSO;

    [SerializeField]
    private SoundEffectsSO carExplosionSFXSO;

    [SerializeField]
    private ParticleSystem[] explosions;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private Transform emissionPoint;

    [SerializeField]
    private GameObject bulletPrefab;

    private bool toDestroy = false;

    private float timeToNextBullet = 0;
    private float timeBetweenBullets = 0.5f;

    private float timeToNextShout = 0;
    private float timeBetweenShouts = 5f;

    private void Start()
    {
        gameObject.GetComponent<HP>().OnDeath += HandleDeath;
    }

    private void OnDestroy()
    {
        gameObject.GetComponent<HP>().OnDeath -= HandleDeath;
    }

    private void Update()
    {
        if (toDestroy)
        {
            Destroy(gameObject);
            return;
        }

        HandleMovement();
        HandleShooting();
        HandleShouts();
    }

    private void HandleMovement()
    {
        transform.LookAt(Player.Instance.transform);
        transform.position +=
            transform.forward
            * 2
            * Time.deltaTime
            * (Vector3.Distance(transform.position, Player.Instance.transform.position));
    }

    private void HandleShooting()
    {
        if (Vector3.Distance(transform.position, Player.Instance.transform.position) > 20)
            return;

        timeToNextBullet += Time.deltaTime;

        if (timeToNextBullet < 0.5f)
            return;

        timeToNextBullet -= timeBetweenBullets;

        GameObject bullet = Instantiate(bulletPrefab, emissionPoint.position, transform.rotation);

        SoundManager.Instance.PlaySound(policeGunSFXSO.audioClips, transform.position);
    }

    private void HandleShouts()
    {
        if (Vector3.Distance(transform.position, Player.Instance.transform.position) > 40)
            return;

        timeToNextShout += Time.deltaTime;

        if (timeToNextShout < timeBetweenShouts)
            return;

        timeToNextShout -= timeBetweenShouts;

        SoundManager.Instance.PlaySound(policemanShoutsSFXSO.audioClips, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Car>(out Car car))
        {
            car.Explode();
        }
    }

    private void HandleDeath(object sender, EventArgs e)
    {
        Explode(true);
    }

    public void Explode(bool isShotDown = false)
    {
        SoundManager.Instance.PlaySound(carExplosionSFXSO.audioClips, transform.position);
        gameObject.GetComponent<BoxCollider>().enabled = false;

        foreach (ParticleSystem explosion in explosions)
        {
            explosion.Play();
        }
        onPoliceCarDestroyed?.Invoke(this, EventArgs.Empty);
        StartCoroutine(ExplodeCoroutine());
    }

    private IEnumerator ExplodeCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        if (meshRenderer == null)
            yield break;
        meshRenderer.enabled = false;
        yield return new WaitForSeconds(0.2f);
        if (gameObject == null)
            yield break;

        toDestroy = true;
    }
}
