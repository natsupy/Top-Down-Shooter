using UnityEngine;
using System.Collections;

//This is the script that we write everything that has to do with the User Input
public class UserInput : MonoBehaviour
{

    public float speed = 5;
    public bool hidemouse;

    Transform cam;
    Vector3 camForward; //stores the forward vector of the cam
    Vector3 move;
    Vector3 moveInput;
    float forwardAmount;
    float turnAmount;

    Vector3 lookPos;

    Animator anim;

    Rigidbody rigidBody;


    void Start()
    {
        SetUpAnimator();
   
        cam = Camera.main.transform;

        anim = GetComponent<Animator>();

        rigidBody = GetComponent<Rigidbody>();

        if(hidemouse)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;       
        }
    }

    void Update()
    {

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, 100))
		{
			lookPos = hit.point;
		}

		Vector3 lookDir = lookPos - transform.position;
		lookDir.y = 0;

		transform.LookAt(transform.position + lookDir, Vector3.up);	
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (cam != null) //if there is a camera
        {
            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
			move = vertical * camForward + horizontal * cam.right;
        }
        else
        {
            move = vertical * Vector3.forward + horizontal * Vector3.right;
        }

        if (move.magnitude > 1) //Make sure that the movement is normalized
            move.Normalize();

        Move(move);

        Vector3 movement = new Vector3(horizontal, 0, vertical);
      
        if (horizontal == 0 && vertical == 0)
        {
            rigidBody.velocity = Vector3.zero * 0.5f;
        }
       

        rigidBody.AddForce(movement * speed / Time.deltaTime);

        
    }

    void Move(Vector3 move)
    {
        //Vector3 move is the input in word space
        if (move.magnitude > 1)
            move.Normalize();

        this.moveInput = move; //store the move

        ConvertMoveInput();
        UpdateAnimator();

    }

    void ConvertMoveInput()
    {

        Vector3 localMove = transform.InverseTransformDirection(moveInput);

        turnAmount = localMove.x;

   
        //Improves sideways speed
        if (localMove.z < -0.3f)
        {
            localMove.z = -1;
        }
        else if (localMove.z > 0.3f)
        {
            localMove.z = 1;
        }
        
        forwardAmount = localMove.z;
    }

    void UpdateAnimator()
    {
        anim.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        anim.SetFloat("Sideways", turnAmount, 0.1f, Time.deltaTime);
    }

    void SetUpAnimator()
    {
        // this is a ref to the animator component on the root.
        anim = GetComponent<Animator>();

        // we use avatar from a child animator component if present
        // this is to enable easy swapping of the character model as a child node
        foreach (var childAnimator in GetComponentsInChildren<Animator>())
        {
            if (childAnimator != anim)
            {
                anim.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break; //if you find the first animator, stop searching
            }
        }
    }


    void OnDisable()
    {
        Cursor.visible = true;
    }
    
}

   