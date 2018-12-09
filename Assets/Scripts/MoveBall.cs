using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Random = UnityEngine.Random;

public enum Player
{
	Left,
	Right,
	None
};

public class MoveBall : MonoBehaviour
{
	public float velocity = 15;
	public float minVelocity = 10;
	public float maxVelocity = 30;

	private Rigidbody2D _rb;
	private List<Action<Player>> callbacks;

	private void Awake()
	{
		this.callbacks = new List<Action<Player>>();
		this._rb = gameObject.GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		this.ResetPosition();
	}

	private void NormalizeVelocity()
	{
		float curVelocity = this._rb.velocity.magnitude;
		if (curVelocity > this.maxVelocity)
		{
			this._rb.velocity = this._rb.velocity.normalized * maxVelocity;
		}
		else if (curVelocity < this.minVelocity)
		{
			this._rb.velocity = this._rb.velocity.normalized * minVelocity;
		}
	}

	// Update is called once per frame
	void Update()
	{
		this.NormalizeVelocity();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Player player = Player.None; 
		switch (other.name)
		{
			case "Left":
				player = Player.Right;
				break;
			case "Right":
				player = Player.Left;
				break;
		}
		foreach (var callback in this.callbacks)
		{
			callback(player);
		}
	}

	public void ResetPosition()
	{
		this._rb.position = Vector2.zero;
		this._rb.velocity = Vector2.zero;
	}

	public void Kickoff()
	{
		float angle = Random.Range(80, 100);
		if (Random.Range(0, 2) >= 1)
		{
			angle += 180;
		}
		this._rb.rotation = angle;
		this._rb.AddRelativeForce(Vector2.up * this.velocity);
	}

	public void RegisterCallback(Action<Player> callback)
	{
		this.callbacks = new List<Action<Player>>();
		this.callbacks.Add(callback);
	}
}
