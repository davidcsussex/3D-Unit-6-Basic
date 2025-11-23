using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    public Camera cam;


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
        float moveSpeed = 20;
        float rotationSmoothTime = 0;

        Vector3 moveDir = Vector3.zero;


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

        rb.linearVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);
    }

    void DoFriction()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.3f, rb.linearVelocity.y, rb.linearVelocity.z * 0.3f);
    }




}

