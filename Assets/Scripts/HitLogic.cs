using UnityEngine;
using System.Collections;

public class HitLogic : MonoBehaviour {

    public GameObject smokePrefab;
    public ParticleSystem part;
    public ParticleCollisionEvent[] collisionEvents;
    
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new ParticleCollisionEvent[16];
    }
    void OnParticleCollision(GameObject other)
    {
        int safeLength = part.GetSafeCollisionEventSize();
        if (collisionEvents.Length < safeLength)
            collisionEvents = new ParticleCollisionEvent[safeLength];

        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;
        while (i < numCollisionEvents)
        {
            Vector3 pos = collisionEvents[i].intersection;

            if (rb)
            {
                
                Vector3 force = collisionEvents[i].velocity * 10;
                rb.AddForce(force);
            }

            Quaternion rot = Quaternion.LookRotation(transform.position - pos);

            Instantiate(smokePrefab, pos, rot);

            i++;
        }

       
    }
}
