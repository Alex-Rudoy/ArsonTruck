using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
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
        float alpha1 = 1 - (e.HP / carHpComponent.MaxHP - 0.75f) * 4;
        Flame1.color = new Color(1, 1, 1, alpha1);

        float alpha2 = 1 - (e.HP / carHpComponent.MaxHP - 0.5f) * 4;
        Flame2.color = new Color(1, 1, 1, alpha2);

        float alpha3 = 1 - (e.HP / carHpComponent.MaxHP - 0.25f) * 4;
        Flame3.color = new Color(1, 1, 1, alpha3);
    }
}
