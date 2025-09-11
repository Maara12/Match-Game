using System;
using UnityEngine;

namespace MaarasMatchGame
{

    public class Timer : MonoBehaviour
    {
        [SerializeField] ProgressUIHandler progressUIHandler;
        bool timerActive = false;
        [SerializeField]float currentTime = 0f;

        [SerializeField] string formattedTime = "00:00";

        Action<string> onTimerRun;
    
        void OnEnable()
        {
            onTimerRun += progressUIHandler.SetTimerText;
        }

        void OnDisable()
        {
            onTimerRun -= progressUIHandler.SetTimerText;
        }
        

        void Update()
        {
            RunTimer();
        }

        private bool RunTimer()
        {
            if (!timerActive) return false;

            currentTime += Time.deltaTime;
            formattedTime = FormatTime(currentTime);
            //Debug.Log(formattedTime);
            onTimerRun?.Invoke(formattedTime);
            return true;
        }

        public void StartTimer()
        {
            timerActive = true;
            currentTime = 0f;
        }

        public float StopTimer()
        {
            timerActive = false;
            return currentTime;
        }

        private string FormatTime(float timeInSeconds)
        {
            int minutes = Mathf.FloorToInt(timeInSeconds / 60F);
            int seconds = Mathf.FloorToInt(timeInSeconds - minutes * 60F);

            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            return formattedTime;
        }
    }
}
