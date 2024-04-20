using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotateSpeed = 1f;
    private PlayerInputActions playerInputActions;
    public bool playerMovement = true;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

    }

    private void Update()
    {
        if (playerMovement)
        {
            movePlayer();
        }
    }

    private void movePlayer()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        transform.position += moveSpeed * transform.forward * inputVector.y * Time.deltaTime;
        transform.position += moveSpeed * transform.right * inputVector.x * Time.deltaTime;

        //bool isWalking = moveDir != Vector3.zero;
    }
}
