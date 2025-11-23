using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //to reduce collider penetrating another, go to->
    //project settings->physics settings-> default solver iterations > 25
    //project settings->physics settings-> default solver velocity iterations > 25


    Rigidbody rb;
    public Camera cam;
    Vector3 moveDir;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Update()
    {
        DoRun();        //do movement
        DoFriction();   //apply friction to stop player sliding
    }


    //debug stuff
    private void OnGUI()
    {
        string text = "";

        //add your debug text here
        text += "Player xyz=" + transform.position.x + "  " + transform.position.y + "  " + transform.position.z;

        //draw text to screen
        GUILayout.BeginArea(new Rect(10f, 10f, 1600f, 1600f));
        GUILayout.Label($"<color=white><size=24>{text}</size></color>");
        GUILayout.EndArea();
    }



    public void DoRun()
    {
        float targetAngle = 0;
        float currentAngle=0;
        float currentAngleVelocity=0;
        float moveSpeed = 50;
        float rotationSmoothTime = 0;

        moveDir = Vector3.zero;


        //get direction from horiz/vertical input
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0, v).normalized;

        //move and rotate player relative to camera angle
        if (direction.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward * moveSpeed;
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
        }

        //***Movement method 1***
        //rb.linearVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);
        //***Movement method 1***
    }

    private void FixedUpdate()
    {
        //***Movement method 2***
        Vector3 force = new Vector3(moveDir.x, -15, moveDir.z); //add small downforce to prevent skipping down slopes
        rb.AddForce(force * 2, ForceMode.Impulse);
        //***Movement method 2***

    }

    void DoFriction()
    {
        //rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.3f, rb.linearVelocity.y, rb.linearVelocity.z * 0.3f);
    }




}

