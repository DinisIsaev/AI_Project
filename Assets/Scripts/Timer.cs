using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    public float reset;
    [SerializeField]
    public float timeLeft;
    
    [SerializeField]
    private PlayerAgent[] playersSize;
    private PlayerAgent[] players;

    private void Start()
    {
        Fill();
        reset = timeLeft;
    }

    public void Fill()
    {
        players = new PlayerAgent[playersSize.Length];
        for(int i = 0; i<playersSize.Length; i++)
        {
            players[i] = playersSize[i];
        }
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            foreach (var agent in players)
            {
                agent.EndEpisode();
            }
            timeLeft = reset;
            var balls = FindObjectsOfType<Ball>();
            foreach (var bola in balls)
            {
                bola.transform.position = Vector3.zero;
            }

        }
    }
}
