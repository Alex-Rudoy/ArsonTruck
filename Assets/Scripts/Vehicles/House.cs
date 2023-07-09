using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class House : MonoBehaviour
{
    public static event EventHandler<onHouseShotDownEventArgs> onHouseShotDown;

    public class onHouseShotDownEventArgs : EventArgs
    {
        public int scoreBonusForKill;

        public onHouseShotDownEventArgs(int scoreBonusForKill)
        {
            this.scoreBonusForKill = scoreBonusForKill;
        }
    }

    [SerializeField]
    private ParticleSystem[] explosions;

    [SerializeField]
    private ParticleSystem[] fireParticles;

    [SerializeField]
    private SoundEffectsSO carExplosionSFXSO;

    [SerializeField]
    private int scoreBonusForKill;

    private void Start()
    {
        gameObject.GetComponent<HP>().OnDeath += HandleDeath;
    }

    private void OnDestroy()
    {
        gameObject.GetComponent<HP>().OnDeath -= HandleDeath;
    }

    private void HandleDeath(object sender, EventArgs e)
    {
        Explode();
    }

    public void Explode()
    {
        onHouseShotDown?.Invoke(this, new onHouseShotDownEventArgs(scoreBonusForKill));

        SoundManager.Instance.PlaySound(carExplosionSFXSO.audioClips, transform.position);
        gameObject.GetComponent<BoxCollider>().enabled = false;

        // foreach (ParticleSystem explosion in explosions)
        // {
        //     explosion.Play();
        // }
    }
}
