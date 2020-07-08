using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by: David Carlyn

/*
 * This class is to be inherited by any class that wishes to be an attachable script to a ScriptableObject.
 */
public abstract class CollisionHandler : MonoBehaviour
{
	/*
	 * This will be the default behavior when an object collides with an programmable object.
	 * Unless the object that collided with the programmable object has a tag handled in the method OnCollisionEnter (down below)
	 * and the inheriting class of this class overrides the appropriate method then this method will be called.
	 * All inheriting classes must implement this method (even if it is just a blank method
	 */
	protected abstract void onCollision(GameObject obj);

	/*
	 * This method will be called if the object that collided with the programmable object has a tag associated with a Player
	 */
	protected virtual void onPlayerCollision(GameObject obj)
	{
		onCollision(obj);
	}

	/*
	 * This method will be called if the object that collided with the programmable object has a tag associated with an Enemy
	 */
	protected virtual void onEnemyCollision(GameObject obj)
	{
		onCollision(obj);
	}

	/*
	 * This method will be called if the object that collided with the programmable object has a tag associated with an Environment
	 */
	protected virtual void onEnvironmentCollision(GameObject obj)
	{
		onCollision(obj);
	}

	/*
	 * This method will be called if the object that collided with the programmable object has a tag associated with a Robot
	 */
	protected virtual void onRobotCollision(GameObject obj)
	{
		onCollision(obj);
	}

	/*
	 * This method will handle what functionality will be used when colliding with an object.
	 * Add more cases here for different tags along with creating its appropriate functionality (method)
	 */
	private void OnCollisionEnter(Collision collision)
	{
		GameObject obj = collision.gameObject;
		switch(collision.gameObject.tag)
		{
			case TagManager.ENEMY:
				onEnemyCollision(obj);
				break;
			case TagManager.ENVIRONMENT:
				onEnvironmentCollision(obj);
				break;
			case TagManager.PLAYER:
				onPlayerCollision(obj);
				break;
			case TagManager.ROBOT:
				onRobotCollision(obj);
				break;
			default:
				onCollision(obj);
				break;
		}
	}
}