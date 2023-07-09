using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarHP : MonoBehaviour
{
    [SerializeField]
    HP carHpComponent;

    [SerializeField]
    private Image Flame1;

    [SerializeField]
    private Image Flame2;

    [SerializeField]
    private Image Flame3;

    private void Start()
    {
        carHpComponent.OnHPChange += HandleHPChange;
    }

    private void OnDestroy()
    {
        carHpComponent.OnHPChange -= HandleHPChange;
    }

    private void HandleHPChange(object sender, HP.OnHPChangeEventArgs e)
    {
        if (e.HP < carHpComponent.MaxHP / 4 * 3)
        {
            if (!Flame1.gameObject.activeSelf)
            {
                Flame1.gameObject.SetActive(true);
            }
        }

        if (e.HP < carHpComponent.MaxHP / 4 * 2)
        {
            if (!Flame2.gameObject.activeSelf)
            {
                Flame2.gameObject.SetActive(true);
            }
        }

        if (e.HP < carHpComponent.MaxHP / 4)
        {
            if (!Flame3.gameObject.activeSelf)
            {
                Flame3.gameObject.SetActive(true);
            }
        }
    }
}
