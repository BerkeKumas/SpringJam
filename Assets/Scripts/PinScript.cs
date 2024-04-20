using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScript : MonoBehaviour
{

    public bool enterPin = false;
    private bool pinModeBool = false;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject pinCamera;
    [SerializeField] private GameObject player;
    private Player playerScript;
    [SerializeField] private GameObject pinBoard;

    private void Awake()
    {
        playerScript = player.GetComponent<Player>();
    }

    void Update()
    {

        if (enterPin)
        {
            playerScript.playerMovement = false;
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            pinCamera.SetActive(true);
            mainCamera.SetActive(false);
            pinBoard.GetComponent<ReadPin>().pinMode = true;
        }
        else
        {
            playerScript.playerMovement = true;
            pinModeBool = false;
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            mainCamera.SetActive(true);
            pinCamera.SetActive(false);
            pinBoard.GetComponent<ReadPin>().pinMode = false;
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

}
