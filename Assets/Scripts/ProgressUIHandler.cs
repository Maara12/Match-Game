
using UnityEngine;
using UnityEngine.UI;

namespace MaarasMatchGame
{
    public class ProgressUIHandler : MonoBehaviour
    {
        [SerializeField] Text timerText_Runtime;
        [SerializeField] Text attemptsText_Runtime;

        public void SetTimerText(string text)
        {
            if (timerText_Runtime != null)
            {
                timerText_Runtime.text = text;
            }
        }

        public void SetDefaultTimerText()
        {
            if (timerText_Runtime != null)
            {
                timerText_Runtime.text = "00.00";
            }
        }

        public void SetAttemptsText(string text)
        {
            if (attemptsText_Runtime != null)
            {
                attemptsText_Runtime.text = text;
            }
        }
        
        public void SetDefaultAttemptsText()
        {
            if (attemptsText_Runtime != null)
            {
                attemptsText_Runtime.text = "0";
            }
        }
    }
}
