using UnityEngine;
using StarterAssets;

public class Abilities : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Attack")] 
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float maxDistance = 1f;

    [Header("Lives")] 
    public int lives;
    public int maxLives;
    public int forceStrengthZ;
    public int forceStrengthY;

    public enum MaskTypeEquipped {Stone, Wing, Saitama}
    public MaskTypeEquipped equippedMask;
    
    
    void Start()
    {
        lives = maxLives;
    }

    public void Attack()
    {
        Debug.Log("Attack");
        
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Vector3 direction = transform.forward;

        if (Physics.SphereCast(origin, radius, direction, out RaycastHit hit, maxDistance))
        {
            Debug.Log("Hit: " + hit.collider.name);
            if (!hit.collider.CompareTag("Player 1") 
                && !hit.collider.CompareTag("Player 2"))
            return;
            
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            ThirdPersonController tpc = hit.collider.GetComponent<ThirdPersonController>();
            Abilities ab = hit.collider.gameObject.GetComponent<Abilities>();
            if (ab != null) ab.KnockBack(hit);
            
            if (ab.equippedMask == MaskTypeEquipped.Stone)
            {
                Debug.Log("Hit Opponent Has Stone Mask");

                // Remove Enemy Buffs
                tpc.MoveSpeed = 2.0f;
                tpc.JumpHeight = 1.0f;
                rb.mass = 1.0f;
                // add knockback reset

                // Add Buffs to yourself
                Debug.Log("Stolen Stone Mask");
                equippedMask = MaskTypeEquipped.Stone;
                ThirdPersonController myTpc = gameObject.GetComponent<ThirdPersonController>();
                Rigidbody myRb = gameObject.GetComponent<Rigidbody>();
                myTpc.MoveSpeed = 1.0f;
                myTpc.JumpHeight = 0.6f;
                myRb.mass = 10f;

            }
            else if (ab.equippedMask == MaskTypeEquipped.Wing)
            {
                Debug.Log("Hit Opponent Has Stone Mask");

                tpc.MoveSpeed = 2.0f;
                tpc.JumpHeight = 1.0f;
                rb.mass = 1.0f;

                Debug.Log("Stolen Wing Mask");
                equippedMask = MaskTypeEquipped.Wing;
                ThirdPersonController myTpc = gameObject.GetComponent<ThirdPersonController>();
                Rigidbody myRb = gameObject.GetComponent<Rigidbody>();
                myTpc.MoveSpeed = 4.0f;
                myTpc.JumpHeight = 3.2f;
                myRb.mass = 0.5f;
            }
            else if (ab.equippedMask == MaskTypeEquipped.Saitama)
            {
                Debug.Log("Hit Opponent Has Saitama Mask");

                tpc.MoveSpeed = 2.0f;
                tpc.JumpHeight = 1.0f;
                rb.mass = 1.0f;

                Debug.Log("Stolen Saitama Mask");
                equippedMask = MaskTypeEquipped.Saitama;
                ThirdPersonController myTpc = gameObject.GetComponent<ThirdPersonController>();
                Rigidbody myRb = gameObject.GetComponent<Rigidbody>();
                myTpc.MoveSpeed = 2.0f;
                myTpc.JumpHeight = 2.0f;
                myRb.mass = 2.0f;
            }
        }
    }

    private void KnockBack(RaycastHit hit)
    {
        var player = hit.collider.GetComponent<StarterAssets.ThirdPersonController>();
        if (player != null)
        {
            Vector3 knockbackDir = (transform.position).normalized;
            knockbackDir += Vector3.up * forceStrengthY;
            player.AddImpact(knockbackDir, forceStrengthZ);
            
            // TODO: DISABLE FOR 
        }
    }
    public void TakeDamage()
    {
        lives--;

        if (lives <= 0)
        {
        Manager.Instance.EndGame();
        }
    }
    
    
    
    // Debug for attack function
    void OnDrawGizmos()
    {
#if true
        RaycastHit hit;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Vector3 direction = transform.forward;
        bool isHit = Physics.SphereCast(origin, radius, direction, out hit, maxDistance);

        if (isHit)
        {
            Gizmos.color = Color.green;
            
            // Draw the "Travel Line" (Center of sphere path)
            Gizmos.DrawLine(origin, origin + direction * hit.distance);
            
            // Draw the sphere at the impact point
            Vector3 sphereCastMidpoint = origin + direction * hit.distance;
            Gizmos.DrawWireSphere(sphereCastMidpoint, radius);
        }
        else
        {
            Gizmos.color = Color.red;
            
            Vector3 endPosition = origin + direction * maxDistance;
            Gizmos.DrawLine(origin, endPosition);
            Gizmos.DrawWireSphere(endPosition, radius);
        }
        
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(origin, radius);
#endif
    }
}