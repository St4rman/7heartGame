using UnityEngine;
using UnityEngine.UI;

public class StatDisplayer : MonoBehaviour
{
    public GameObject player;
    Rigidbody rb;
    PlayerStats pstats;
    int curspeed;

    public Text speedText;

    public Image crossheir; 
    public Image healthBarUI;
    float hp;
    float lerpSpeed;

    void Awake()
    {
        rb = player.GetComponent<Rigidbody>();
        pstats = player.GetComponent<PlayerStats>();
       
    }
    void Update()
    {
        speedText.text = pstats.currentHealth.ToString();

        HeathBarFiller();
        ColorChanger();

        lerpSpeed = 3f * Time.deltaTime;

        if(Input.GetMouseButton(1)) crossheir.enabled = false;
        else crossheir.enabled = true;

    }

    public void HeathBarFiller()
    {
        healthBarUI.fillAmount = Mathf.Lerp(healthBarUI.fillAmount, pstats.ratioHealth, lerpSpeed);
    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, pstats.ratioHealth);
        healthBarUI.color = healthColor;
    }
}
