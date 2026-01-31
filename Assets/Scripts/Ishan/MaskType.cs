using UnityEngine;

public enum MaskType
{
    Stone,
    Wing,
    Saitama
}

public class MaskTypeScript : MonoBehaviour
{
    public MaskType type;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player1")
        {
            EquipMask(collider);
        }

        if (collider.tag == "Player2")
        {
            EquipMask(collider);
        }
    }



    public void EquipMask(Collider col)
    {
        if (type == MaskType.Stone)
        {
            Debug.Log("Stone Mask Equipped");
           // what does this type do
        }
        else if (type == MaskType.Wing)
        {
            Debug.Log("Wing Mask Equipped");
            // what does this type do
        }
        else if (type == MaskType.Saitama)
        {
            Debug.Log("Saitama Mask Equipped");
            // what does this type do
        }

        Destroy(gameObject);
    }
}
