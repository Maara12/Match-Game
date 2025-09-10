using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaarasMatchGame
{

    public class CardManager : MonoBehaviour
    {
        [SerializeField] List<Transform> slots = new List<Transform>();
        [SerializeField] List<Card> allCards = new List<Card>();
        [SerializeField] int matchCount = 2;
        [SerializeField] int attempts = 0;
        [SerializeField] int attemptCounter = 0;

        [SerializeField] List<Card> currentFlippedCards = new List<Card>();
    }
}
