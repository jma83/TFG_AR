using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject gm_obj;
    private Player player1;
    private Transform t;
    private bool parentPlayer = true;
    private Vector3 offset;
    private Vector3 defaultCameraPos;
    private Quaternion defaultCameraRot;
    Vector3 FirstPoint;
    Vector3 SecondPoint;
    float xAngle = 0;
    float yAngle = 0;
    float xAngleTemp;
    float yAngleTemp;
    private Transform target;
    public Vector3 targetOffset;
    public float distanceI = 0.0f;
    public float distance = 20.0f;
    public float distanceF = 0.0f;
    public float maxDistance = 35.0f;
    public float minDistance = 15.0f;
    public float xSpeed = 25.0f;
    public float ySpeed = 15.0f;
    public int yMinLimit = -80;
    public int yMaxLimit = 80;
    public float zoomRate = 10.0f;
    public float panSpeed = 2.0f;
    public float zoomDampening = 5.0f;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;
    private bool checkdistance = false;

    private Vector3 FirstPosition;
    private Vector3 SecondPosition;
    private Vector3 delta;
    private Vector3 lastOffset;
    private Vector3 lastOffsettemp;

    [SerializeField] private Text textInfo;
    //private Vector3 CameraPosition;
    //private Vector3 Targetposition;
    //private Vector3 MoveDistance;

    // Use this for initialization
    void Start()
    {
        player1 = GameManager.Instance.CurrentPlayer;
        t = player1.gameObject.transform;
        offset = transform.position - player1.transform.position;
        gm_obj.transform.position = player1.transform.position;
        defaultCameraPos = transform.localPosition;
        defaultCameraRot = transform.localRotation;
        Init();
    }
    public void Init()
    {
        //If there is no target, create a temporary target at 'distance' from the cameras current viewpoint
        if (!target)
        {
            target = player1.transform;
        }

        distance = Vector3.Distance(transform.position, target.position);
        currentDistance = distance;
        desiredDistance = distance;

        //be sure to grab the current rotations as starting points.
        position = transform.position;
        rotation = transform.rotation;
        currentRotation = transform.rotation;
        desiredRotation = transform.rotation;

        xDeg = Vector3.Angle(Vector3.right, transform.right);
        yDeg = Vector3.Angle(Vector3.up, transform.up);
    }
    // Update is called once per frame
    void LateUpdate()
    {

        /*if (parentPlayer == false)
        {
            
            //transform.LookAt(player1.gameObject.transform);
            

            if (Input.touchCount > 0)
            {
                //print("hola");
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    FirstPoint = Input.GetTouch(0).position;
                    xAngleTemp = xAngle;
                    yAngleTemp = yAngle;
                    //print("hola2");
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    SecondPoint = Input.GetTouch(0).position;
                    xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                    yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;

                    Vector3 tmp;
                    tmp.x = (Mathf.Cos(xAngle * (Mathf.PI / 180)) * Mathf.Sin(yAngle * (Mathf.PI / 180)) * offset.x + player1.transform.position.x);
                    tmp.z = (Mathf.Sin(xAngle * (Mathf.PI / 180)) * Mathf.Sin(yAngle * (Mathf.PI / 180)) * offset.z + player1.transform.position.z);
                    tmp.y = Mathf.Sin(yAngle * (Mathf.PI / 180)) * offset.y + player1.transform.position.y;
                    transform.position = Vector3.Slerp(transform.position, tmp, 1 * Time.deltaTime);
                    //print("hola3");
                }
            }
            else
            {
                gm_obj.transform.position = player1.transform.position + offset;
            }
            transform.LookAt(player1.gameObject.transform);

        }*/

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);

            Touch touchOne = Input.GetTouch(1);



            Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;

            Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;



            float prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
            float TouchDeltaMag = (touchZero.position - touchOne.position).magnitude;


            float deltaMagDiff = prevTouchDeltaMag - TouchDeltaMag;
            //deltaMagDiff = 1f;

            desiredDistance += deltaMagDiff * Time.deltaTime * zoomRate * 0.0025f * Mathf.Abs(desiredDistance);
            if (parentPlayer)
            {
                distanceI = Vector3.Distance(transform.position, target.position);
                desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
                if (distanceI > 7f || (distanceI <= 7f && currentDistance < desiredDistance))
                    checkdistance = true;
                else
                    checkdistance = false;
                currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);

                distanceF = currentDistance - minDistance;
                if (distanceF <= 0f) distanceF = 1f;
                distanceF = distanceF / 20f;
                if (distanceF < 0.0005f) distanceF = 0.0005f;
                if (checkdistance)
                    transform.localPosition = defaultCameraPos * distanceF;
            }
        }

        if (parentPlayer == false)
        {

            // If middle mouse and left alt are selected? ORBIT
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchposition = Input.GetTouch(0).deltaPosition;
                xDeg += touchposition.x * xSpeed * 0.002f;
                yDeg -= touchposition.y * ySpeed * 0.002f;
                yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);

            }
            desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
            currentRotation = transform.rotation;
            rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);



            if (Input.GetMouseButtonDown(1))
            {
                FirstPosition = Input.mousePosition;
                lastOffset = targetOffset;
            }

            if (Input.GetMouseButton(1))
            {
                SecondPosition = Input.mousePosition;
                delta = SecondPosition - FirstPosition;
                targetOffset = lastOffset + transform.right * delta.x * 0.003f + transform.up * delta.y * 0.003f;

            }

            desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
            currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);

            position = target.position - (rotation * Vector3.forward * currentDistance);

            //position = position - targetOffset;
            if (position.y <= player1.transform.position.y)
            {
                position.y = player1.transform.position.y;
                rotation = transform.rotation;
            }
            else
            {
                transform.rotation = rotation;
            }
            transform.position = position;
        }
        textInfo.text = "Desired d:" + currentDistance + " current: " + currentDistance;
    }

    public void switchParent()
    {
        if (parentPlayer)
        {
            transform.SetParent(gm_obj.transform);

            parentPlayer = false;
        }
        else
        {

            transform.SetParent(player1.gameObject.transform);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, defaultCameraRot, Time.deltaTime * zoomDampening);

            parentPlayer = true;


        }
        if (distanceF <= 0) distanceF = 1.0f;
        transform.localPosition = defaultCameraPos * distanceF;
        transform.LookAt(player1.gameObject.transform);
    }
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
