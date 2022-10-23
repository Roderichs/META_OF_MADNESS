using UnityEngine;
using System.Collections;

public class ControladorVRCamara : MonoBehaviour
{
    public float Velocidad = 50;
    public float SencibilidadDeArrastre = 2;
    public bool RotacionDelTeclado = true;
    public bool RotacionDelMouse = true;
    public bool RotacionDeArrastre = true;

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {

    }

    void Update()
    {
        // **** Controles Del Teclado **** \\
        if (RotacionDelTeclado)
        {
            x += Input.GetAxis("Horizontal") * Velocidad * Time.deltaTime;
            y -= Input.GetAxis("Vertical") * Velocidad * Time.deltaTime;
        }
#if !UNITY_ANDROID && !UNITY_IOS || UNITY_EDITOR

        //**** Controles Del Mouse ****\\
        if (RotacionDelMouse && Input.GetMouseButton(0))
        {
            x += Input.GetAxis("Mouse X") * Velocidad * Time.deltaTime * SencibilidadDeArrastre;
            y -= Input.GetAxis("Mouse Y") * 1.5f * Velocidad * Time.deltaTime * SencibilidadDeArrastre;
        }
#endif
        // **** Controles Táctiles **** \\
        if (RotacionDeArrastre && Input.touchCount == 1)
        {
            Touch f0 = Input.GetTouch(0);
            Vector3 f0Delta2 = new Vector3(f0.deltaPosition.x, -f0.deltaPosition.y, 0);
            x += Mathf.Deg2Rad * f0Delta2.x * SencibilidadDeArrastre * 10;
            y += Mathf.Deg2Rad * f0Delta2.y * SencibilidadDeArrastre * 10;
        }

        y = ClampAngle(y, -85, 85);
        Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
        transform.rotation = rotation;
    }
    //	void OnGUI(){
    //		GUI.Box(new Rect(0,0, 128,64),"");
    //		GUILayout.Label("X = : " + x);
    //		GUILayout.Label("Y = " + y);
    //	}
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360.0f)
            angle += 360.0f;
        if (angle > 360.0f)
            angle -= 360.0f;
        return Mathf.Clamp(angle, min, max);
    }
}
