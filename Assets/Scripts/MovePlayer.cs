using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Collision
{
	None,
	Top,
	Bottom
};

public class MovePlayer : MonoBehaviour
{
	public float velocity = 7.5f;
	public string upKey = "up";
	public string downKey = "down";
	
	private Rigidbody2D _rb;
	private Collision _col;

	private List<string> borderNames = new List<string>{"Top", "Bottom"};
	
	// Use this for initialization
	void Start ()
	{
		this._rb = GetComponent<Rigidbody2D>();
		this._col = Collision.None;
	}
	
	// Update is called once per frame
	void Update ()
	{
		int dir = 0;
		if (Input.GetKey(this.upKey))
		{
			dir = 1;
		}
		if (Input.GetKey(this.downKey))
		{
			dir = -1;
		}
		if (isValidDirection(dir))
		{
			this._rb.velocity = Vector2.up * this.velocity * dir;
		} 
	}

	private bool isValidDirection(int dir)
	{
		return !(dir == 1 && this._col == Collision.Top || dir == -1 && this._col == Collision.Bottom);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		switch (other.gameObject.name)
		{
			case "Top":
				this._col = Collision.Top;
				break;
			case "Bottom":
				this._col = Collision.Bottom;
				break;
		}
		this._rb.velocity = Vector2.zero;
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (borderNames.Contains(other.gameObject.name))
		{
			this._col = Collision.None;	
		}
	}
}
