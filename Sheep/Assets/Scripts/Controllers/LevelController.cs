﻿using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
	public int Id = 0;
	public float Length = 60;
	public float SpawnWolvesAt = 30;

	public GameObject[] Sheeps;
	public GameObject[] Wolves;
	private int aliveSheeps = 0;
	private int aliveWolves = 0;
	public int Score = 0;

	public GUIController _GUIController;
	public GameController _GameController;

	void Awake()
	{
		_GameController = FindGameController();
	}

	void Start()
	{
		aliveSheeps = Sheeps.Length;
		aliveWolves = Wolves.Length;
		Time.timeScale = 1;
	}

	void Update()
	{
		if(Length <= 0 || aliveSheeps == 0)
		{
			Time.timeScale = 0;
			EndRound();
		}

		if(Length <= SpawnWolvesAt)
		{
			SpawnWolves();
			SpawnWolvesAt = -1;
		}

		Length -= Time.deltaTime;
		//Debug.Log((int)Length);
	}

    public void AddScore(int value)
    {
		Score += value;
    }

    public void AnimalDied(Animals animal)
    {
        switch(animal)
		{
			case Animals.Wolf:
			{
				--aliveWolves;
				break;
			}
			case Animals.Sheep:
			{
				--aliveSheeps;
				break;
			}
		}
    }

	public GameController FindGameController()
	{
		GameObject gameControllerObject = GameObject.Find("GameController");
		if(gameControllerObject != null)
		{
			return gameControllerObject.GetComponent<GameController>();
		}else
		{
			gameControllerObject = new GameObject();
			gameControllerObject.name = "GameController";
			gameControllerObject.AddComponent<GameController>();
			//Instantiate(gameControllerObject);

			return gameControllerObject.GetComponent<GameController>();
		}
	}

	private void EndRound()
	{
		_GUIController.EndRound(aliveSheeps);
	}

	private void SpawnWolves()
	{
		foreach (var wolf in Wolves)
		{
			wolf.SetActive(true);
		}
	}
}
