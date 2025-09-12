using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MaarasMatchGame
{
    public class GameOverUIHandler : MonoBehaviour
    {
        [SerializeField] string mainMenuSceneName;
        [SerializeField] Text timeTakenText_GameOverUI;
        [SerializeField] Text attempsText_GameOverUI;
        [SerializeField] Text scoreText;
        [SerializeField] Text highScoreText;

        [SerializeField] AnimationCurve scaleUpCurve;
        [SerializeField] AnimationCurve scaleDownCurve;

        [SerializeField] float animDuration = 0.5f;

        bool isScalingUp = false;
        bool isScalingDown = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShowAndAnimatePanel();
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                HideAndAnimatePanel();
            }
        }

        public void OnMainMenuButtonClicked()
        {
            Invoke(nameof(LoadMainMenuScene),1f);
        }

        private void LoadMainMenuScene()
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }

        public void SetTimeTakenText(string timeTaken)
        {
            timeTakenText_GameOverUI.text = timeTaken;
        }

        public void SetAttemptsText(string attempts)
        {
            attempsText_GameOverUI.text = attempts;
        }
        public void SetScoreText(string score)
        {
            scoreText.text = score;
        }
        public void SetHighScoreText(string highScore)
        {
            highScoreText.text = highScore;
        }

        public void ShowAndAnimatePanel()
        {
            if (isScalingUp) return;
            transform.gameObject.SetActive(true);
            StartCoroutine(ScaleUpCoroutine());
        }

        IEnumerator ScaleUpCoroutine()
        {
            isScalingUp = true;

            float timer = 0f;
            Vector3 startScale = transform.localScale;
            Vector3 endScale = new Vector3(1f, 1f, 1f);

            while (timer < animDuration)
            {
                timer += Time.deltaTime;
                float t = timer / animDuration;
                t = scaleUpCurve.Evaluate(t);
                transform.localScale = Vector3.LerpUnclamped(startScale, endScale, t);
                yield return null;
            }
            transform.localScale = endScale;

            isScalingUp = false;

        }

        public void HideAndAnimatePanel()
        {
            if (isScalingDown) return;
            StartCoroutine(ScaleDownCoroutine(() => transform.gameObject.SetActive(false)) );
        }


        IEnumerator ScaleDownCoroutine(Action onComplete = null)
        {
            isScalingDown = true;

            float timer = 0f;
            Vector3 startScale = transform.localScale;
            Vector3 endScale = new Vector3(0.01f, 0.01f, 0.01f);

            while (timer < animDuration)
            {
                timer += Time.deltaTime;
                float t = timer / animDuration;
                t = scaleDownCurve.Evaluate(t);
                transform.localScale = Vector3.LerpUnclamped(startScale, endScale, t);
                yield return null;
            }
            transform.localScale = endScale;

            isScalingDown = false;

            onComplete?.Invoke();
        }
    }

    
}

