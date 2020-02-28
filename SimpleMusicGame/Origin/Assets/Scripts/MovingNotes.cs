using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingNotes : MonoBehaviour {
    public float Speed0 = 50;
    public bool Effect;
    static public float Speed;
	void Start () {

        Speed0 = StageManager.NoteSpeed;


    }
	
	// Update is called once per frame
	void Update () {
        if(!Effect)
            transform.Translate(Vector2.down * Time.deltaTime * Speed0);
        else
            if(gameObject.transform.position.y<42)
                transform.Translate(Vector2.up * Time.deltaTime * Speed0);
    }
}
