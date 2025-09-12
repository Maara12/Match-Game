using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaarasMatchGame
{

    public class CardManager : MonoBehaviour
    {
        [Tooltip("Slots must be equal to the number of cards")]
        [SerializeField] List<Transform> slots = new List<Transform>();

        [Tooltip("Cards must be equal to the number of slots")]
        [SerializeField] List<Card> allCards = new List<Card>();

        [SerializeField] int matchCount = 2;
        [SerializeField] int attempts = 0;
        [SerializeField] int attemptCounter = 0;
        [SerializeField] float winCheckDelay = 1f;
        [SerializeField] float scoreMultiplier = 1000f;

        [SerializeField] bool canClickAll = true;
        public bool GetCanClickAll => canClickAll;

        [SerializeField] List<Card> currentFlippedCards = new List<Card>();

        [SerializeField] Timer timer;
        [SerializeField] ProgressUIHandler progressUIHandler;
        [SerializeField] GameOverUIHandler gameOverUIHandler;

        [SerializeField] AudioPlayer audioPlayer;

        Action<string> onAttemptsUpdate;

        void Awake()
        {
            InitializeAllCards();
        }

        void OnEnable()
        {
            onAttemptsUpdate += progressUIHandler.SetAttemptsText;
        }

        void OnDisable()
        {
            onAttemptsUpdate -= progressUIHandler.SetAttemptsText;
        }

        void Start()
        {
            timer.StartTimer();
        }

        private void InitializeAllCards()
        {
            SetCardManagerToAllCards();
            SetAllCardsToRandomSlots();
        }

        private void SetCardManagerToAllCards()
        {
            for (int i = 0; i < allCards.Count; i++)
            {
                allCards[i].SetCardManager(this);
            }
        }

        public void SetAllCardsToRandomSlots()
        {
            if (slots.Count == allCards.Count)
            {
                if (slots.Count == 0)
                {
                    Debug.LogError("No slots/cards assigned in CardManager");
                    return;
                }

                List<Card> cardsWithUnassignedSlots = new List<Card>(allCards);

                for (int i = 0; i < slots.Count; i++)
                {
                    int randomCardIndex = UnityEngine.Random.Range(0, cardsWithUnassignedSlots.Count);
                    Card randomCard = cardsWithUnassignedSlots[randomCardIndex];

                    randomCard.SetSlot(slots[i]);
                    randomCard.SetSlotAsParent();

                    cardsWithUnassignedSlots.RemoveAt(randomCardIndex);

                }
            }
            else
            {
                Debug.LogError("Slots and Cards count do not match");
            }

        }
        public void SetCanClickAll(bool value)
        {
            canClickAll = value;
        }

        public void CheckIfCardsMatch()
        {
            if (attemptCounter != 0) return;
            if (currentFlippedCards.Count != matchCount) return;
            bool isMatched = true;

            for (int i = 1; i < currentFlippedCards.Count; i++)
            {
                if (currentFlippedCards[i].GetMatchID != currentFlippedCards[0].GetMatchID)
                {
                    isMatched = false;
                    break;
                }
            }

            if (isMatched)
            {
                for (int i = 0; i < currentFlippedCards.Count; i++)
                {
                    currentFlippedCards[i].AnimateCardScale();
                }

                Debug.Log($"<color=green>Matched!</color>");
                PlayMatchFoundSound();
            }
            else
            {
                for (int i = 0; i < currentFlippedCards.Count; i++)
                {
                    currentFlippedCards[i].ShakeCard();
                }

                for (int i = 0; i < currentFlippedCards.Count; i++)
                {
                    currentFlippedCards[i].FlipCardDown(currentFlippedCards[i].GetShakeDuration);
                }

                PlayNoMatchFoundSound();
            }

            UpdateCurrentFlippedCardStatus();
            StartCoroutine(DelayedCheckAllCardsMatched(winCheckDelay));
        }

        public void AddCardToCurrentFlippedCards(Card card)
        {
            currentFlippedCards.Add(card);
        }

        public void ClearCurrentFlippedCards()
        {
            currentFlippedCards.Clear();
        }

        public void UpdateCurrentFlippedCardStatus()
        {
            if (attemptCounter == 0)
            {
                ClearCurrentFlippedCards();
            }
        }



        private void CheckIfAllCardsMatched()
        {
            for (int i = 0; i < allCards.Count; i++)
            {
                if (!allCards[i].GetIsFacingUp)
                {
                    return;
                }
            }

            Debug.Log($"<color=yellow>All Cards Matched in {attempts} attempts!</color>");
            float timeTaken = timer.StopTimer();
            int score = (int)CalculateScore(timeTaken, attempts);
            Debug.Log($"<color=cyan>Score: {score}</color>");
            UpdateGameOverUI(score);
            gameOverUIHandler.ShowAndAnimatePanel();
            PlayWinSound();
        }



        private void UpdateGameOverUI(int score)
        {
            gameOverUIHandler.SetTimeTakenText(timer.GetFormattedTime);
            gameOverUIHandler.SetAttemptsText(attempts.ToString());
            gameOverUIHandler.SetScoreText(score.ToString());
        }

        //this method is called from Restart button from GameOverUI
        public void RestartGameDelayed(float delay)
        {
            if (gameOverUIHandler.gameObject.activeSelf)
            {
                gameOverUIHandler.HideAndAnimatePanel();
            }

            StartCoroutine(RestartGameCoroutine(delay));
        }

        IEnumerator RestartGameCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            for (int i = 0; i < allCards.Count; i++)
            {
                allCards[i].ResetCard();
            }
            attempts = 0;
            attemptCounter = 0;
            canClickAll = true;
            ClearCurrentFlippedCards();
            progressUIHandler.SetDefaultTimerText();
            progressUIHandler.SetDefaultAttemptsText();

            SetAllCardsToRandomSlots();
            timer.StartTimer();
        }

        IEnumerator DelayedCheckAllCardsMatched(float delay)
        {
            yield return new WaitForSeconds(delay);
            CheckIfAllCardsMatched();
        }

        public void UpdateAttemptCounter()
        {
            attemptCounter++;
            if (attemptCounter >= matchCount)
            {
                UpdateAttempt();
                attemptCounter = 0;
            }
        }

        private void UpdateAttempt()
        {
            attempts++;
            onAttemptsUpdate?.Invoke(attempts.ToString());
        }

        private float CalculateScore(float timeTaken, int attemptsMade)
        {
            float score = 0f;
            score = 1 / (timeTaken + attemptsMade) * (scoreMultiplier / 50);
            score *= scoreMultiplier;
            return score;

        }

        #region Audio Methods
        public void PlayCardClickSound()
        {
            audioPlayer.PlayAudioClip(0);
        }

        public void PlayCardFlipSound()
        {
            audioPlayer.PlayAudioClip(1);
        }

        public void PlayMatchFoundSound()
        {
            audioPlayer.PlayAudioClip(2);
        }

        public void PlayNoMatchFoundSound()
        {
            audioPlayer.PlayAudioClip(3);
        }
        private void PlayWinSound()
        {
            audioPlayer.PlayAudioClip(4);
        }
#endregion

    }
}
