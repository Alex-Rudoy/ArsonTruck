using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  [SerializeField]
  private Button startButton;

  [SerializeField]
  private Button exitButton;
  [SerializeField]
  private AudioSource hoverSound;
  [SerializeField]
  private AudioSource clickSound;
  [SerializeField]
  private Color hoverColor;
  [SerializeField]
  private Color normalColor;

  private void Awake()
  {
    startButton.onClick.AddListener(() =>
    {
      PlayClickSound();
      Loader.LoadScene(Loader.ScenesEnum.GameScene);
    });
    exitButton.onClick.AddListener(() =>
    {
      PlayClickSound();
      Application.Quit();
    });
  }

  void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
  {
    PlayHoverSound();
    startButton.image.color = hoverColor;
  }

  void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
  {
    PlayHoverSound();
    startButton.image.color = normalColor;
  }

  private void PlayHoverSound()
  {
    hoverSound.Play();
  }

  private void PlayClickSound()
  {
    clickSound.Play();
  }
}
