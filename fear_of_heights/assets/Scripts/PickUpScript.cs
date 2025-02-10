using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject crosshair1, crosshair2;
    public Transform basketballTrans, cameraTrans;
    public bool interactable, pickedup;
    public Rigidbody basketballRigidBody;
    public float throwAmout;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entered OnTriggerStay");
            crosshair1.SetActive(false);
            crosshair2.SetActive(true);
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickedup == false)
            {
                crosshair1.SetActive(true);
                crosshair2.SetActive(false);
                interactable = false;
            }
            if (pickedup == true)
            {
                basketballTrans.parent = null;
                basketballRigidBody.useGravity = true;
                crosshair1.SetActive(true);
                crosshair2.SetActive(false);
                interactable = false;
                pickedup = false;
            }
        }
    }

    void Update()
    {
        if (interactable == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Aici apasam in joooos");
                basketballTrans.parent = cameraTrans;
                basketballRigidBody.useGravity = false;
                basketballRigidBody.isKinematic = true; // Set to Kinematic to move it with the player
                pickedup = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Aici apasam in suuuus");
                basketballTrans.parent = null;
                basketballRigidBody.useGravity = true;
                basketballRigidBody.isKinematic = false; // Set back to non-Kinematic before throwing
                pickedup = false;
            }

            if (pickedup == true)
            {
                Debug.Log("Object picked up");
                if (Input.GetMouseButtonDown(1))
                {
                    Debug.Log("Object thrown");

                    basketballTrans.parent = null;
                    basketballRigidBody.useGravity = true;
                    basketballRigidBody.isKinematic = false; // Ensure it's not Kinematic when throwing

                    // Apply throw force
                    basketballRigidBody.AddForce(cameraTrans.forward * throwAmout, ForceMode.VelocityChange); // Use AddForce with ForceMode.VelocityChange to apply instant velocity

                    pickedup = false;
                }
            }
        }
    }
}
