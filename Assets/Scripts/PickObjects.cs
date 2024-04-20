using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickObjects : MonoBehaviour
{
    [SerializeField] private GameObject holdPosObj;
    private GameObject holdObject;
    private float rotationAmount = 0f;
    private bool holdObjectBool = false;
    private bool putObjectBool = false;
    private GameObject rayObject;
    private bool holdingObject = false;
    private bool putLaundry = false;
    private bool focusLaptop = false;
    private GameObject laptopObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //if (holdObjectBool)
            //{
            //    holdObject = triggerObject;
            //    holdObject.transform.GetComponent<Rigidbody>().isKinematic = true;
            //    gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            //    holdObject.transform.parent = gameObject.transform.GetChild(3);
            //    holdPosObj.transform.localPosition = Vector3.zero;
            //    holdObject.transform.localEulerAngles = new Vector3(0, 90f, 0);
            //}
            //if (putObjectBool)
            //{
            //    Destroy(holdObject);
            //}
            if (focusLaptop)
            {
                if (laptopObject.GetComponent<PinScript>().enterPin == false)
                {
                    laptopObject.GetComponent<PinScript>().enterPin = true;
                }
                else
                {
                    laptopObject.GetComponent<PinScript>().enterPin = false;
                }
            }

            if (holdingObject == false)
            {
                if (rayObject != null)
                {
                    if (rayObject.tag == "fruittag" || rayObject.tag == "clothestag" || rayObject.tag == "cuptag")
                    {
                        holdObject = rayObject;
                        holdObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                        gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                        holdObject.transform.parent = holdPosObj.transform;
                        holdObject.GetComponent<BoxCollider>().enabled = false;
                        holdObject.transform.localPosition = Vector3.zero;
                        holdingObject = true;
                    }
                }
            }
            else if (holdingObject)
            {
                if (putLaundry)
                {
                    Destroy(holdObject);
                    holdingObject = false;
                }
                else
                {
                    holdObject.transform.GetComponent<Rigidbody>().isKinematic = false;
                    holdObject.GetComponent<BoxCollider>().enabled = true;
                    holdObject.transform.parent = null;
                    holdingObject = false;
                }
            }
        }

        RotateObject();

        //Pour champagne
        if (holdObject != null && holdObject.name == "Champagne")
        {
            if (rotationAmount >= 45 || rotationAmount <= -45)
            {
                holdObject.transform.GetChild(0).gameObject.SetActive(true);
                holdObject.GetComponent<ChampScript>().pourChamp = true;
            }
            else
            {
                holdObject.transform.GetChild(0).gameObject.SetActive(false);
                holdObject.GetComponent<ChampScript>().pourChamp = false;
            }
        }

        //raycast
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
        float rayLength = 5000f;
        float rayLimit = 5f;

        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.distance <= rayLimit)
            {
                rayObject = hit.transform.gameObject;
                if (rayObject.tag == "fruittag" || rayObject.tag == "clothestag" || rayObject.tag == "cuptag")
                {
                    gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                }
                if (rayObject.tag == "cantag" && holdObject != null)
                {
                    if (holdObject.tag == "clothestag")
                    {
                        gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                        putLaundry = true;
                    }
                }
                else
                {
                    gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                    putLaundry = false;
                }
                if (rayObject.tag == "laptoptag")
                {
                    focusLaptop = true;
                    laptopObject = rayObject;
                    laptopObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else if (laptopObject != null)
                {
                    focusLaptop = false;
                    laptopObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                }
            }
            else
            {
                rayObject = null;
            }
        }
    }

    private void RotateObject()
    {
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
    }
}