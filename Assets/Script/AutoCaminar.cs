using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCaminar : MonoBehaviour
{
    public GameObject VisionVR;
    public const int AnguloRecto = 90;
    public bool EstaCaminando = false;
    public float Velocidad;
    public bool CaminarCuandoPulsamos;
    public bool CaminarCuandoMiramos;
    public double AnguloDelUmbral;
    public bool CongelarLaPosicionY;
    public float CompensarY;

    // Update is called once per frame
    void Update()
    {
        if (CaminarCuandoMiramos && !CaminarCuandoPulsamos && !EstaCaminando && VisionVR.transform.eulerAngles.x >= 
            AnguloDelUmbral && VisionVR.transform.eulerAngles.x <= AnguloRecto)
        {
            EstaCaminando = true;
        }
        else if (CaminarCuandoMiramos && !CaminarCuandoPulsamos && EstaCaminando && (VisionVR.transform.eulerAngles.x <=
            AnguloDelUmbral || VisionVR.transform.eulerAngles.x >= AnguloRecto))
        {
            EstaCaminando = false;
        }
        if (EstaCaminando)
        {
            Caminar();
        }
        if (CongelarLaPosicionY)
        {
            transform.position = new Vector3(transform.position.x, CompensarY, transform.position.z);
        }
    }

    public void Caminar()
    {
        Vector3 Direccion = new Vector3(VisionVR.transform.forward.x, 0, VisionVR.transform.forward.z).normalized * Velocidad * Time.deltaTime;
        Quaternion Rotacion = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        transform.Translate(Rotacion * Direccion);
    }

}
