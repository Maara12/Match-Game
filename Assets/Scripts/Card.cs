using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaarasMatchGame
{
    public class Card : MonoBehaviour
    {
        [SerializeField] int matchID = -1;

        public int GetMatchID => matchID;
        [SerializeField] SpriteRenderer matchSpriteRenderer;
        [SerializeField] Transform slot;
        [SerializeField] bool isFacingUp = false;
        public bool GetIsFacingUp => isFacingUp;
        [SerializeField] bool canClick = true;
        [SerializeField] AnimationCurve flipCurve;
        [SerializeField] float flipDuration = 0.5f;
        [SerializeField] AnimationCurve scaleCurve;
        [SerializeField] float scaleDuration = 0.5f;
        [SerializeField] float scaleStrength = 10f;
        [SerializeField] AnimationCurve shakeCurve;
        [SerializeField] float shakeDuration = 0.5f;
        public float GetShakeDuration => shakeDuration;
        [SerializeField] float shakeStrength = 10f;

        [SerializeField] bool isFlippingUp = false;
        [SerializeField] bool isFlippingDown = false;
        [SerializeField] bool isCardShaking = false;
        [SerializeField] bool isCardInScaleAnimation = false;

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

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AnimateCardScale();
            }
        }

        private void OnMouseDown()
        {
            if (!cardManager.GetCanClickAll) return;

            if (!canClick) return;

            if (isFacingUp) return;

            if (isFlippingUp) return;

            cardManager.PlayCardClickSound();
            StartCoroutine(FlipCardUp());

        }


        IEnumerator FlipCardUp()
        {
            canClick = false;
            cardManager.PlayCardFlipSound();
            cardManager.SetCanClickAll(false);

            isFlippingUp = true;

            cardManager.AddCardToCurrentFlippedCards(this);
            cardManager.UpdateAttemptCounter();

            float timer = 0f;
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.Euler(0f, 180f, 0f);

            while (timer < flipDuration)
            {
                timer += Time.deltaTime;
                float t = timer / flipDuration;
                if (t >= 0.7f)
                {
                    matchSpriteRenderer.enabled = true;
                }

                t = flipCurve.Evaluate(t);
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
                yield return null;
            }
            transform.rotation = endRotation;

            isFacingUp = true;
            isFlippingUp = false;

            canClick = true;
            cardManager.SetCanClickAll(true);
            cardManager.CheckIfCardsMatch();
        }

        public void FlipCardDown(float delay = 0f)
        {
            if (isFlippingDown) return;
            StartCoroutine(FlipCardDownCoroutine(delay));
        }

        IEnumerator FlipCardDownCoroutine(float delay = 0f)
        {
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }
            cardManager.PlayCardFlipSound();
            cardManager.SetCanClickAll(false);
            canClick = false;
            isFlippingDown = true;

            float timer = 0f;
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.Euler(0f, 0f, 0f);

            while (timer < flipDuration)
            {
                timer += Time.deltaTime;
                float t = timer / flipDuration;
                if (t >= 0.7f)
                {
                    matchSpriteRenderer.enabled = false;
                }

                t = flipCurve.Evaluate(t);
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
                yield return null;
            }
            transform.rotation = endRotation;

            isFacingUp = false;
            isFlippingDown = false;

            canClick = true;
            cardManager.SetCanClickAll(true);
        }

        public void ShakeCard()
        {
            if (isCardShaking) return;
            StartCoroutine(ShakeCardCoroutine());
        }

        IEnumerator ShakeCardCoroutine()
        {
            cardManager.SetCanClickAll(false);
            canClick = false;
            isCardShaking = true;

            float timer = 0f;
            Quaternion startRotation = transform.rotation;

            while (timer < shakeDuration)
            {
                timer += Time.deltaTime;
                float t = timer / shakeDuration;
                t = shakeCurve.Evaluate(t);
                transform.rotation = startRotation * Quaternion.Euler(0f, 0f, t * shakeStrength);
                yield return null;
            }
            transform.rotation = startRotation;

            isCardShaking = false;

            canClick = true;
            cardManager.SetCanClickAll(true);
        }

        public void AnimateCardScale()
        {
            if (isCardInScaleAnimation) return;
            StartCoroutine(AnimateCardScaleCoroutine());
        }
        
        IEnumerator AnimateCardScaleCoroutine()
        {
            cardManager.SetCanClickAll(false);
            canClick = false;
            isCardInScaleAnimation = true;

            float timer = 0f;
            Vector3 startScale = transform.localScale;

            while (timer < scaleDuration)
            {
                timer += Time.deltaTime;
                float t = timer / scaleDuration;
                t = scaleCurve.Evaluate(t);
                //transform.localScale = new Vector3(startScale.x * t, startScale.y * t, startScale.z);
                transform.localScale = startScale * (1 + t * scaleStrength);
                yield return null;
            }
            transform.localScale = startScale;

            isCardInScaleAnimation = false;

            canClick = true;
            cardManager.SetCanClickAll(true);
        }
        
        

    }

    
}
