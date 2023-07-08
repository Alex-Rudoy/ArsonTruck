using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarLights : MonoBehaviour
{
    [SerializeField]
    private Light BlueLight;

    [SerializeField]
    private Light RedLight;

    private float time = 0;

    private void Start() { }

    private void Update()
    {
        time += Time.deltaTime;
        BlueLight.intensity = Mathf.Abs(Mathf.Sin(time * 5)) * 4;
        RedLight.intensity = Mathf.Abs(Mathf.Cos(time * 5));
    }
}
