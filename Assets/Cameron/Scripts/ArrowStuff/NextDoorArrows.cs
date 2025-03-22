using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class NextDoorArrows : MonoBehaviour
{
    [SerializeField]
    private Sprite aboveDoorSprite;
    [SerializeField]
    private Sprite originalArrow;
    private GameObject doorPositionL;
    private GameObject doorPositionR;

    private RectTransform arrowRectTransformR;
    private RectTransform arrowRectTransformL;
    private Image pointerImage;
    

    private void Awake()
    {
        GameObject ArrowL = GameObject.FindGameObjectWithTag("LeftArrow");

        if (ArrowL != null)
        {
            arrowRectTransformL = ArrowL.GetComponent<RectTransform>();
        }

        GameObject ArrowR = GameObject.FindGameObjectWithTag("RightArrow");

        if (ArrowL != null)
        {
            arrowRectTransformR = ArrowR.GetComponent<RectTransform>();
        }

        pointerImage = gameObject.GetComponent<Image>();

        doorPositionL = GameObject.FindGameObjectWithTag("ArrowPointL");
        doorPositionR = GameObject.FindGameObjectWithTag("ArrowPointR");
    }

    void Update()
    {
        doorPositionL = GameObject.FindGameObjectWithTag("ArrowPointL");
        doorPositionR = GameObject.FindGameObjectWithTag("ArrowPointR");
        if (arrowRectTransformL != null && doorPositionL != null)
        {
            Debug.Log("Pointing Left");
            pointerImage.color = new Color(pointerImage.color.r, pointerImage.color.g, pointerImage.color.b, 0.2705882f);
            DoorLeft();
            
        }
        else if (gameObject.CompareTag("LeftArrow"))
        {
            pointerImage.color = new Color(pointerImage.color.r, pointerImage.color.g, pointerImage.color.b, 0f);
            Debug.Log("Pointing Left off");
        }



        if (arrowRectTransformR != null && doorPositionR != null)
        {
            pointerImage.color = new Color(pointerImage.color.r, pointerImage.color.g, pointerImage.color.b, 0.2705882f);
            DoorRight();
            Debug.Log("Pointing Right");
        }
        else if (gameObject.CompareTag("RightArrow"))
        {
            pointerImage.color = new Color(pointerImage.color.r, pointerImage.color.g, pointerImage.color.b, 0f);
        }


        //debug stuff 
        if (arrowRectTransformL == null)
        {
            Debug.LogError("arrowRectTransformL Null");
        }
        if (arrowRectTransformR == null)
        {
            Debug.LogError("arrowRectTransformR null");
        }
        if (doorPositionL == null)
        {
            Debug.LogError("doorPositionL null");
        }
        if (doorPositionR == null)
        {
            Debug.LogError("doorPositionR null");
        }
    }



    private void DoorLeft()
    {
        // Setting Variables
        Vector3 toPosition = doorPositionL.transform.position;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;

        //transforming the arrows angle
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
        arrowRectTransformL.localEulerAngles = new Vector3(0, 0, angle);

        //Screen Boarder
        float boarderSize = 100f;

        //arrowPoint
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(toPosition);

        bool isOffScreen = targetPositionScreenPoint.x <= boarderSize || targetPositionScreenPoint.x >= Screen.width - boarderSize || targetPositionScreenPoint.y <= boarderSize || targetPositionScreenPoint.y >= Screen.height - boarderSize;


        // check if off screen
        // Debug.Log(isOffScreen + " " + targetPositionScreenPoint);
        if (isOffScreen)
        {
            pointerImage.sprite = originalArrow;
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= boarderSize) cappedTargetScreenPosition.x = boarderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - boarderSize) cappedTargetScreenPosition.x = Screen.width - boarderSize;
            if (cappedTargetScreenPosition.y <= boarderSize) cappedTargetScreenPosition.y = boarderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - boarderSize) cappedTargetScreenPosition.y = Screen.height - boarderSize;


            arrowRectTransformL.position = cappedTargetScreenPosition;
            /*Vector3 arrowWorldPosition = Camera.main.ScreenToWorldPoint(cappedTargetScreenPosition);
            arrowRectTransform.position = arrowWorldPosition; */
            arrowRectTransformL.localPosition = new Vector3(arrowRectTransformL.localPosition.x, arrowRectTransformL.localPosition.y, 0f);
        }
        else
        {
            pointerImage.sprite = aboveDoorSprite;
            arrowRectTransformL.position = new Vector3(targetPositionScreenPoint.x, targetPositionScreenPoint.y + 100, targetPositionScreenPoint.z);
            arrowRectTransformL.localPosition = new Vector3(arrowRectTransformL.localPosition.x, arrowRectTransformL.localPosition.y, 0f);

            arrowRectTransformL.localEulerAngles = new Vector3(0, 0, -90);
            //arrowRectTransform.Rotate(0, 0, -90);
        }
    }

    private void DoorRight()
    {
        // Setting Variables
        Vector3 toPosition = doorPositionR.transform.position;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;

        //transforming the arrows angle
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
        arrowRectTransformR.localEulerAngles = new Vector3(0, 0, angle);

        //Screen Boarder
        float boarderSize = 100f;

        //arrowPoint
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(toPosition);

        bool isOffScreen = targetPositionScreenPoint.x <= boarderSize || targetPositionScreenPoint.x >= Screen.width - boarderSize || targetPositionScreenPoint.y <= boarderSize || targetPositionScreenPoint.y >= Screen.height - boarderSize;


        // check if off screen
        // Debug.Log(isOffScreen + " " + targetPositionScreenPoint);
        if (isOffScreen)
        {
            pointerImage.sprite = originalArrow;
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= boarderSize) cappedTargetScreenPosition.x = boarderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - boarderSize) cappedTargetScreenPosition.x = Screen.width - boarderSize;
            if (cappedTargetScreenPosition.y <= boarderSize) cappedTargetScreenPosition.y = boarderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - boarderSize) cappedTargetScreenPosition.y = Screen.height - boarderSize;


            arrowRectTransformR.position = cappedTargetScreenPosition;
            /*Vector3 arrowWorldPosition = Camera.main.ScreenToWorldPoint(cappedTargetScreenPosition);
            arrowRectTransform.position = arrowWorldPosition; */
            arrowRectTransformR.localPosition = new Vector3(arrowRectTransformR.localPosition.x, arrowRectTransformR.localPosition.y, 0f);
        }
        else
        {
            pointerImage.sprite = aboveDoorSprite;
            arrowRectTransformR.position = new Vector3(targetPositionScreenPoint.x, targetPositionScreenPoint.y + 100, targetPositionScreenPoint.z);
            arrowRectTransformR.localPosition = new Vector3(arrowRectTransformR.localPosition.x, arrowRectTransformR.localPosition.y, 0f);

            arrowRectTransformR.localEulerAngles = new Vector3(0, 0, -90);
            //arrowRectTransform.Rotate(0, 0, -90);
        }
    }

}
