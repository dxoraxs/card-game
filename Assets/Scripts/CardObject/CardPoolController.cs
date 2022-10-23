using UnityEngine;

public class CardPoolController : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Card prefab;
    private ObjectsPoolController<Card> pool;

    public Card GetFreeCard => pool.GetFreeObject();
    public void ReturnCardPool(Card card) => pool.ReturnObject(card);

    private void Start()
    {
        pool = new ObjectsPoolController<Card>(prefab, parent, 10);
    }
}