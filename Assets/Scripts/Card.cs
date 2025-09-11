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
        [SerializeField] AnimationCurve shakeCurve;
        [SerializeField] float shakeDuration = 0.5f;

        [SerializeField] bool isFlippingUp = false;
        [SerializeField] bool isFlippingDown = false;

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
            if (!cardManager.GetCanClickAll) return;

            if (!canClick) return;

            if (isFacingUp) return;

            if (isFlippingUp) return;
            StartCoroutine(FlipCardUp());

        }


        IEnumerator FlipCardUp()
        {
            canClick = false;
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
        
        public void FlipCardDown()
        {
            if (isFlippingDown) return;
            StartCoroutine(FlipCardDownCoroutine());
        }

        IEnumerator FlipCardDownCoroutine()
        {
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
        
        

    }

    
}
