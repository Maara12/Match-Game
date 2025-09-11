using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaarasMatchGame
{
    public class Card : MonoBehaviour
    {
        [SerializeField] int matchID = -1;
        [SerializeField] SpriteRenderer matchSpriteRenderer;
        [SerializeField] Transform slot;
        [SerializeField] bool isFacingUp = false;
        [SerializeField] bool canClick = true;
        [SerializeField] AnimationCurve flipCurve;
        [SerializeField] float flipDuration = 0.5f;
        [SerializeField] AnimationCurve scaleCurve;
        [SerializeField] float scaleDuration = 0.5f;
        [SerializeField] AnimationCurve shakeCurve;
        [SerializeField] float shakeDuration = 0.5f;

        [SerializeField] bool isAnimating = false;

        [SerializeField] CardManager cardManager;

        public void SetCardManager(CardManager manager)
        {
            cardManager = manager;
        }

        public void SetSlot(Transform slotTransform)
        {
            slot = slotTransform;
        }

        public void SetSlotAsParent()
        {
            if (slot == null) return;
            transform.SetParent(slot);
            transform.localPosition = Vector3.zero;
        }

        private void OnMouseDown()
        {
            if (!canClick) return;

            if (isAnimating) return;
            StartCoroutine(FlipCard());

        }
        

        IEnumerator FlipCard()
        {
            canClick = false;
            isAnimating = true;
            float timer = 0f;
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation;

            if (isFacingUp)
            {
                endRotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                endRotation = Quaternion.Euler(0f, 180f, 0f);
            }

            while (timer < flipDuration)
            {
                timer += Time.deltaTime;
                float t = timer / flipDuration;
                if (t >= 0.7f)
                {
                    if (!isFacingUp)
                    {
                        matchSpriteRenderer.enabled = true;
                    }
                    else
                    {
                        matchSpriteRenderer.enabled = false;
                    }
                     
                }
                t = flipCurve.Evaluate(t);
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
                yield return null;
            }
            transform.rotation = endRotation;

            if (isFacingUp)
            {
                isFacingUp = false;
            }
            else
            {
                isFacingUp = true;
            }
            isAnimating = false;
            canClick = true;
        }
        
        

    }

    
}
