using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PlayerAgent : Agent
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private Rigidbody Ball;

    [SerializeField] private Transform EnemyGoal;
    [SerializeField] private Transform OwnGoal;
    private Vector3 initialBallPosition;
    private Vector3 initialPlayerPosition;
    private Quaternion initialPlayerRotation;
    private Rigidbody rb;
    public float timerForInactivity;
    public bool team; //true means red


    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        initialBallPosition = Ball.position;
        initialPlayerPosition = rb.position;
        initialPlayerRotation = transform.rotation;
        timerForInactivity = 10f;
        switch (gameObject.tag){
            case "Red":
                team = true;
                break;
            case "Blue":
                team = false;
                break;
        }
    }

    void Update(){
        timerForInactivity -= 0.1f;
        Debug.Log(timerForInactivity);
        if(timerForInactivity <= 0.0){
            AddReward(-0.05f);
        }
    }

    public override void OnEpisodeBegin()
    {
        transform.position = initialPlayerPosition;
        rb.position = initialPlayerPosition;
        transform.rotation = initialPlayerRotation;
        Ball.position = initialBallPosition;
        Ball.linearVelocity = Vector3.zero;
        Ball.angularVelocity = Vector3.zero;
        Ball.isKinematic = true;
        Ball.isKinematic = false;
        timerForInactivity = 10f;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var moveForward = actions.ContinuousActions[0];
        var moveRotate = actions.ContinuousActions[1];
        rb.MovePosition(transform.position + transform.forward * moveForward * moveSpeed * Time.deltaTime);
        transform.Rotate(0f, moveRotate * moveSpeed, 0f, Space.Self);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(Ball.transform.position);
        sensor.AddObservation(EnemyGoal.position);
        sensor.AddObservation(OwnGoal.position);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag){
            case "Wall":
            AddReward(-1f);
                break;
            case "RedGoal":
                AddReward(-1f);
                break;
            case "BlueGoal":
                AddReward(-1f);
                break;
        }
    }
}
