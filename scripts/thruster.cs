using UnityEngine;
using UnityEngine.InputSystem;

public class thruster : MonoBehaviour
{
    [SerializeField] private ParticleSystem left_thruster;
    [SerializeField] private ParticleSystem right_thruster;
    [SerializeField] private ParticleSystem centre_thruster;
    private lander landerRef;
     private void Awake()
    {
        left_thruster.Stop();
        right_thruster.Stop();
        centre_thruster.Stop();
        landerRef = GetComponent<lander>();
    }
    void FixedUpdate()
    {
        if (landerRef.netfuel <= 0f)
        {
            Debug.Log("out of fuel !!!");
                left_thruster.Stop();
                right_thruster.Stop();
                centre_thruster.Stop();
            return;
        }
       if(Keyboard.current.upArrowKey.isPressed)
        {
            
                centre_thruster.Play();
        }
        
         if(Keyboard.current.leftArrowKey.isPressed)
        {
          
                right_thruster.Play();
        }
        
         if(Keyboard.current.rightArrowKey.isPressed)
        {
         
                left_thruster.Play();
        }
        if(Keyboard.current.shiftKey.isPressed)
        {
            centre_thruster.Play();
            right_thruster.Play();
            left_thruster.Play();
        }
        if(Keyboard.current.upArrowKey.isPressed == false && Keyboard.current.shiftKey.isPressed == false)
        {
                
            centre_thruster.Stop();
                   
        }
            
        if(Keyboard.current.leftArrowKey.isPressed == false && Keyboard.current.shiftKey.isPressed == false)
        {
            
            right_thruster.Stop();
        }
            
        if(Keyboard.current.rightArrowKey.isPressed == false && Keyboard.current.shiftKey.isPressed == false)
        {
            
        left_thruster.Stop();
        }

    }
}
