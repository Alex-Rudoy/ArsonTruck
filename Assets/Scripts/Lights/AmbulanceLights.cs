using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceLights : MonoBehaviour
{
    [SerializeField]
    private Light BlueLight;

    [SerializeField]
    private Light RedLight;

    [SerializeField]
    private Light SideLight1;

    [SerializeField]
    private Light SideLight2;

    private float time = 0;

    private void Start() { }

    private void Update()
    {
        time += Time.deltaTime;
        BlueLight.intensity = Mathf.Abs(Mathf.Sin(time * 5)) * 4;
        RedLight.intensity = Mathf.Abs(Mathf.Cos(time * 5));
        SideLight1.intensity = Mathf.Abs(Mathf.Sin(time * 12));
        SideLight2.intensity = Mathf.Abs(Mathf.Cos(time * 12));
    }
}
