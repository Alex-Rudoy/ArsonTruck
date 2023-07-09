using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// class to add hover and click functionality to buttons
public class ButtonHoverClicker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private SoundEffectsSO hoverSound;

    [SerializeField]
    private SoundEffectsSO clickSound;

    private Color defaultColor;

    private void Start()
    {
        defaultColor = GetComponent<Image>().color;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(clickSound.audioClips, Vector3.zero, 0, 0.7f);
        });
        ;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(0.6823529f, 0.3686275f, 0.3568628f, 1);
        SoundManager.Instance.PlaySound(hoverSound.audioClips, Vector3.zero, 0, 0.3f);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = defaultColor;
    }
}
