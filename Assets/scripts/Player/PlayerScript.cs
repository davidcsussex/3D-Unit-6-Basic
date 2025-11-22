using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;
    float currentAngle;
    float currentAngleVelocity;
    public Vector3 moveDir;
    public float moveSpeed = 10;
    public Camera cam;
    public float rotationSmoothTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Update()
    {
        DoRun();
        DoFriction();
    }


    //debug stuff
    private void OnGUI()
    {
        string text = "";
        text += "Player xyz=" + transform.position.x + "  " + transform.position.y + "  " + transform.position.z;

        GUILayout.BeginArea(new Rect(10f, 10f, 1600f, 1600f));
        GUILayout.Label($"<color=white><size=24>{text}</size></color>");
        GUILayout.EndArea();
    }



    public void DoRun()
    {
        float targetAngle = 0;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(h, 0, v).normalized;

        if (direction.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward * 0.2f;
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
        }
        else
        {
            moveDir = Vector3.zero;
        }

        moveDir *= moveSpeed;

        rb.linearVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);
    }

    void DoFriction()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.3f, rb.linearVelocity.y, rb.linearVelocity.z * 0.3f);
    }




}

