using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ManageGame : MonoBehaviour
{
	public Text guiText;
	public Text countdownText;
	public GameObject ball;

	private int left = 0;
	private int right = 0;
	private MoveBall _moveBall;

	private void Awake()
	{
		Assert.IsNotNull(this.guiText);
		Assert.IsNotNull(this.ball);
		this._moveBall = this.ball.GetComponent<MoveBall>();
		Assert.IsNotNull(this._moveBall);
	}

	// Use this for initialization
	void Start () {
		Action<Player> callback = new Action<Player>(this.ScorePlayer);
		this._moveBall.RegisterCallback(callback);
		StartCoroutine(this.CountDown());
	}
	
	void ScorePlayer(Player player)
	{
		switch (player)
		{
			case Player.Left:
				this.left++;
				break;
			case Player.Right:
				this.right++;
				break;
		}
		this.guiText.text = String.Format("{0} - {1}", this.left, this.right);
		StartCoroutine(this.CountDown());
	}

	IEnumerator CountDown()
	{
		this._moveBall.ResetPosition();
		for (int i = 3; i > 0; i--)
		{
			this.countdownText.text = String.Format("{0}", i);
			yield return new WaitForSeconds(1f);
		}
		this.countdownText.text = "";
		this._moveBall.Kickoff();
	}
}
