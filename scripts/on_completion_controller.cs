using UnityEngine;

public class on_completion_controller : MonoBehaviour
{
    public void show()
    {
        gameObject.SetActive(true);
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }
}
