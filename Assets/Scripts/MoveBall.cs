﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
	public float velocity = 15;
	public float minVelocity = 10;
	public float maxVelocity = 30;
	public Canvas canvas;
	
	private Rigidbody2D _rb;
	private Scoreboard _sb;
	
	// Use this for initialization
	void Start ()
	{
		this._rb = gameObject.GetComponent<Rigidbody2D>();
		this._sb = canvas.GetComponent<Scoreboard>();
		this.RandomDirection();
	}

	private void NormalizeVelocity()
	{
		float curVelocity = this._rb.velocity.magnitude;
		if (curVelocity > this.maxVelocity)
		{
			this._rb.velocity = this._rb.velocity.normalized * maxVelocity;
		} else if (curVelocity < this.minVelocity)
		{
			this._rb.velocity = this._rb.velocity.normalized * minVelocity;
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.NormalizeVelocity();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.name)
		{
			case "Left":
				this._sb.Right();
				break;
			case "Right":
				this._sb.Left();
				break;
		}
		this.ResetPosition();
		this.RandomDirection();
	}

	void ResetPosition()
	{
		this._rb.position = Vector2.zero;
	}

	void RandomDirection()
	{
		this._rb.velocity = Vector2.left * this.velocity;
	}
}