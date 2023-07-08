using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        lookAt,
        lookAtInverted,
        cameraForward,
        cameraForwardInverted,
    }

    [SerializeField]
    private Mode mode = Mode.cameraForward;

    void LateUpdate()
    {
        switch (mode)
        {
            case Mode.lookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.lookAtInverted:
                Vector3 invertedDirection = transform.position - Camera.main.transform.position;
                transform.LookAt(invertedDirection);
                break;
            case Mode.cameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.cameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;

            default:
                throw new System.NotImplementedException();
        }
    }
}
