using UnityEngine;
using System.Collections;
/*
This script is the Character Movement script, we have this for each character, either an NPC/Player or Enemy, his movement is controlled by another script.
This is a pretty flexible script and you can use it for almost any type of game you want. 
Although take note that you might not want hundreds of characters moving with physics to save on performance.

The basic idea behind it is:
We want to move the character with forces (rigidbody), we calculate the amount of force and direction we want to apply to the rigidbody
with the real world formula for calculating speed, more info below

*/
public class CharMove : MonoBehaviour
{

    float moveSpeedMultiplier = 1;
    float stationaryTurnSpeed = 180; //if the character is not moving, how fast he will turn
    float movingTurnSpeed = 360;//same as the above but for when the character is moving

    public bool onGround; //if true the character is on the ground

    Animator animator;

    Vector3 moveInput; //The move vector
    float turnAmount;  //the calculated turn amount to pass to mecanim
    float forwardAmount; //the calculated forwardAmount to pass to mecanim
    Vector3 velocity; //the 3d velocity of the character
    float jumpPower = 10;
   
    IComparer rayHitComparer;
    
	Rigidbody rigidBody; //Since we don't have the shortcuts from Unity 4 anymore, we store the rigidbody reference and use that

  
   
  /*  void GroundCheck()
    {
		//Checks if the character is on the ground or airborne
		
        Ray ray = new Ray(transform.position + Vector3.up * .1f, -Vector3.up);

        RaycastHit[] hits = Physics.RaycastAll(ray, .1f); //perform a raycast using that ray for a distance of 0.1 (or .5)
        rayHitComparer = new RayHitComparer();
        System.Array.Sort(hits, rayHitComparer); //sort the hits using our comparer (based on distance)

        if (velocity.y < jumpPower * .5f)
        { 	
			//if the character is not airborne due to a jump (we currently don't have a jump functionality)
			
			//assume that the character is on the air and falling	
            onGround = false;
            rigidBody.useGravity = true;

            foreach (var hit in hits)//for each of the hits			
            { // check whether we hit a non-trigger collider (and not the character itself)
			
                if (!hit.collider.isTrigger)
                {
					// this counts as being on ground.
					
					// stick to surface - helps character stick to ground - specially when running down slopes
					//if the character is falling and is close to the ground, we assume that he goes down a slope	
                    if (velocity.y <= 0)
                    {
                          rigidBody.position = Vector3.MoveTowards(rigidBody.position, hit.point, Time.deltaTime * 100);
						  //change the rigid body potition to the hit point
                    }

                    onGround = true; //set the on ground variable since we found our collider
                    rigidBody.useGravity = false;  //disable gravity since we use the above to stick the character to the ground
					
					//might need this, might not
					break; //ignore the rest of the hits
                }
            }
        }
    }

	//Compares two raycasts based on their distance
    class RayHitComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
			//this returns < 0 if x < y
			// > 0 if x > y
			// 0 if x = y
        }
    }
    */
}