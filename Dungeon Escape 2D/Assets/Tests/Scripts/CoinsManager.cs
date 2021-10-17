﻿using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class CoinsManager : MonoBehaviour
{
	//References
	[Header ("UI references")]
	[SerializeField] Text coinUIText;
	[SerializeField] GameObject animatedCoinPrefab;
	[SerializeField] Transform target;

	[Space]
	[Header ("Available coins : (coins to pool)")]
	[SerializeField] int maxCoins;
	Queue<GameObject> coinsQueue = new Queue<GameObject> ();


	[Space]
	[Header ("Animation settings")]
	[SerializeField] [Range (0.5f, 0.9f)] float minAnimDuration;
	[SerializeField] [Range (0.9f, 2f)] float maxAnimDuration;

	[SerializeField] Ease easeType;
	[SerializeField] float spread;

	Vector3 targetPosition;


	private int _c = 0;

	public int Coins {
		get{ return _c; }
		set {
			_c = value;
			//update UI text whenever "Coins" variable is changed
			coinUIText.text = Coins.ToString ();
		}
	}

	void Awake ()
	{
		//prepare pool
		PrepareCoins ();
	}

    void Update()
    {
		// Use an Update routine to change the tween's endValue each frame
		// so that it updates to the target's position if that changed

		//if (targetLastPos == target.position)
		//return;

		//tweens.ChangeEndValue(target.position, true);
		targetPosition = target.position;
	}

    void PrepareCoins ()
	{
		GameObject coin;
		for (int i = 0; i < maxCoins; i++) {
			coin = Instantiate (animatedCoinPrefab);
			coin.transform.parent = transform;
			coin.SetActive (false);
			coinsQueue.Enqueue (coin);
		}
	}

	public void Animate (Vector3 collectedCoinPosition, int amount)
	{
		for (int i = 0; i < amount; i++) 
		{
			//check if there's coins in the pool
			if (coinsQueue.Count > 0) {
				//extract a coin from the pool
				GameObject coin = coinsQueue.Dequeue ();
				coin.SetActive (true);

				//move coin to the collected coin pos
				coin.transform.position = collectedCoinPosition + new Vector3 (Random.Range (-spread, spread), 0f, 0f);

				//animate coin to target position
				float duration = Random.Range (minAnimDuration, maxAnimDuration);
				//coin.transform.domove
				coin.transform.DOMove(targetPosition, duration)
				.SetEase (easeType)
				.OnComplete (() => {
					//executes whenever coin reach target position
					coin.SetActive (false);
					coinsQueue.Enqueue (coin);
					
					Coins++;
				}).SetAutoKill(false);
			}
		
		}
	}

	public void AddCoins (Vector3 collectedCoinPosition, int amount)
	{
		Animate (collectedCoinPosition, amount);
	}
}
