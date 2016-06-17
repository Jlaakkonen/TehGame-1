using UnityEngine;
using System.Collections;

public class MoviePlayer : MonoBehaviour
{
    private string movPath = "Teh GAME animation with SNOW.mp4";
    private static bool moviePlayed = false;

    // Use this for initialization
    void Start()
    {
		if (moviePlayed == false) {
			Movieplayer (movPath);
			Debug.Log ("Vilmi pyörinyt");
		}
    }

    public void Movieplayer(string path)
    {
		Handheld.PlayFullScreenMovie(path, Color.white, FullScreenMovieControlMode.CancelOnInput);
        moviePlayed = true;
		GlobalGameManager.GGM.StartMapScene ();
    }


}
