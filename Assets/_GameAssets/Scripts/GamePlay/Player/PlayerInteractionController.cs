using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(Consts.WheatTypes.GOLD_WHEATE))
        {
            other.gameObject.GetComponent<GoldWheatCollectible>().Collect();
        }

        if (other.CompareTag(Consts.WheatTypes.HOLY_WHEATE))
        {
            other.gameObject.GetComponent<HolyWheatCollectible>().Collect();
        }

        if (other.CompareTag(Consts.WheatTypes.ROTTEN_WHEATE))
        {
            other.gameObject.GetComponent<RottenWheatCollectible>().Collect();
        }

    }
}
