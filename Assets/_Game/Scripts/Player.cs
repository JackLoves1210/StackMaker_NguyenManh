using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject Object;
    [SerializeField] public GameObject gameController;
    [SerializeField] Stack<GameObject> brickUp = new Stack<GameObject>();
    [SerializeField] private Swipe swipeControlls;
    Vector3 desiredPosition;
    

    private Vector3 startTouchPosition;
    private Vector3 endTouchPosition;

    bool IsMove = false;

    Vector3 target = new Vector3();
    Vector3 direction = new Vector3();

   

    [SerializeField] private float force = 400f;
    int count = 2;
    private Vector3 stack = new Vector3(0, 0.25f, 0);
    void Start()
    {
        
        gameController = GameObject.FindGameObjectWithTag("GameController");
        swipeControlls = GameObject.FindObjectOfType<Swipe>();
        direction = Vector3.right;
        target = transform.position;
        desiredPosition = transform.position;   
    }

    private void Update()
    {
        // input => var

        // checkMoving();
        CheckMovingSwipe();
        MoveController();
        ControllerBrick();
    }

   
    void FixedUpdate()
    {
       // move: var 

        //Moving
        //MoveController();
        //AddBrick
       // ControllerBrick();

    }

    private void CheckMovingSwipe()
    {
        Ray Ray = new Ray();
        Debug.Log(IsDestination());
        if (IsDestination())
        {
            if (swipeControlls.SwipeLeft)
            {
                Debug.Log("Left");
                IsMove = true;
                Ray = ray();
                Debug.Log("Ray : " + Ray);
                GetTarget(Ray);
            }
            if (swipeControlls.SwipeRight)
            {
                Debug.Log("right");
                IsMove = true;
                Ray = ray();
                GetTarget(Ray);
            }
            if (swipeControlls.SwipeForward)
            {
                IsMove = true;
                Ray = ray();
                GetTarget(Ray);
            }
            if (swipeControlls.SwipeBackward)
            {
                IsMove = true;
                Ray = ray();
                GetTarget(Ray);
            }
        }
        Debug.Log(IsMove);
        if (IsMove)
        {
            target.y = transform.position.y;
            Debug.Log("target :" + target);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.fixedDeltaTime * speed);
        }

    }
    private void checkMoving()
    {
        Ray Ray = new Ray();
        Debug.Log(IsDestination());
        if (IsDestination())
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                IsMove = true;
                
                Ray = ray();
                
                GetTarget(Ray);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                IsMove = true;
                Ray = ray();
                GetTarget(Ray);

            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                IsMove = true;
                Ray = ray();
                GetTarget(Ray);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                IsMove = true;
                Ray = ray();
                GetTarget(Ray);
            }
        }

    }
    void GetTarget(Ray ray)
    {
        direction = GetDirection(ray);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f) && hit.collider.tag == "wall")
        {
            Debug.Log(hit.collider.name);
            Debug.Log("hit : " + hit.point);
            target = hit.point - direction.normalized *0.5f;
        }
    }

    public bool IsDestination()
    {
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.x, target.z)) < 0.1f)
        {
            return true;
        }

        return false;
    }

    private Vector3 GetDirection(Ray ray)
    {
        return ray.direction;
    }
    private Ray ray()
    {
        Ray ray;
        Vector3 driection = new Vector3();
        if (swipeControlls.SwipeRight)
        {
            driection = Vector3.right;
        }
        else if (swipeControlls.SwipeLeft)
            {
            driection = Vector3.left;
        }
        else if (swipeControlls.SwipeBackward)
        {
            driection = Vector3.back;
        }
        else if (swipeControlls.SwipeForward)
        {
            driection = Vector3.forward;
        }
        ray = new Ray(transform.position, driection);
        return ray;
    }
    private void MoveController()
    {
        if (IsMove)
        {
            target.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position , target, Time.fixedDeltaTime *speed);
        }
    }
   
    private void ControllerBrick()
    {

        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 100f, color: Color.red);
        if (Physics.Raycast(transform.position,  Vector3.down, out hit, 100f))
        {
            if (hit.collider.tag == "brick")
            {
                // Tạo 1 stack ngay dưới stack cũ
                GameObject obj = Instantiate(Object, new Vector3(transform.position.x, transform.position.y - count * stack.y, transform.position.z), Quaternion.identity) as GameObject;

                brickUp.Push(obj);

                // tăng độ cao nhân vật thêm 1 stack (0.25f)
                transform.position += stack;

                // xóa brick khi va chạm với raycast
                Destroy(hit.collider.gameObject);

                // sét các brick sinh ra làm con của player
                obj.transform.SetParent(transform);
                // tăng số lượng gạch lên 1 đv

                count++;
                Debug.Log("1 :" + (count - 2));
            }
            else if (hit.collider.tag == "bridge")
            {
                count--;
                Destroy(hit.collider.gameObject);
                RemoveBrick();
            }
            else if (hit.collider.tag == "bridge_end")
            {
                Destroy(hit.collider.gameObject);
                if (brickUp.Count <= 3)
                {
                    EndGame();
                }
                else
                {
                    ClearBrick();
                }
            }
            else if ((hit.collider.tag == "FinishLevel"))
            {
                FinishLevel();
            }
        }
    }

    private void RemoveBrick()
    {
       
            transform.position -= stack;
            Destroy(brickUp.Pop());
    } 
    private void ClearBrick()
    {
        Debug.Log(brickUp.Count);
        transform.position -= new Vector3(0, 0.75f, 0);
        if (brickUp.Count >= 3)
        {
            
            Destroy(brickUp.Pop());
            Destroy(brickUp.Pop());
            Destroy(brickUp.Pop());
        }
    }

    private void FinishLevel()
    {
        gameController.GetComponent<GameController>().getGold(50);
        gameController.GetComponent<GameController>().PnlNextLevel();
    }

    private void EndGame()
    {
        gameController.GetComponent<GameController>().EndGame();
    }
}
