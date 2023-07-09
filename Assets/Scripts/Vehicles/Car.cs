using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class Car : MonoBehaviour
{
    public static event EventHandler<OnCarShotDownEventArgs> onCarShotDown;

    public class OnCarShotDownEventArgs : EventArgs
    {
        public int fuelBonusForKill;
        public int hpBonusForKill;

        public OnCarShotDownEventArgs(int fuelBonusForKill, int hpBonusForKill)
        {
            this.fuelBonusForKill = fuelBonusForKill;
            this.hpBonusForKill = hpBonusForKill;
        }
    }

    [SerializeField]
    private float maxSpeed;

    private float speed;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private CarColorsSO possibleCarColors;

    [SerializeField]
    private int _damageToPlayerOnCollision;

    [SerializeField]
    private ParticleSystem[] explosions;

    [SerializeField]
    private SoundEffectsSO carExplosionSFXSO;

    [SerializeField]
    private int fuelBonusForKill;

    [SerializeField]
    private int hpBonusForKill;

    public int damageToPlayerOnCollision
    {
        get => _damageToPlayerOnCollision;
    }

    private bool toDestroy = false;

    private void Start()
    {
        gameObject.GetComponent<HP>().OnDeath += HandleDeath;
        maxSpeed += UnityEngine.Random.Range(-2, 2);
        speed = maxSpeed;
        setRandomCarColor();
    }

    private void OnDestroy()
    {
        gameObject.GetComponent<HP>().OnDeath -= HandleDeath;
    }

    private void setRandomCarColor()
    {
        var materials = meshRenderer.materials;

        if (!possibleCarColors)
            return;

        materials[0] = possibleCarColors.carMaterials[
            UnityEngine.Random.Range(0, possibleCarColors.carMaterials.Length)
        ];
        meshRenderer.materials = materials;
    }

    private void Update()
    {
        if (toDestroy)
        {
            Destroy(gameObject);
            return;
        }
        float rayLength = 20;

        speed += 5 * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0, maxSpeed);
        transform.position += transform.forward * Time.deltaTime * speed;

        if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayLength))
            return;

        if (hit.collider.gameObject.TryGetComponent<Car>(out Car car) == false)
            return;

        speed -= Time.deltaTime * (rayLength - hit.distance) * 0.5f;
        speed = Mathf.Clamp(speed, 0, maxSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Car>(out Car car))
        {
            Explode();
            car.Explode();
        }
    }

    private void HandleDeath(object sender, EventArgs e)
    {
        Explode(true);
    }

    public void Explode(bool isShotDown = false)
    {
        if (isShotDown)
        {
            onCarShotDown?.Invoke(
                this,
                new OnCarShotDownEventArgs(fuelBonusForKill, hpBonusForKill)
            );
        }

        SoundManager.Instance.PlaySound(carExplosionSFXSO.audioClips, transform.position);
        gameObject.GetComponent<BoxCollider>().enabled = false;

        foreach (ParticleSystem explosion in explosions)
        {
            explosion.Play();
        }

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
