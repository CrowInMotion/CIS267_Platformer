using UnityEngine;

//attached to collectable used to remove object when needed
//and getthe value of the given collectable

public class CollectibleData : MonoBehaviour
{
    [SerializeField]
    private int collectableValue;
    
    public void destroyCollectable()
    {
        Destroy(this.gameObject);
    }

    public void getCollectableValue()
    {

    }
}
