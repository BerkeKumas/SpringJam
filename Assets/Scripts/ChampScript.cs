using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampScript : MonoBehaviour
{
    public bool pourChamp = false;
    [SerializeField] private GameObject glass;
    [SerializeField] private float pourRiseSpeed = 1f;

    void Update()
    {
        if (pourChamp)
        {
            Vector2 pourVec = new Vector2(gameObject.transform.GetChild(0).transform.position.x, gameObject.transform.GetChild(0).transform.position.z);
            Vector2 glassVec = new Vector2(glass.transform.position.x, glass.transform.position.z);
            Debug.Log(glass.transform.GetChild(0).transform.localPosition.y);
            if (Vector2.Distance(pourVec, glassVec) <= 0.25f && glass.transform.GetChild(0).transform.localPosition.y <= 0.92f)
            {
                Debug.Log("test");
                glass.transform.GetChild(0).transform.localPosition += new Vector3(0, pourRiseSpeed * Time.deltaTime, 0);
            }
        }
    }
}
