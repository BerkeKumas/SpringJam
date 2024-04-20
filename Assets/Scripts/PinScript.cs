using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScript : MonoBehaviour
{

    private bool pinBool = false;
    private bool pinModeBool = false;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject pinCamera;
    [SerializeField] private GameObject player;
    private Player playerScript;

    private void Awake()
    {
        playerScript = player.GetComponent<Player>();
    }

    void Update()
    {
        if (pinBool)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (pinModeBool == false)
                {
                    playerScript.playerMovement = false;
                    pinModeBool = true;
                    gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                    pinCamera.SetActive(true);
                    mainCamera.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pinModeBool == true)
                {
                    playerScript.playerMovement = true;
                    pinModeBool = false;
                    gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    mainCamera.SetActive(true);
                    pinCamera.SetActive(false);
                }
            }
        }
    }

    private void EnterPinMode()
    {
        gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }

    public void ReadPinCode(string Code)
    {
        Debug.Log(Code);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            pinBool = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            pinBool = false;
        }
    }
}
