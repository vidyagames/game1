using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SetColliderSize : MonoBehaviour {

    private BoxCollider2D myCollider;

	// Use this for initialization
	void Awake () {
        myCollider = GetComponent<BoxCollider2D>();
	}
	
	public void SetXSize(float x)
    {
        myCollider.size = new Vector2(x, myCollider.size.y);
    }

    public void SetYSize(float y)
    {
        myCollider.size = new Vector2(myCollider.size.x, y);
    }
}
