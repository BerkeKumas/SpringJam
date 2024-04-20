using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReadPin : MonoBehaviour
{


    private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[] {
        KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
        KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9
    };

    private char[] pinCode = {'1', '2', '3', '4'};
    private char[] enteredPinColors = {'0', '0', '0', '0'};
    private string enteredPin = "";
    private Tile[] tiles;
    public bool pinMode = true;
    [SerializeField] private GameObject[] Tiles;

    private void Awake()
    {
        tiles = GetComponentsInChildren<Tile>();
    }

    private int columnIndex = 0;

    void Update()
    {
        if (pinMode)
        {
            if (enteredPin.Length == 4)
            {
                for (int i = 0; i < enteredPinColors.Length; i++)
                {
                    switch (enteredPinColors[i])
                    {
                        case '1':
                            Tiles[i].GetComponent<Image>().color = Color.green;
                            break;
                        case '2':
                            Tiles[i].GetComponent<Image>().color = Color.yellow;
                            break;
                        case '3':
                            Tiles[i].GetComponent<Image>().color = Color.red;
                            break;
                        default:
                            break;
                    }
                }
            }
            if (enteredPin.Length <= 3)
            {
                for (int i = 0; i < SUPPORTED_KEYS.Length; i++)
                {
                    if (Input.GetKeyDown(SUPPORTED_KEYS[i]))
                    {
                        tiles[columnIndex].SetLetter((char)SUPPORTED_KEYS[i]);
                        enteredPin += (char)SUPPORTED_KEYS[i];
                        // 1 correct place of number
                        // 2 numbers exist at another place
                        // 3 number doesnt exist
                        bool numberExist = false;
                        foreach (char num in pinCode)
                        {
                            if (num == (char)SUPPORTED_KEYS[i])
                            {
                                numberExist = true;
                            }
                        }
                        if (pinCode[columnIndex] == (char)SUPPORTED_KEYS[i])
                        {
                            enteredPinColors[columnIndex] = '1';
                        }
                        else if (numberExist)
                        {
                            enteredPinColors[columnIndex] = '2';
                        }
                        else
                        {
                            enteredPinColors[columnIndex] = '3';
                        }
                        Debug.Log(enteredPinColors[0].ToString() + enteredPinColors[1].ToString() + enteredPinColors[2].ToString() + enteredPinColors[3].ToString());
                        columnIndex++;
                        break;
                    }
                }
            }
            if (enteredPin.Length >= 1)
            {
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    columnIndex--;
                    enteredPinColors[columnIndex] = '0';
                    tiles[columnIndex].SetLetter((char)KeyCode.Space);
                    enteredPin = enteredPin.Remove(enteredPin.Length - 1);
                }
            }
        }
    }
}
