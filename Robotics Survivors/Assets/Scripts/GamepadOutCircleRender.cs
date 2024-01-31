using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class GamepadOutCircleRender : MonoBehaviour
{
    // СКРИПТ НЕ ИСПОЛЬЗУЕТСЯ !!!!!!!!!!!!!!
    // [SerializeField] private LineRenderer circleRenderer;
    // void Start()
    // {
    //     DrawCircle(1000, 2);
    // }

    // void Update()
    // {
        
    // }

    // void DrawCircle(int steps, float radius)
    // {
    //     circleRenderer.positionCount = steps;
    //     float circumferenceProgress;
    //     float currentRadian;
    //     float xScaled;
    //     float yScaled;
    //     float x;
    //     float y;
    //     Vector3 currentPosition;

    //     for(int currentStep = 0; currentStep<steps; currentStep++)
    //     {
    //         circumferenceProgress = (float)currentStep/steps;
            
    //         currentRadian = circumferenceProgress * 2 * Mathf.PI;

    //         xScaled = Mathf.Cos(currentRadian);
    //         yScaled = Mathf.Sin(currentRadian);

    //         x = xScaled * radius;
    //         y = yScaled * radius;

    //         currentPosition = new Vector3(x,y,0);

    //         circleRenderer.SetPosition(currentStep, currentPosition);
    //     }
    // }
}
