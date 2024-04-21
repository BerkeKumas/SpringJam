using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    public Image fillImage;
    public float totalTime = 60f;
    [SerializeField] private TextMeshProUGUI coffeeText;
    [SerializeField] private Image fadeImage;

    private float fillSpeed = 2f;
    private float currentTime = 0f;
    Color color;

    private void Awake()
    {
        color = fadeImage.color;
    }
    void Update()
    {
        if (currentTime < totalTime)
        {
            currentTime += Time.deltaTime * fillSpeed;
            fillImage.fillAmount = currentTime / totalTime;

            if (currentTime > 30)
            {
                color.a = (currentTime - 30) * 2 / totalTime;
                fadeImage.color = color;
                coffeeText.gameObject.SetActive(true);
            }
            else
            {
                color.a = 0;
                coffeeText.gameObject.SetActive(false);
            }


            if (currentTime >= 59)
            {
                Debug.Log("game over");
            }
        }
    }

    public void DecreaseFill(float amount)
    {
        currentTime = Mathf.Max(0, currentTime - amount);
        fillImage.fillAmount = currentTime / totalTime;
    }
}
