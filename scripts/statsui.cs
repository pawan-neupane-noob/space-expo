using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class statsui : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI stats;
    [SerializeField] private GameObject uparrow;
    [SerializeField] private GameObject leftarrow;
    [SerializeField] private GameObject rightarrow;
    [SerializeField] private GameObject downarrow;
    [SerializeField] private lander lander;
    [SerializeField] private Image fuelbar;
    private void Start()
    {
        uparrow.SetActive(false);
        leftarrow.SetActive(false);
        rightarrow.SetActive(false);
        downarrow.SetActive(false);
        gameObject.SetActive(false);
    }
        private void Update()
        {
            updatestats();
        }
    private void updatestats()
    {
        if (lander.xvelocity() >0.1f)
        {
            rightarrow.SetActive(true);
            leftarrow.SetActive(false);
        }
        else if (lander.xvelocity() < 0.1f)
        {
            rightarrow.SetActive(false);
            leftarrow.SetActive(true);
        }
        else
        {
            rightarrow.SetActive(false);
            leftarrow.SetActive(false);
        }
        if (lander.yvelocity() > 0.1f)
        {
            uparrow.SetActive(true);
            downarrow.SetActive(false);
        }
        else if (lander.yvelocity() < 0.1f)
        {
            uparrow.SetActive(false);
            downarrow.SetActive(true);
        }
        else
        {
            uparrow.SetActive(false);
            downarrow.SetActive(false);
        }
        fuelbar.fillAmount = lander.Netfuel() / 10;
        stats.text =  lander.Netfuel().ToString("F2") + "\n" +
                     lander.coin().ToString() + "\n" +
                     lander.getscore().ToString() + "\n" +
                     lander.xvelocity().ToString("F2") + "\n" +
                     lander.yvelocity().ToString("F2") + "\n" +
                     lander.gettime().ToString();
}
        public void hide()
        {
            gameObject.SetActive(false);
        }
        public void show()
        {
            gameObject.SetActive(true);
        }
}
