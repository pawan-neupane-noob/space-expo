using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class gamemanagement : MonoBehaviour
{
    [SerializeField] static int levelnumber = 1;
    [SerializeField] private List<level_dat> levels;
    private void Start()
    {
        loadcurrentlevel();
    }

    private void loadcurrentlevel()
    {
        foreach (level_dat level in levels)
        {
            if (level.getlevelnumber() == levelnumber)
            {
               level_dat spawnedlevel = Instantiate(level, Vector3.zero, Quaternion.identity);
                lander.instance.transform.position = spawnedlevel.getlanderspawnpoint();
            }
        }
    }
    public void loadnextlevel()
    {
        levelnumber++;
        SceneManager.LoadScene(0);
    }
    public void reloadlevel()
    {
        SceneManager.LoadScene(0);
    }
}
