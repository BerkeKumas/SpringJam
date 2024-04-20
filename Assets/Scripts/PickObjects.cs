using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickObjects : MonoBehaviour
{
    private GameObject triggerObject;
    private GameObject holdObject;
    private Vector3 holdVec = new Vector3 (0, 2.5f, 1.5f);
    private float rotationAmount = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            holdObject = triggerObject;
            holdObject.transform.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            holdObject.transform.parent = gameObject.transform.GetChild(3);
            holdObject.GetComponent<BoxCollider>().enabled = false;
            holdObject.transform.localPosition = holdVec;
            holdObject.transform.localEulerAngles = new Vector3(0, 90f, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && holdObject != null)
        {
            holdObject.transform.GetComponent<Rigidbody>().isKinematic = false;
            holdObject.transform.parent = null;
        }

        float rotateAngle = 90f;
        float maxRotation = 50f;

        if (Input.GetKey(KeyCode.Q))
        {
            if (rotationAmount > -maxRotation)
            {
                float rotateStep = rotateAngle * Time.deltaTime;
                rotationAmount -= rotateStep;
                rotationAmount = Mathf.Max(rotationAmount, -maxRotation);
                holdObject.transform.Rotate(-rotateStep, 0, 0);
                holdObject.transform.GetChild(0).rotation = Quaternion.identity;
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (rotationAmount < maxRotation)
            {
                float rotateStep = rotateAngle * Time.deltaTime;
                rotationAmount += rotateStep;
                rotationAmount = Mathf.Min(rotationAmount, maxRotation);
                holdObject.transform.Rotate(rotateStep, 0, 0);
                holdObject.transform.GetChild(0).rotation = Quaternion.identity;
            }
        }

        if (holdObject != null && holdObject.name == "Champagne")
        {
            if (rotationAmount >= 45 || rotationAmount <= -45)
            {
                holdObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                holdObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Champagne")
        {
            triggerObject = other.gameObject;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Champagne")
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
