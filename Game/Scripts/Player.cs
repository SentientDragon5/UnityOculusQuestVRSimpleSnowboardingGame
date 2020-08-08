/* Snowboarding1 - SentientDragon5
 * 
 * This is The player script. I have annotated some of the functions, though most of it should be straightforward
 * considering I am a novice programmer.
 */
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public enum GameState
{
    Transitioning,
    Start,
    Play,
    Pause,
    Lose
}
public class Player : MonoBehaviour
{
    #region Variables
    [Header("Quest Input")]
    public bool QuestBuild = true; //This is used to decide whether to get Oculus Controls including whether to allow drifting. if False then then A and D are for left and right.
    public float Height = 1f; // This is a dynamic variable that is how high the user's head is from the ground in roomscale distance (meters)

    [Header("Start")]
    public GameObject startingPoint; //where to spawn the player at start
    public float startSpeed = 2000f;
    public int gameStart = 0;
    public float StartDelay = 5f;
    public GameObject CountDownObject;
    public bool countDownComplete = false;

    [Header("Movement")]
    public AnimationCurve heightCurve; //now i originally had a brilliant idea of the player's speed being controlled by height, this is the relationship as a function of X being height, to Y being Acceleration, and indirectly speed.
    public float heightBound = 1;
    public float rotationSpeed = 100f;
    [Range(0, 40f)] public float maxSpeed = 20f; //these ranges are the max and min speeds, the speed is clamped to these.
    [Range(0, 40f)] public float minSpeed = 2f;
    public float strafeVelocity = 5f;
    public float strafeVelocityDeterioration = 0.1f;
    public float strafeBuffer = 0.1f;
    public float jumpBuffer = 0.1f;
    public float maxBackClimbVel = 5f;// this is to ensure the player doesnt end up rocking back and forth on too big of a slope, it makes for a less realistic game, but i don't feel like ending the run for a faliure on world generation.

    private float[] heights = { 0f, 0f, 0f }; // i was attempting to add upward force when the player jumped or moved upward suddenly.
    private float headsetHeightLast = 0;

    [Header("Game")]
    public GameState currentState;
    public int Coins = 0; //this is local, only to this scene, it gets its info from the LoadManager.

    [Header("Effects")]
    public List<GameObject> loseEffect = new List<GameObject>();

    [Header("GameObjects")]
    public GameObject Board;
    public GameObject LeftEmission;//effects of particle effects when you strafe
    public GameObject RightEmission;

    [Header("Bounds")]
    public float xBound = 6f;

    [Header("Debug")]
    public GameObject left; // left and right arrows on the score canvas.
    public GameObject right;

