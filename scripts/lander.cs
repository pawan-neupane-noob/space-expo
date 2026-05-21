using UnityEngine;
using UnityEngine.InputSystem;
public class lander : MonoBehaviour
{
    [SerializeField] private statsui statsui;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private exlosion_script explosion_script;
    [SerializeField] private level_dat level_dat;
    public static lander instance{get; private set;}
    public float forcefactor = 100f;
    public float rotationfactor = 100f;
    public float vel_threshold = 3.5f;
    public float ang_threshold = 20f;
    private int total_score = 0;
    private float time = 0f;
     private Rigidbody2D rb;
     private bool isstarted;
     private bool islanded = false;
     private bool isgameover ;
     private string LandedAt;
     private void Start()
     {
        isgameover = false;
        isstarted = false;
     }
    private void Awake()
        {
            instance = this;
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            statsui.hide();
        }

    private void Update ()
    {
        
        if (!isgameover && isstarted)
        {
            time += Time.deltaTime;
        }
    }   
    private void FixedUpdate()
    {
        if (isgameover)
        {
            return;
        }
        if ( netfuel <= 0f)
        {
            Debug.Log("out of fuel !!!");
            return;
        }
       if (Keyboard.current.upArrowKey.isPressed)
        {
            rb.AddForce(transform.up *forcefactor*Time.deltaTime);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            rb.AddTorque(+rotationfactor*Time.deltaTime);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            rb.AddTorque(-rotationfactor*Time.deltaTime);
        }
        if(Keyboard.current.shiftKey.isPressed)
        {
            rb.AddForce(transform.up *forcefactor*Time.deltaTime*2);
        }

        // for fuel consumption
            if(Keyboard.current.upArrowKey.isPressed || Keyboard.current.leftArrowKey.isPressed || Keyboard.current.rightArrowKey.isPressed )
            {
                consumefuel(1);
                rb.gravityScale = 0.7f;
                statsui.show();
                isstarted = true;
            }
            if (Keyboard.current.shiftKey.isPressed)
            {
                consumefuel(2);
                rb.gravityScale = 0.7f;
                statsui.show();
                isstarted = true;
            }
    }
    private void OnCollisionEnter2D(Collision2D collision2d)
    {
        isgameover = true;
        float landingangle_cos= Vector2.Dot( Vector2.up ,transform.up);
        float relvel = collision2d.relativeVelocity.magnitude;

        if(! collision2d.gameObject.TryGetComponent(out landiningpad Landingpad))
        {  
            LandedAt = "terrain";
            // Debug.Log("landed on terrain !!!");
            Onunsuccessfullanding();
            return;
        }  
        else if (relvel > vel_threshold)
       {
            LandedAt = "High velocity";
            //Debug.Log($"hard landing !!! {relvel}");
            Onunsuccessfullanding();
            return;  
       }     
        else if (landingangle_cos< Mathf.Cos(ang_threshold * Mathf.Deg2Rad))
       {
            LandedAt = "Steep Angle";
            //Debug.Log($"unstable landing !!! {landingangle_cos}");
            Onunsuccessfullanding();
            return;
       }
        Debug.Log("landing successful");
        islanded = true;
        float maxanglescore = 100f;
        float maxvelscore = 100f;
        float velscore = (vel_threshold - relvel)/(vel_threshold -1 ) * maxvelscore;
        if ( velscore > maxvelscore) 
        {
            velscore = maxvelscore;
        }
        float anglescore = (1 -( 1 - landingangle_cos)/(1-Mathf.Cos(ang_threshold * Mathf.Deg2Rad)))* maxanglescore;
        total_score = Mathf.RoundToInt(((anglescore + velscore)/2) * Landingpad.Getmultiplier()); // Update the class-level variable
        Debug.Log($"total score updated: {total_score}");
    }

    // fuel mechanism
    public float netfuel = 10f;
    public int coincollected = 0;
    private float fuelconsumptionrate = 1f;
    // coin collection and fuel collection mechanism
    private void OnTriggerEnter2D(Collider2D collision2d)
    {
        if (collision2d.gameObject.TryGetComponent(out fuel fuel))
        {
            netfuel = 10f;
            fuel.destroy();
        }
        if(collision2d.gameObject.TryGetComponent(out coin coin))
        {
            coin.collect();
            coincollected ++;
            Debug.Log($"coin collected : {coincollected}");
        }
    }
    // fuel consumption mechanism
    private void consumefuel(float amount)
    {
        netfuel -= amount * fuelconsumptionrate * Time.deltaTime;
        Debug.Log($"fuel left : {netfuel:F2}");
    }
    private void Onunsuccessfullanding()
    {
      explosion_script.explode();
      islanded = false;
      sr.enabled = false;
    }

    // for stats ui
    public float xvelocity()
    {
            return rb.linearVelocity.x;
    }
    public float yvelocity()
    {
        
            return rb.linearVelocity.y;
    }
    public float Netfuel()
    {
        return netfuel;
    }
    public float coin()
    {
        return coincollected;
    }
    public int getscore()
    {
        return total_score;
    }
    public float gettime()
    {
        return (float)Mathf.Round(time * 100f) / 100f;
    }
    public bool landed()
    {
        return islanded;
    }
    public bool getgameover()
    {
        return isgameover;
    }
    public string getLandedAt()
    {
        return LandedAt;
    }
    // matter related to levels
    private void InitializeLevel()
    {
        transform.position = level_dat.getlanderspawnpoint();
    }
}