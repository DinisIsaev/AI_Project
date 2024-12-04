using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Environment Env;
    public PlayerAgent ballLastTouchedBy;
    public PlayerAgent ballLastEnemyTouched;
    public PlayerAgent ballLastAllyTouched;
    private Rigidbody rb;
    private float kick = 0.8f;
    ContactPoint contact;
    Vector3 collisionNormal;

    
    public void Start(){
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {   
        contact = collision.contacts[0];
        collisionNormal = contact.normal;
        switch(collision.gameObject.tag){
            case "Red":
                rb.AddForce(collisionNormal*kick, ForceMode.Impulse);
                if(ballLastTouchedBy == null){
                    ballLastTouchedBy = collision.gameObject.GetComponent<PlayerAgent>();
                }
                else{
                    if (ballLastTouchedBy == collision.gameObject.GetComponent<PlayerAgent>()){ //drible
                        ballLastTouchedBy.AddReward(2f);
                    }
                    else if (ballLastTouchedBy != collision.gameObject.GetComponent<PlayerAgent>() && ballLastTouchedBy.team){ //passe
                        ballLastTouchedBy.AddReward(1f);
                        ballLastAllyTouched = ballLastTouchedBy;
                    }
                    else if (ballLastTouchedBy != collision.gameObject.GetComponent<PlayerAgent>() && !ballLastTouchedBy.team){ //passou a inimigo
                        ballLastTouchedBy.AddReward(-1f);
                        ballLastEnemyTouched = ballLastTouchedBy;
                    }
                    ballLastTouchedBy = collision.gameObject.GetComponent<PlayerAgent>();
                }
                ballLastTouchedBy.timerForInactivity = 10f;
                if (collisionNormal.z < -0.5){ //Shot forward
                    ballLastTouchedBy.AddReward(2f);
                }
                break;
            case "Blue":
                rb.AddForce(collisionNormal*kick, ForceMode.Impulse);
                if(ballLastTouchedBy == null){
                    ballLastTouchedBy = collision.gameObject.GetComponent<PlayerAgent>();
                }
                else{
                    if (ballLastTouchedBy == collision.gameObject.GetComponent<PlayerAgent>()){ //drible
                        ballLastTouchedBy.AddReward(2f);
                    }
                    else if (ballLastTouchedBy != collision.gameObject.GetComponent<PlayerAgent>() && !ballLastTouchedBy.team){ //passe
                        ballLastTouchedBy.AddReward(1f);
                    }
                    else if (ballLastTouchedBy != collision.gameObject.GetComponent<PlayerAgent>() && ballLastTouchedBy.team){ //passou a inimigo
                        ballLastTouchedBy.AddReward(-1f);
                        ballLastEnemyTouched = ballLastTouchedBy;
                    }
                    ballLastTouchedBy = collision.gameObject.GetComponent<PlayerAgent>();
                }
                ballLastTouchedBy.timerForInactivity = 10f;
                if (collisionNormal.z > 0.5){ //Shot forward
                    ballLastTouchedBy.AddReward(2f); 
                }
                break;
            case "BlueGoal":
                if (ballLastTouchedBy.team){
                    //golo dos vermelho
                    ballLastTouchedBy.AddReward(40f);
                    if (ballLastAllyTouched != null){
                        ballLastAllyTouched.AddReward(15f);
                    }
                }
                else{
                    //autogolo dos azuis
                    ballLastTouchedBy.AddReward(-40f);
                    if (ballLastEnemyTouched != null){
                        ballLastEnemyTouched.AddReward(15f);
                    }
                }
                Env.EndEpisodeForAllPlayers();
                break;
            case "RedGoal":
                if (ballLastTouchedBy.team){
                    //autogolo dos vermelhos
                    ballLastTouchedBy.AddReward(-40f);
                    if (ballLastEnemyTouched != null){
                        ballLastEnemyTouched.AddReward(15f);
                    }
                }
                else{
                    //golo dos azuis
                    ballLastTouchedBy.AddReward(40f);
                    if (ballLastAllyTouched != null){
                        ballLastAllyTouched.AddReward(15f);
                    }
                }
                Env.EndEpisodeForAllPlayers();
                break;
        }
    }
}