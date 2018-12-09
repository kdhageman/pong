using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
	private int left = 0;
	private int right = 0;

	private Text guiText; 
	
	// Use this for initialization
	void Start ()
	{
		this.guiText = GetComponentInChildren<Text>();
		UpdateBoard();
	}
	
	private void UpdateBoard()
	{
		this.guiText.text = String.Format("{0}-{1}", this.left, this.right);
	}
	
	public void Left()
	{
		this.left++;
		UpdateBoard();
	}

	public void Right()
	{
		this.right++;
		UpdateBoard();
	}
}
