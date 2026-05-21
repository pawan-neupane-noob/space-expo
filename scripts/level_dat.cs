using UnityEngine;

public class level_dat : MonoBehaviour
{

    [SerializeField] private int level_number;
    [SerializeField] private Transform lander_spawn_point;

    public Vector3 getlanderspawnpoint()
    {
        return lander_spawn_point.position;
    }
    public int getlevelnumber()
    {
        return level_number;
    }

}
