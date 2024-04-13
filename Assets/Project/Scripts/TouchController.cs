using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    Vector2 startPosition;
    Vector2 delta;
    ProjectileEmitter emitter;
    TrajectoryPredictor predictor;


    private void Start()
    {
        Input.simulateMouseWithTouches = true;
        emitter = GetComponent<ProjectileEmitter>();
        predictor = GetComponent<TrajectoryPredictor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {            
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                startPosition = touch.position;
                predictor.isActive = true;
                emitter.Prepare();
            } 
            else if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                delta += touch.deltaPosition;
                emitter.AimHorizontal(delta.x);
                predictor.AimForce(delta.y);                
            }
            else if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                predictor.isActive = false;
                delta = Vector2.zero;
                emitter.Fire(predictor.direction * predictor.force);
            }
        }        
    }
}
