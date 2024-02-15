using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShrink : MonoBehaviour
{
    internal float startScale = 1f;
    //Value for object scale to be edited by at start

    public float scaledecrease = 0.1f;
    //How fast it loses scale
    private float scale;
    //tracks object scale

void Start(){
    scale = transform.localScale.x * startScale;
    //Decrease object scale through value given by the script that spawned it 
}

    void Update()
    {
        scale -= scaledecrease * Time.deltaTime;
        //Decrease scale value over time
        transform.localScale = new Vector3(scale,scale,scale);
        //Changes scale

        if(scale <= 0f){
            Destroy(gameObject);
            //Destroys object once it can no longer be seen
        }
    }
}
