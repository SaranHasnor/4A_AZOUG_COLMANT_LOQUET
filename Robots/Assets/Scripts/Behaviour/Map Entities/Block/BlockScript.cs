using UnityEngine;

public class BlockScript : MapEntity
{
	// The only argument for making this a runnable entity is to update their state each turn
	// Maybe we could also have blocks take actions at specific turns, but I'd like this to be a property instead

	void Start()
	{
		base.Initialize();
	}

    void OnCollisionEnter(Collision collision) {
        foreach (var contact in collision.contacts) {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > 2)
            audio.Play();
    }

    void OnCollisionStay(Collision collisionInfo) {
        foreach (var contact in collisionInfo.contacts) {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }
	
    void OnCollisionExit(Collision collisionInfo) {
        // TODO : ideally would require that properties are initialized with their related entity
        transform.parent.GetComponent<MapEntity>().Interact(EntityEvent.CollisionExit,
															collisionInfo.gameObject.GetComponent<MapEntity>());
    }
}
