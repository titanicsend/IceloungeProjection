using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerAutoMover : MonoBehaviour
{

    bool m_Started;
    public LayerMask m_LayerMask;

    public float collisionSphereSize = 1000;

    public float forwardSpeed = 0.0f;

    public GameObject virtualCameraOrbiter; //the object with a Virtual camera from cinemachine.
    public GameObject virtualCameraWander; //the object with a Virtual camera from cinemachine.

    public CinemachineBrain cinemachineBrain;

    public bool wandering = true;
    public bool orbiting = false;

    public float cameraWanderTime = 10.0f;
    public float cameraOrbitTime = 20.0f;

    // public Transform player; //the player that moves around the scene and controls the dolly.
    // public Transform lookForward; //a transform directly in front of the player to control looking forward.

    // private bool track1Played = false; // a boolean to track whether or not the first track has played
    private Collider[] hitColliders;
    // private Dictionary<string, Collider> colliderDict = new Dictionary<string, Collider>();
    private List<Collider> dancersNearby = new List<Collider>();
    private CinemachineVirtualCamera cineOrbiter;
    public float timer = 0.0f;

    void Start()
    {
        //Use this to ensure that the Gizmos are being drawn when in Play Mode.
        m_Started = true;
    }

    void FixedUpdate()
    {
        MyCollisions();
        // no my collisions
    }

    // string createPersonKey(Collider selectedPerson){
    //     return selectedPerson.name+selectedPerson.transform.position.x.ToString("#.00")+selectedPerson.transform.position.y.ToString("#.00")+selectedPerson.transform.position.z.ToString("#.00");
    // }

    void getDancersNearby(){
        this.hitColliders = Physics.OverlapSphere(gameObject.transform.position, collisionSphereSize);
        this.dancersNearby.Clear();
        for (int i = 0; i < this.hitColliders.Length; i++) 
        {
            // Debug.Log("collider in box: " + collider.name);
            if (this.hitColliders[i].name.Contains("dancer")){
                this.dancersNearby.Add(this.hitColliders[i]);
            }
        }
    }
    void MyCollisions()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.

        // get the objects that are near the camera  and put them in a dictionary hashed by object name and position.
        getDancersNearby();

        timer += Time.deltaTime;

        if (this.wandering){
            if (!cinemachineBrain.IsBlending){
                // float sine = Mathf.Sin(Time.deltaTime * frequency);
                // movementX = sine/5;
                // movementY = speed * ;

                // Debug.Log("movementX "+movementX+"movementY "+movementY);
                // gameObject.transform
                // Debug.Log("wandering and cinemachineBrain.IsBlending: "+ cinemachineBrain.IsBlending.ToString() + "Time.deltaTime*forwardSpeed: "+ (Time.deltaTime*forwardSpeed).ToString());

                gameObject.transform.Translate(0.0f, 0.0f, Time.deltaTime*forwardSpeed);
                // player.Rotate(0.0f,Time.deltaTime*speed,0.0f);
            }

            if (timer > cameraWanderTime){
                this.wandering = false;
                this.orbiting = true;
                this.cineOrbiter = virtualCameraOrbiter.GetComponent<CinemachineVirtualCamera>();
                CinemachineVirtualCamera cineWander = virtualCameraWander.GetComponent<CinemachineVirtualCamera>();

                if (this.dancersNearby.Count>0){
                    // if the time has expired and there are dancers nearby then go look at one of them.

                    // set the thing to orbit to be a new thing.
                    int idx = Random.Range(0,this.dancersNearby.Count-1);

                    // this.dancersNearby[idx].gameObject.parent;
                    
                    this.cineOrbiter.LookAt = this.dancersNearby[idx].transform;
                    this.cineOrbiter.Follow = this.dancersNearby[idx].transform;
                    // this.cineOrbiter.LookAt.Translate(-100*Vector3.up);
                    // this.cineOrbiter.Follow.Translate(500*Vector3.up);

                    Debug.Log("Change orbiter position");
                    // Set orbit as priority
                    this.cineOrbiter.Priority = 100;
                    cineWander.Priority = 1;
                    Debug.Log("set Orbiter as priority");
                    timer = 0; //reset timer
                } else {
                    // if the time has expired and there are no dancers nearby, keep wandering for another cycle.
                    this.wandering = true;
                    this.orbiting = false;
                    Debug.Log("no dancers nearby, gunna keep wandering");
                    timer = 0;
                }
            }
        }
        if (this.orbiting){
            if (timer > cameraOrbitTime){
                this.wandering = true;
                this.orbiting = false;
                // Set wander as priority
                CinemachineVirtualCamera cineWander = virtualCameraWander.GetComponent<CinemachineVirtualCamera>();
                cineWander.Priority = 100;
                this.cineOrbiter.Priority = 1;
                Debug.Log("set Wanderer as priority");
                timer = 0; //reset timer
            }
        }
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Color color = new Color();
        color = Color.red;
        color.a = 0.5f;
        Gizmos.color = color;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawSphere(transform.position, collisionSphereSize);
            if (this.hitColliders != null){
                // Debug.Log("-----dancers in list ---- ");
                for (int i = 0; i < this.dancersNearby.Count; i++) 
                {
                    Gizmos.DrawSphere(this.dancersNearby[i].transform.position, 100);
                }
            }
            if (this.cineOrbiter!=null && this.cineOrbiter.LookAt !=null){
                Gizmos.color=Color.green;
                Gizmos.DrawSphere(this.cineOrbiter.LookAt.position, 100);
            }

    }
}
