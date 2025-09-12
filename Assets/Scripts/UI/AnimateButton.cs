using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MaarasMatchGame
{
    public class AnimateButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField] bool canAnimate = true;

        [SerializeField] float animationDuration = 0.25f;
        [SerializeField] float scaleMultiplier = 1.2f;


        bool isScalingUp = false;
        bool isScalingDown = false;

        public UnityEvent onPointerEnter;

        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter?.Invoke();
            ScaleUpButton();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ScaleDownButton();
        }

        public void ScaleUpButton()
        {
            if (!canAnimate) return;
            if (isScalingUp) return;
            StartCoroutine(ScaleUpCoroutine());
        }

        IEnumerator ScaleUpCoroutine(Action onComplete = null)
        {
            isScalingUp = true;

            float timer = 0f;
            Vector3 startScale = transform.localScale;
            Vector3 endScale = new Vector3(1f, 1f, 1f) * scaleMultiplier;

            while (timer < animationDuration)
            {
                timer += Time.deltaTime;
                float t = timer / animationDuration;
                transform.localScale = Vector3.Lerp(startScale, endScale, t);
                yield return null;
            }
            transform.localScale = endScale;

            isScalingUp = false;

            onComplete?.Invoke();
        }

        public void ScaleDownButton()
        {
            if (!canAnimate) return;
            if (isScalingDown) return;
            StartCoroutine(ScaleDownCoroutine());
        }



        IEnumerator ScaleDownCoroutine(Action onComplete = null)
        {
            isScalingDown = true;

            float timer = 0f;
            Vector3 startScale = transform.localScale;
            Vector3 endScale = new Vector3(1f, 1f, 1f);

            while (timer < animationDuration)
            {
                timer += Time.deltaTime;
                float t = timer / animationDuration;
                transform.localScale = Vector3.Lerp(startScale, endScale, t);
                yield return null;
            }
            transform.localScale = endScale;

            isScalingDown = false;

            onComplete?.Invoke();
        }

       
    }


}

