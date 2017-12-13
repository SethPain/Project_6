using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerSeek : MonoBehaviour {

    GameObject target;              // This will be the target for the seeking behavior
    Vector3 currentPosition;        // This is the seeking object's current position
    Vector3 previousPosition;       // This is the seeking object's previous position, before the last change in velocity
    Vector3 targetPosition;         // This is the current position of the target
    Vector3 velocity;               // This is the seeking object's velocity
    Vector3 desiredVel;             // This is the seeking object's desired velocity (the vector pointing towards the target)
    Vector3 seekForce;              // This is the difference between the desired velocity and current velocity (the steering force, the vector that needs to be applied to the seeking object with its current velocity in order to reach the target)
    public float Speed = 7f;        // This determines how fast the seeking object moves
    public float SteeringLimit = 0.01f; // This limits the rate at which the velocity changes


    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Perlin>().enemy;
        previousPosition = transform.position;  // Initialize the seeking object's previous position
        currentPosition = transform.position;   // Initialize the seeking object's current position

    }


    void Update()
    {
        //float minDis = Mathf.Infinity;
        //foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        //{
        //    if ((enemy.transform.position - transform.position).magnitude < minDis)
        //        target = enemy;
        //}
        if (target != null)
        {
            targetPosition = target.transform.position; // Update the target's position
            currentPosition = transform.position;       // Update the seeking object's position
            velocity = currentPosition - previousPosition;  // Calculate the seeking object's current velocity
            desiredVel = (targetPosition - currentPosition).normalized * Speed * Time.deltaTime;    // Calculate the desire velocity
            seekForce = (desiredVel - velocity);    // Calculate the steering force
            seekForce = new Vector3(seekForce.x * SteeringLimit, seekForce.y, seekForce.z * SteeringLimit); // Limit the rate at which the steering force can change the seeking object's velocity
            previousPosition = transform.position; // Prepare the seeking object's previous position for the next call to Update()
            velocity = velocity + seekForce;    // Calculate the seeking object's new velocity
                                                // transform.rotation = Quaternion.LookRotation(targetPosition); // Make the seeking object face the target?
            transform.position = (transform.position + velocity);   // Apply the velocity to the seeking object
        }
    }

    void OnTriggerEnter()
    {
        Destroy(gameObject, 1.25f);
    }
}
