using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingButton : MonoBehaviour
{
	public float fallTime;
	public Vector2 startPoint;
	public Vector2 endPoint;
	public Vector2 startScale;
	public Music music;
	public float totalTime;
	// Start is called before the first frame update
	void Start()
	{
		totalTime = 0;
		//startScale = this.transform.localScale;
	}

	// Update is called once per frame
	void Update()
	{
		if (fallTime > 0) {
			// if this is zero, we haven't been initialized
			// update how long we've existed
			totalTime += Time.deltaTime;
			float fracDone = totalTime/fallTime;
			float fracDoneZ = Mathf.Pow(totalTime/fallTime, 2);
            this.transform.localPosition = startPoint + (endPoint - startPoint)*fracDoneZ;
            //this.transform.position = new Vector3(0, 0, 0);
            this.transform.localScale = startScale*fracDone;
			if (totalTime > fallTime + .5f) {
				// the player has missed this note
				PlayerStats.Misses += 1;
				PlayerStats.Meter_Val -= 1;
				if (PlayerStats.Meter_Val <= 0) {
					music.Lose();
				}
				Destroy(gameObject);
			} 
		}
	}
}
