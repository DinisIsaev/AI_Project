using UnityEngine;

public class Environment : MonoBehaviour
{
    PlayerAgent[] allAgentInEnv;
    Ball[] ball;

    public void Start()
    {
        allAgentInEnv = GetComponentsInChildren<PlayerAgent>();
        ball = GetComponentsInChildren<Ball>();
    }

    public void EndEpisodeForAllPlayers(){
        ball[0].ballLastTouchedBy = null;
        ball[0].ballLastEnemyTouched = null;
        foreach (var agent in allAgentInEnv)
        {
            agent.EndEpisode();
        }
    }
}