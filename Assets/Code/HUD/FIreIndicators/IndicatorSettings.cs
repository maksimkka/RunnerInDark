using UnityEngine;
using UnityEngine.UI;

namespace Code.HUD.FIreIndicators
{
    public class IndicatorSettings : MonoBehaviour
    {
        [field: SerializeField] private Image[] Images;

        public void SwitchOn()
        {
            Images[0].gameObject.SetActive(false);
            Images[1].gameObject.SetActive(true);
        }
        
        public void SwitchOff()
        {
            Images[0].gameObject.SetActive(true);
            Images[1].gameObject.SetActive(false);
        }
    }
}