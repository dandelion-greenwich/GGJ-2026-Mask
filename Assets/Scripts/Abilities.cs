using UnityEngine;

public class Abilities : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Attack")] 
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float maxDistance = 1f;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Debug.Log("Attack");
        
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Vector3 direction = transform.forward;

        if (Physics.SphereCast(origin, radius, direction, out RaycastHit hit, maxDistance))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
    }
    
    // Debug for attack function
    void OnDrawGizmos()
    {
#if false
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
