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

        [SerializeField] List<Card> currentFlippedCards = new List<Card>();

        void Awake()
        {
            InitializeAllCards();
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
                    int randomCardIndex = Random.Range(0, cardsWithUnassignedSlots.Count);
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
    }
}
