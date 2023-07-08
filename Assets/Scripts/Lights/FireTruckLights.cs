using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTruckLights : MonoBehaviour
{
    [SerializeField]
    private Light Light1;

    [SerializeField]
    private Light Light2;

    private float time = 0;

    private void Start() { }

    private void Update()
    {
        time += Time.deltaTime;
        Light1.intensity = Mathf.Abs(Mathf.Sin(time * 10));
        Light2.intensity = Mathf.Abs(Mathf.Cos(time * 10));
    }
}
