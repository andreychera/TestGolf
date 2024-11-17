using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BowllingBall : MonoBehaviour
{

    public Collider[] Balls;
    public Collider MainCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool DoObjectsCollide(){
        foreach (Collider Ball in Balls){
            if (Ball.bounds.Intersects(MainCollider.bounds) && Ball != MainCollider){
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (DoObjectsCollide()){
            UnityEngine.Debug.Log("Collided");
        }
    }
}
