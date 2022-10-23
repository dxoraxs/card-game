using UnityEngine;

public class PreviewCardController : MonoBehaviour
{
    public void SetCard(Card card)
    {
        card.transform.SetParent(transform);
    }
}