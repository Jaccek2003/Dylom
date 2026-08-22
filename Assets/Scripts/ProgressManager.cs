using UnityEngine;
using UnityEngine.Events;

public class ProgressManager : MonoBehaviour
{
    public UnityEvent onGameStarted;

    private bool wasBackpackTaken = false;
    public bool WasBackpackTaken
    {
        get => wasBackpackTaken;
        set => wasBackpackTaken = value;
    }

    [SerializeField] private UnityEvent allFlowersCollected;

    private bool flowerOne = false;
    public bool FlowerOne
    {
        get => flowerOne;
        set
        {
            flowerOne = value;
            CheckFlowers();
        }
    }

    private bool flowerTwo = false;
    public bool FlowerTwo
    {
        get => flowerTwo;
        set
        {
            flowerTwo = value;
            CheckFlowers();
        }
    }

    private bool flowerThree = false;
    public bool FlowerThree
    {
        get => flowerThree;
        set
        {
            flowerThree = value;
            CheckFlowers();
        }
    }

    private void CheckFlowers()
    {
        if (flowerOne && flowerTwo && flowerThree)
        {
            allFlowersCollected?.Invoke();
        }
    }

    public static ProgressManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        onGameStarted.Invoke();
    }
}