    private GameObject headset;
    private Rigidbody rb; //rigidbody
    private float startTime = 0f;
    private PauseGameController pauseGameController;
    #endregion
    #region Initialization
    // Start is called before the first frame update
    void Start()
    {
        // Set current state
        currentState = GameState.Start;

        // Prepare the Components and Tracking
        rb = GetComponent<Rigidbody>();
        headset = GameObject.Find("CenterEyeAnchor");
        pauseGameController = GetComponent<PauseGameController>();
        pauseGameController.enabled = false;

        //Import Settings
        Coins = LoadManager.instance.data.Coins;

        // set the effects to off
        LeftEmission.SetActive(false);
        RightEmission.SetActive(false);

        //SetStartLocation
        transform.position = startingPoint.transform.position;

    }
    // Update is called once per frame
    void Update()
    {
        //OVR Input Checks
        OVRInput.Update();// ALWAYS call this if you want
        OVRInput.FixedUpdate();
        SavePlayer();
        //Start
        if (currentState == GameState.Start)
        {
            pauseGameController.enabled = false;// is pausing allowed in this state?
            for (int i = 0; i < loseEffect.Count; i++)//ensure game is not showing that it is failed
            {
                loseEffect[i].SetActive(false);
            }
            rb.isKinematic = true;
            if (startTime == 0f)
            {
                startTime = Time.time;
            }
            if (Time.time > startTime + StartDelay)
            {
                countDownComplete = true;
            }
            else
            {
                countDownComplete = false;

            }
            if (!countDownComplete)
            {
                rb.isKinematic = true;
                CountDownObject.SetActive(true);
            }
            if (countDownComplete)
            {
                currentState = GameState.Play;
                CountDownObject.SetActive(false);
                rb.isKinematic = false;
                GameStart(Vector3.zero, true);
                startTime = 0f;
            }
        }
        //Play
        if(currentState == GameState.Play)
        {
            pauseGameController.enabled = true; // is pausing allowed in this state?
            rb.isKinematic = false;
            Board.GetComponent<Rigidbody>().isKinematic = false;
            // check the box in the inspector if this is a build that is going on the Oculus Quest
            if (QuestBuild)
            {
                Height = transform.position.y - headset.transform.position.y;
                headsetRotateControls();
            }
            else{ keyboardRotateControls();}
            if (Height >= heightBound && Height <= heightBound + 1)
            {
                float heightCurveY = this.heightCurve.Evaluate(Height - heightBound);
                rb.AddForce(transform.forward * -10 * heightCurveY);
            }
            // this is essentially my Air resistance, it keeps the player from going too fast if they are standing.
            //lockedRotation();
            preventBackClimbing();
            checkBounds();
            clampSpeed();
            jump();
            
        }
        if (currentState == GameState.Lose)
        {
            pauseGameController.enabled = false;// is pausing allowed in this state?
            LoseGame();
        }
        SceneSwitchOveride("q", "start3");
        SceneSwitchOveride("e", "5");

    }
    #endregion
    #region Movement
    // This function will clamp my rotation on all axis, though it is very jerky. It doess not look right and I will probably not use.
    void lockedRotation()
    {
        var rotation = transform.localEulerAngles;
        rotation.x = Mathf.Clamp(rotation.x, -60, 60);
        rotation.y = Mathf.Clamp(rotation.y, -15, 15);
        rotation.z = Mathf.Clamp(rotation.z, -15, 15);
        transform.eulerAngles = rotation;
    }
    //This function is to allow the useer to strafe in the inspector
    void headsetRotateControls()
    {
        if (headset.transform.position.x - transform.position.x < (-1 * strafeBuffer))
        {
            //Board.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, -10, 0), rotationSpeed);
            left.SetActive(true);
            right.SetActive(false);
            LeftEmission.SetActive(true);
            RightEmission.SetActive(false);
            rb.AddForce(transform.right * strafeVelocity * (headset.transform.position.x - transform.position.x));
        }
        else if (headset.transform.position.x - transform.position.x > strafeBuffer)
        {
            //Board.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, 10, 0), rotationSpeed);
            left.SetActive(false);
            right.SetActive(true);
            LeftEmission.SetActive(false);
            RightEmission.SetActive(true);
            rb.AddForce(transform.right * strafeVelocity * (headset.transform.position.x - transform.position.x));
        }
        else
        {
            //Board.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, 0, 0), rotationSpeed);
            left.SetActive(false);
            right.SetActive(false);
            LeftEmission.SetActive(false);
            RightEmission.SetActive(false);
            if (rb.velocity.x < 0f)
            {
                rb.velocity = new Vector3(rb.velocity.x + strafeVelocityDeterioration, rb.velocity.y, rb.velocity.z);

            }
            else if (rb.velocity.x > 0f)
            {
                rb.velocity = new Vector3(rb.velocity.x - strafeVelocityDeterioration, rb.velocity.y, rb.velocity.z);
            }
        }
    }
    // This function will clamp my speed on the rigidbody, so that I will stop gaining momentum at max speed
    void clampSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        if (rb.velocity.magnitude < minSpeed)
        {
            rb.velocity = rb.velocity.normalized * minSpeed;
        }
    }
    #endregion
    #region Jump
    //This function does not work, but was to attempt a jump when the head goes, down, up, down.
    void jump()
    {
        heights[2] = heights[1];
        heights[1] = heights[0];
        if (((headsetHeightLast - Height) < jumpBuffer) || ((headsetHeightLast - Height) > (-1 * jumpBuffer)))
        {
            heights[2] = heights[1];
            heights[1] = heights[0];
            heights[0] = Height;
        }
        if ((heights[0] > heights[1]) && (heights[1] < heights[2]))
        {
            //rb.AddForce(Vector3.up * ((heights[0] - heights[1]) + (heights[2] - heights[1])));
            rb.AddForce(Vector3.up * 20);


        }
    }
    #endregion
    #region Bounds
    //This function is to allow the player to keep moving forward, even when climbing hills.
    void preventBackClimbing()
    {
        if (rb.velocity.magnitude < maxBackClimbVel)
        {
            rb.AddForce(transform.forward * 2 * rb.velocity.magnitude);
        }
    }
    //This function is to keep the player restrained within two floats which controll the max and min on the x axis that the player can go to. The old version is colliders, but they do nothing.
    void checkBounds()
    {
        if( transform.position.x >= xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
    }
    #endregion
    #region Save
    //This function is to overwrite data in the Loadmanager
    public void SavePlayer()
    {
        //SaveSystem.SavePlayer(this);
        LoadManager.instance.data.Coins = Coins;
    }
    #endregion
    #region Start
    //This function is to teleport a player to a location, and give them a boost is boost is enabled.
    public void GameStart(Vector3 Location, bool boost)
    {
        transform.position = Location;
        if (startingPoint != null)
        {
            transform.position = startingPoint.transform.position;
        }
        if (boost)
        {
            rb.AddForce(transform.forward * startSpeed);
        }
        
    }
    //This function is to delay the start of the game
    IEnumerator kinematicDelay(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        
    }
    #endregion
    #region Lose
    //This function is to trigger the lose State.
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Danger"))
        {
            currentState = GameState.Lose;
        }
    }
    //This function is to come out of a pause, it doesn't work right though.
    public void ResumeGame()
    {
        for (int i = 0; i < loseEffect.Count; i++)//ensure game is not showing that it is failed
        {
            loseEffect[i].SetActive(false);
        }
        rb.isKinematic = true;
        if (startTime == 0f)
        {
            startTime = Time.time;
        }
        if (Time.time > startTime + StartDelay)
        {
            countDownComplete = true;
        }
        else
        {
            countDownComplete = false;

        }
        if (!countDownComplete)
        {
            rb.isKinematic = true;
            CountDownObject.SetActive(true);
        }
        if (countDownComplete)
        {
            currentState = GameState.Play;
            CountDownObject.SetActive(false);
            rb.isKinematic = false;
            GameStart(transform.position, false);
            startTime = 0f;
        }
    }
    //This function is to stop the game and bring up the fail menu.
    public void LoseGame()
    {
        pauseGameController.enabled = false;
        rb.isKinematic = true;
        Board.GetComponent<Rigidbody>().isKinematic = true;
        for (int i = 0; i < loseEffect.Count; i++)
        {
            loseEffect[i].SetActive(true);
        }
    }
    // can be called by a Unity Event
    public void Quit()
    {
        SceneManager.LoadScene("start3");
    }
    public void Reload()
    {
        SceneManager.LoadScene("5");
    }
    #endregion
    #region OveridesForDebug
    // This function will allow me to recive input from the keyboard keys for testing turning
    void keyboardRotateControls()
    {
        if (Input.GetKey("a"))
        {
            //Board.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, -10, 0), rotationSpeed);
            left.SetActive(true);
            right.SetActive(false);
            rb.AddForce(transform.right * -1);
            if (Board.GetComponent<BoardEmission>().OnGround)
            {
                LeftEmission.SetActive(true);
                RightEmission.SetActive(false);
            }
        }
        else if (Input.GetKey("d"))
        {
            //Board.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, 10, 0), rotationSpeed);
            left.SetActive(false);
            right.SetActive(true);
            rb.AddForce(transform.right);
            if (Board.GetComponent<BoardEmission>().OnGround)
            {
                LeftEmission.SetActive(false);
                RightEmission.SetActive(true);
            }
        }
        else
        {
            //Board.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, 0, 0), rotationSpeed);
            left.SetActive(false);
            right.SetActive(false);
            if (rb.velocity.x < 0f)
            {
                rb.velocity = new Vector3(rb.velocity.x + strafeVelocityDeterioration, rb.velocity.y, rb.velocity.z);

            }
            else if (rb.velocity.x > 0f)
            {
                rb.velocity = new Vector3(rb.velocity.x - strafeVelocityDeterioration, rb.velocity.y, rb.velocity.z);
            }
            if (Board.GetComponent<BoardEmission>().OnGround)
            {
                LeftEmission.SetActive(false);
                RightEmission.SetActive(false);
            }
        }
    }
    // If i press this button, it will reload the specified scene.
    public void SceneSwitchOveride(string button, string scene)
    {
        if (Input.GetKeyDown(button))
        {
            SceneManager.LoadScene(scene);
        }
    }

    #endregion
}
