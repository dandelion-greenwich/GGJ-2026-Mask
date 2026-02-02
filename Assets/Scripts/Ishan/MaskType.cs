using System;
using UnityEngine;
using StarterAssets;
using Random = Unity.Mathematics.Random;

public enum MaskType
{
    Stone,
    Wing,
    Saitama
}

public class MaskTypeScript : MonoBehaviour
{
    public MaskType type;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player 1") || collider.CompareTag("Player 2"))
        {
            EquipMask(collider);
        }
        
        if (collider.CompareTag("Ground"))
        {
            Floater floater = GetComponent<Floater>();
            if (floater != null)
            {
                floater.enabled = true;
                floater.ResetOffset();
            }
        }
    }

    private void Start()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        switch (rand)
        {
            case 1:
                type = MaskType.Stone;
                break;
            case 2:
                type = MaskType.Wing;
                break;
            case 3:
                type = MaskType.Saitama;
                break;
        }
        
        Floater floater = GetComponent<Floater>();
        if (floater != null) floater.enabled = false;
    }

    private void EquipMask(Collider col)
    {
        if (type == MaskType.Stone)
        {
            Debug.Log("Stone Mask Equipped By: " + col.tag);
            EquipStone(col);
        }
        else if (type == MaskType.Wing)
        {
            Debug.Log("Wing Mask Equipped By: " + col.tag);
            EquipWing(col);
        }
        else if (type == MaskType.Saitama)
        {
            Debug.Log("Saitama Mask Equipped By: " + col.tag);
            EquipSaitama(col);
        }

        Destroy(gameObject);
    }

    private void EquipStone(Collider col)
    {
        ThirdPersonController tpc = col.GetComponent<ThirdPersonController>();
        tpc.MoveSpeed = 1.0f;
        tpc.JumpHeight = 0.6f;

        Rigidbody rb = col.GetComponent<Rigidbody>();
        rb.mass = 10f;
        // add knockback multiplier

        Abilities a = col.GetComponent<Abilities>();
        a.equippedMask = Abilities.MaskTypeEquipped.Stone;
    }

    private void EquipWing(Collider col)
    {
        ThirdPersonController tpc = col.GetComponent<ThirdPersonController>();
        tpc.MoveSpeed = 4.0f;
        tpc.JumpHeight = 3.2f;

        Rigidbody rb = col.GetComponent<Rigidbody>();
        rb.mass = 0.5f;

        Abilities a = col.GetComponent<Abilities>();
        a.equippedMask = Abilities.MaskTypeEquipped.Wing;
    }

    private void EquipSaitama(Collider col)
    {
        ThirdPersonController tpc = col.GetComponent<ThirdPersonController>();
        tpc.MoveSpeed = 2.0f;
        tpc.JumpHeight = 2f;

        Rigidbody rb = col.GetComponent<Rigidbody>();
        rb.mass = 2f;

        Abilities a = col.GetComponent<Abilities>();
        a.equippedMask = Abilities.MaskTypeEquipped.Saitama;
    }
}
