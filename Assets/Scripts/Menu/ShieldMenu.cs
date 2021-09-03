using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShieldMenu : MonoBehaviour,IShieldMenu
{
   [SerializeField] private Slider slider;

   private IShield _iShield;

   public void UpdateSlider()
   {
      slider.value = _iShield.CurrentShieldValue;
   }

   [Inject]
   private void Construct(IShield isShield)
   {
      _iShield = isShield;
   }
}
