using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShieldMenu : MonoBehaviour
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
