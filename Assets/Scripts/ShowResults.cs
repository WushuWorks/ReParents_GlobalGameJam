using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowResults : MonoBehaviour
{
    public Text hit_perc, points, rank_title;
    public int S_rank_score, A_rank_score, B_rank_score, C_rank_score, D_rank_score;

    void Start()
    {
        float total = PlayerStats.Hits + PlayerStats.Misses;
        float meter_m = PlayerStats.Meter_Max;
        float song_len = PlayerStats.Points; //We had an extra field and needed song length here
        // Do not divide by zero
        if (total == 0) total = 1;
        if (meter_m == 0) meter_m = 1;
        if (song_len == 0) song_len = 1;

        float hit_perc_raw = ((float)PlayerStats.Hits / total); //decimal percentage
        float meter_perc = (float)PlayerStats.Meter_Val / (float)meter_m;
        float survive_perc = (float)PlayerStats.Seconds_Survived / (float)song_len;
        
        int game_score = CalcPoints(PlayerStats.Hits, meter_perc, survive_perc); //Note that hit_perc is not included

        points.text = game_score.ToString();
        hit_perc.text = (hit_perc_raw * 100.0f).ToString("F1");

        int weightedScore = (int)(game_score * hit_perc_raw);

        if (weightedScore < D_rank_score) { rank_title.text = "Total Burnout"; } //F rank
        else if (weightedScore < C_rank_score) { rank_title.text = "Beat Cruiser"; } //D rank
        else if (weightedScore < B_rank_score) { rank_title.text = "Synth Surfer"; } //C rank
        else if (weightedScore < A_rank_score) { rank_title.text = "Wave Runner"; } //B rank
        else if (weightedScore < S_rank_score) { rank_title.text = "Mix Master"; } //A rank
        else { rank_title.text = "Lord of Synth"; } //S rank
    }

    //You can have up to 2x your hits as your max score
    //Meter_perc and survive_perc are usually 1~ so we halve them
    public int CalcPoints(int hits, float meter_perc, float survive_perc)
    {
        float w_hit_score = hits;
        float w_meter_score = 0.5f * (hits * meter_perc);
        float w_survive_score = 0.5f * (hits * meter_perc);
        return (int)(w_hit_score + w_meter_score + w_survive_score);
    }
}
