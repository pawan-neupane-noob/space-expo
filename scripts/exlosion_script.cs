using UnityEngine;

public class exlosion_script : MonoBehaviour
{
   [SerializeField] private ParticleSystem explosion;
   private void Awake()
   {
        explosion.Stop(); 
    }
    public void explode()
    {
        explosion.Play();
    }
}
