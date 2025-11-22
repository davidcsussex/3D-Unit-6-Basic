using UnityEngine;
using System.Collections;



public class GUIScript : MonoBehaviour
{
    public string text;
    public static GUIScript gui;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (gui != null && gui != this)
        {
            Destroy(this);
        }
        else
        {
            gui = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnGUI()
    {

        GUILayout.BeginArea(new Rect(10f, 10f, 1600f, 1600f));
        GUILayout.Label($"<color=white><size=24>{text}</size></color>");
        GUILayout.EndArea();


    }

    private void LateUpdate()
    {
        //text = null;

    }
}
