using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject NoSword = null;
    [SerializeField] private GameObject WithSword = null;

    [SerializeField] private float mouseSensitive = 1;

    bool freezeY;
    float y;
    private void Start() {if (mouseSensitive == 0) mouseSensitive = 1;}
    

    void Update()
    {
        //Con esto hago que el follow target siga al pj sin ser su hijo
        transform.position = player.transform.position;
        if (freezeY)
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        transform.position += new Vector3(0, 0.5f, 0);
        MouseMove();
    }

    private void MouseMove()
    {
        //Rotate the Follow Target transform based on the input
        //transform.eulerAngles = new Vector3(15, 65, 20);
        transform.rotation *= Quaternion.AngleAxis(Input.GetAxisRaw("Mouse X") * mouseSensitive, Vector3.up);
        transform.rotation *= Quaternion.AngleAxis(Input.GetAxisRaw("Mouse Y") * -mouseSensitive, Vector3.right);

        var angles = transform.localEulerAngles;
            angles.z = 0;

        var angle = transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 330)
            angles.x = 330;
        else if (angle < 180 && angle > 50)
            angles.x = 50;

        transform.eulerAngles = angles;
    }

    public void SwitchCamera(bool nya)
    {
        if (nya)
        {
            NoSword.SetActive(false);
            WithSword.SetActive(true);
        }
        else
        {
            NoSword.SetActive(true);
            WithSword.SetActive(false);
        }
    }
    public void SetSensitive(float nya) => mouseSensitive = nya;

    public void NoJump(float nya, bool myan)
    {
        y = nya;
        freezeY = myan;
    }
}
