﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	private LineRenderer lineRenderer;

	public Transform laserHit;

	public LayerMask collisionMask;

	public GameObject explosionEffect;

	private float timer = 0.0f;
	public float waitTime = 1.0f;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.enabled = true;
		lineRenderer.useWorldSpace = true;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > waitTime) {
			// Fire the laser after a few seconds.
			RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.up, Mathf.Infinity, collisionMask);
			laserHit.position = hit.point;

			lineRenderer.startColor = new Color (1, 0, 0, 1);
			lineRenderer.endColor = new Color (1, 0, 0, 1);
			lineRenderer.widthMultiplier = 0.1f;

			lineRenderer.SetPosition (0, transform.position);
			lineRenderer.SetPosition (1, laserHit.position);

			// Check if the laser hits the player.
			if (hit.transform.gameObject.tag.Equals ("Player")) {
				hit.transform.gameObject.GetComponent<Player> ().ReceiveDamage ();
			}
			if (hit.transform.gameObject.tag.Equals ("Trap")) {
				Debug.Log ("Laser hit a trap!");
				Instantiate (explosionEffect, hit.transform.position, hit.transform.rotation);
				Destroy (hit.transform.gameObject);
			}
		} else {
			// Before firing the laser, show where the laser will fire.
			RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.up, Mathf.Infinity, collisionMask);
			laserHit.position = hit.point;

			lineRenderer.startColor = new Color(1,1,1,1);
			lineRenderer.endColor = new Color(1,1,1,1);
			lineRenderer.widthMultiplier = 0.02f;

			lineRenderer.SetPosition (0, transform.position);
			lineRenderer.SetPosition (1, laserHit.position);
		}
	}
}
