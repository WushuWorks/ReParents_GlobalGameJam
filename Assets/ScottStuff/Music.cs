using UnityEngine;
using UnityEngine.SceneManagement;

// The Audio Source component has an AudioClip option.  The audio
// played in this example comes from AudioClip and is called audioData.

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
	AudioSource audioData;

	// num tracks is variable to save/simplify code space
	// just keep it at 4
	public int numTracks;
	// each of these arrays should be of size 4
	public int[] beatsDropped;
	// this "beats" array holds each tap track
	// hopefully I've filled this in, it is hard to do
	public int[,] beats;
	// each falling beat should look different
	public GameObject[] beatPrefabs;
	// this positions the start of each falling beat at the vanishing point
	public GameObject startButtonFallPoint;
	// this allows beats to fall into place to be tapped
	public GameObject[] endButtonFallPoints;
	// tracks combine notes of a certain color so they can be ordered and we can check if they're correctly tapped
	public GameObject[] tracks;
	public GameObject[] lightUpGraphics;
	public int BeatsToLoseDifficulty;

	void Start()
	{
		PlayerStats.Hits = 0;
		PlayerStats.Misses = 0;
		PlayerStats.Meter_Max = BeatsToLoseDifficulty; // this is basically how many notes you can miss in a row before losing
		PlayerStats.Meter_Val = (int)(PlayerStats.Meter_Max * .5f);
		PlayerStats.Seconds_Survived = 0;
		audioData = GetComponent<AudioSource>();
		PlayerStats.Points = (int)Mathf.Floor(audioData.clip.length);
		audioData.Play(0);
		// I was messing around with the time, in order not to fail, ignore all previous notes
		//audioData.time = 0*60 + 20; // 1st change
		//audioData.time = 1*60; // 1st change
		//audioData.time = 2*60 + 35; // end
		//audioData.time = 1*60; // 1st change
		//audioData.time = 1*60 + 19; // 1st yay
		//audioData.time = 1*60 + 58; // 2nd yay
		//audioData.time = 2*60 + 10; // 2nd solo
		audioData.time = 0;
		for (int i = 0; i < 4; ++i)
		{
			beatsDropped[i] = (int)GetFloatBeats(audioData.time);
		}
		// 0 is a rest, 1 is a beat, this is quantized into 16th notes
		// it's an int instead of a bool just incase you want to have '2' be a slide note or something
		// the song is about 2 minutes 45 seconds long
		// 2*60+45 = 165 seconds
		// 165/60 = 2.75 minutes
		// 2.75*100 = 275 beats
		// 275*4 = 1100 16th notes
		// so our track lengths are going to be 1100 indices
		int trackLength = 1100;
		beats = new int[4, trackLength];
		// go through and set all the values to zero
		for (int i = 0; i < 4; ++i)
		{
			for (int j = 0; j < trackLength; ++j)
			{
				beats[i, j] = 0;
			}
		}
		// red intro
		for (int i = 0; i < 144; ++i)
		{
			if (i % 4 == 0)
			{
				beats[0, i] = 1;
			}
		}
		// later, the snare will hit every other quarter note (off the beat)
		// I calculated these numbers by going into the program, Audacity and grabbing the seconds at certain times
		// then, I used similar calculations to the track length calculation above to find how many beats in we are at these times
		for (int i = 144; i < 250; ++i)
		{
			if (i % 4 == 0)
			{
				beats[0, i] = 1;
			}
			if (i % 4 == 2)
			{
				beats[2, i] = 1;
			}
		}
		for (int i = 250; i < 400; ++i)
		{
			if (i % 4 == 0)
			{
				if (i % 32 < 16)
					beats[0, i] = 1;
				else
					beats[1, i] = 1;
			}
			if (i % 4 == 2)
			{
				beats[2, i] = 1;
			}
		}
		//int thing = 274;
		int yayPoint = 530;
		for (int i = 400; i < yayPoint - 4; ++i)
		{
			if (i % 4 == 0)
			{
				if (i % 32 < 16)
					beats[1, i] = 1;
				else
					beats[0, i] = 1;
			}
			if (i % 4 == 2)
			{
				beats[3, i] = 1;
			}
		}
		for (int i = yayPoint; i < yayPoint + 4; ++i)
		{
			beats[i - yayPoint, i] = 1;
		}
		int secYay = 792;
		for (int i = yayPoint + 8; i < secYay; ++i)
		{
			int ii = i + 8;
			if (ii % 4 == 0)
			{
				if (ii % 32 < 16)
					beats[1, i] = 1;
				else
					beats[0, i] = 1;
			}
			if (ii % 4 == 2)
			{
				beats[3, i] = 1;
			}
		}
		for (int i = secYay; i < secYay + 4; ++i)
		{
			beats[i - secYay, i] = 1;
		}
		int bellsStop = 1000;
		int soloStart = 860;
		for (int i = secYay + 8; i < soloStart; ++i)
		{
			int ii = i + 7;
			if (ii % 4 == 0)
			{
				if (ii % 32 < 16)
					beats[1, i] = 1;
				else
					beats[0, i] = 1;
			}
			if (ii % 4 == 1)
			{
				beats[2, i] = 1;
			}
			if (ii % 4 == 2)
			{
				beats[3, i] = 1;
			}
		}
		beats[1, soloStart + 0] = 1;
		beats[1, soloStart + 1] = 1;
		beats[2, soloStart + 2] = 1;
		beats[2, soloStart + 3] = 1;
		beats[3, soloStart + 4] = 1;
		beats[3, soloStart + 5] = 1;
		beats[3, soloStart + 6] = 1;
		beats[3, soloStart + 7] = 1;
		beats[2, soloStart + 8] = 1;
		beats[2, soloStart + 9] = 1;
		beats[1, soloStart + 10] = 1;
		beats[1, soloStart + 11] = 1;
		beats[1, soloStart + 12] = 1;
		beats[1, soloStart + 13] = 1;
		int nextSolo = 898;
		for (int i = soloStart + 14; i < nextSolo; ++i)
		{
			int ii = i + 7;
			if (ii % 4 == 0)
			{
				if (ii % 32 < 16)
					beats[1, i] = 1;
				else
					beats[0, i] = 1;
			}
			if (ii % 4 == 1)
			{
				beats[2, i] = 1;
			}
			if (ii % 4 == 2)
			{
				beats[3, i] = 1;
			}
		}
		beats[0, nextSolo + 0] = 1;
		beats[1, nextSolo + 1] = 1;
		beats[0, nextSolo + 2] = 1;
		beats[1, nextSolo + 3] = 1;
		beats[1, nextSolo + 4] = 1;
		beats[1, nextSolo + 5] = 1;
		beats[1, nextSolo + 6] = 1;
		beats[1, nextSolo + 7] = 1;
		beats[1, nextSolo + 8] = 1;
		beats[2, nextSolo + 9] = 1;
		beats[3, nextSolo + 10] = 1;
		beats[1, nextSolo + 11] = 1;
		beats[2, nextSolo + 12] = 1;
		beats[3, nextSolo + 13] = 1;
		beats[1, nextSolo + 14] = 1;
		beats[2, nextSolo + 15] = 1;
		beats[3, nextSolo + 16] = 1;
		beats[3, nextSolo + 17] = 1;
		beats[3, nextSolo + 18] = 1;
		beats[3, nextSolo + 19] = 1;
		beats[3, nextSolo + 20] = 1;
		beats[3, nextSolo + 21] = 1;
		beats[3, nextSolo + 22] = 1;
		int endYay = 1062;
		for (int i = nextSolo + 23; i < endYay; ++i)
		{
			int ii = i + 7;
			if (ii % 4 == 0)
			{
				if (ii % 32 < 16)
					beats[0, i] = 1;
				else
					beats[1, i] = 1;
			}
			if (ii % 4 == 2)
				beats[3, i] = 1;
			if (ii % 4 == 3)
				beats[3, i] = 1;
		}
		for (int i = endYay; i < endYay + 4; ++i)
		{
			beats[i - endYay, i] = 1;
		}
		int totalPossibleHits = 0;
		for (int i = 0; i < 4; ++i)
		{
			for (int j = 0; j < trackLength; ++j)
			{
				totalPossibleHits += beats[i, j];
			}
		}
		Debug.Log("total possible hits: " + totalPossibleHits);
	}
	public void Lose()
	{
		PlayerStats.Seconds_Survived = (int)Mathf.Floor(audioData.time);
		PlayerStats.Points = (int)Mathf.Floor(audioData.clip.length);
		SceneManager.LoadScene("Lose Scene");
	}
	public float GetFloatBeats(float time)
	{
		float bpm = 100f;
		// the '* 4' is to convert it to 16th notes (the fastest note)
		float beatsIn = time / 60 * bpm * 4 - 0.5f;
		return beatsIn;
	}
	public bool OnBeat(float time)
	{
		// this is used to tell if a button hit was on time (and light up the beat button)
		float beatsIn = GetFloatBeats(time);
		float beatsInFloor = Mathf.Floor(beatsIn);
		float beatFracSinceLast = beatsIn - beatsInFloor;
		float beatFracBeforeNext = Mathf.Abs(1 - beatFracSinceLast);
		float difficultyMargin = .2f;
		if (beatFracSinceLast < difficultyMargin || beatFracBeforeNext < difficultyMargin)
		{
			return true;
		}
		return false;
	}
	public bool OnBeat()
	{
		return OnBeat(audioData.time);
	}
	public void HitBeatButton(int track)
	{
		// this is called by the beat button
		if (tracks[track].transform.childCount > 0)
		{
			Transform lastChild = tracks[track].transform.GetChild(tracks[track].transform.childCount - 1);
			FallingButton fallButt = lastChild.gameObject.GetComponent<FallingButton>();
			float difficultyMargin = .3f;
			if (Mathf.Abs(fallButt.fallTime - fallButt.totalTime) < difficultyMargin)
			{
				PlayerStats.Hits += 1;
				PlayerStats.Meter_Val += 1;
				if (PlayerStats.Meter_Val > PlayerStats.Meter_Max)
					PlayerStats.Meter_Val = PlayerStats.Meter_Max;
				lightUpGraphics[track].GetComponent<LightUp>().DoLightUp();
				Destroy(lastChild.gameObject);
			}
			else
			{
				PlayerStats.Misses += 1;
				PlayerStats.Meter_Val -= 1;
				if (PlayerStats.Meter_Val <= 0)
				{
					this.Lose();
				}
			}
		}
		else
		{
			PlayerStats.Misses += 1;
			PlayerStats.Meter_Val -= 1;
			if (PlayerStats.Meter_Val <= 0)
			{
				this.Lose();
			}
		}
	}
	public void Update()
	{
		float fallTime = 1.8f; // in seconds
		for (int i = 0; i < numTracks; ++i)
		{
			// issue all beats that we need to catch up
			while (beatsDropped[i] < GetFloatBeats(audioData.time + fallTime))
			{
				beatsDropped[i] += 1; // this line is first because if you forget it, the while loop will hang
									  // check if this is an actual beat according to the song
				int isBeat = beats[i, beatsDropped[i] - 1];
				if (isBeat == 1)
				{
					// create a new beat object
					GameObject thisBeat = (GameObject)Instantiate(beatPrefabs[i]);
					// get the script for the beat (this has the variables)
					FallingButton thisBeatScript = thisBeat.GetComponent<FallingButton>();
					// set the start point as the vanishing point
					thisBeatScript.startPoint = startButtonFallPoint.transform.localPosition;
					// set the button to know us so it can make us lose
					thisBeatScript.music = this;
					// set the endpoint to be where the player taps
					thisBeatScript.endPoint = endButtonFallPoints[i].transform.localPosition;
					// put the beat in the correct track
					thisBeat.transform.SetParent(tracks[i].transform, worldPositionStays: true);
					// put the newest beat behind the rest
					thisBeat.transform.SetAsFirstSibling();
					// if we're late on this beat, start it with some time to catch up (this doesn't seem to work)
					thisBeatScript.totalTime = (GetFloatBeats(audioData.time + fallTime) - beatsDropped[i]);
					// setting the fallTime will initialize the object
					thisBeatScript.fallTime = fallTime;
				}
			}
		}
		if (!audioData.isPlaying)
		{
			SceneManager.LoadScene("Win Scene");

		}
	}
}

