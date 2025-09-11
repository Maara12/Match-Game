
using UnityEngine;
using UnityEngine.UI;

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

    public void SetAttemptsText(string text)
    {
        if (attemptsText_Runtime != null)
        {
            attemptsText_Runtime.text = text;
        }
    }
}
