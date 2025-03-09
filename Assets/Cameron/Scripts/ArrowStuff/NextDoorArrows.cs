using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class NextDoorArrows : MonoBehaviour
{
    [SerializeField]
    private Sprite aboveDoorSprite;
    [SerializeField]
    private Sprite originalArrow;
    [SerializeField]
    private GameObject doorPosition;

    private RectTransform arrowRectTransform;
    private Image pointerImage;
    

    private void Awake()
    {
        arrowRectTransform = gameObject.GetComponent<RectTransform>();
        pointerImage = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        // Setting Variables
        Vector3 toPosition = doorPosition.transform.position;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;

        //transforming the arrows angle
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
        arrowRectTransform.localEulerAngles = new Vector3(0, 0, angle);

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


            arrowRectTransform.position = cappedTargetScreenPosition;
            /*Vector3 arrowWorldPosition = Camera.main.ScreenToWorldPoint(cappedTargetScreenPosition);
            arrowRectTransform.position = arrowWorldPosition; */
            arrowRectTransform.localPosition = new Vector3(arrowRectTransform.localPosition.x, arrowRectTransform.localPosition.y, 0f);
        } else
        {
            pointerImage.sprite = aboveDoorSprite;
            arrowRectTransform.position = new Vector3(targetPositionScreenPoint.x, targetPositionScreenPoint.y + 100, targetPositionScreenPoint.z);
            arrowRectTransform.localPosition = new Vector3(arrowRectTransform.localPosition.x, arrowRectTransform.localPosition.y, 0f);

            arrowRectTransform.localEulerAngles = new Vector3(0, 0, -90);
            //arrowRectTransform.Rotate(0, 0, -90);
        }
    }

    
}
