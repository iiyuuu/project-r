using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSlider : MonoBehaviour
{
  public TextMeshProUGUI numberText;

  [SerializeField] private Slider slider;

  void Start() {

  }

  public void SetNumberText(float value) {
        numberText.text = value.ToString();
  }
}
