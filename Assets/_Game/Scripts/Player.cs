using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject Object;

    [SerializeField] Stack<GameObject> brickUp = new Stack<GameObject>();

    bool IsMove = false;

    Vector3 target = new Vector3();
    Vector3 direction = new Vector3();

    public GameObject gameController;
    private Vector3 currentTranform;

    [SerializeField] private float force = 400f;
    int count = 2;
    private Vector3 stack = new Vector3(0, 0.25f, 0);
    void Start()
    {
        currentTranform = new Vector3(0, transform.position.y, 0);
        gameController = GameObject.FindGameObjectWithTag("GameController");
        direction = Vector3.right;
        target = transform.position;
    }

    private void Update()
    {
        // input => var
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

    void FixedUpdate()
    {
       // move: var 

        //Moving();
        MoveController();
        //AddBrick();
        ControllerBrick();

    }

    void GetTarget(Ray ray)
    {
        direction = GetDirection(ray);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            target = hit.point - direction.normalized *0.5f;
        }
    }

    void Moving()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Move D");
            Ray ray = new Ray(transform.position, transform.right);
            RaycastHit hit;
            Debug.DrawLine(transform.position, transform.position + Vector3.right * 100f, color: Color.red);
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "wall")
            {
                float distance = hit.distance;
                Vector3 taget = new Vector3(transform.position.x + distance , transform.position.y, transform.position.z) - transform.position;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + distance, transform.position.y, transform.position.z), Time.deltaTime * speed);
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Move W");
            Ray ray = new Ray(transform.position, Vector3.forward);
            RaycastHit hit;
            Debug.DrawLine(transform.position, transform.position + Vector3.forward * 100f, color: Color.red);
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "wall")
            {

                Debug.Log(hit.collider.name);
                float distance = hit.distance;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + distance - 0.5f), Time.deltaTime * speed);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            Ray ray = new Ray(transform.position, - transform.right);
            RaycastHit hit;
            Debug.DrawLine(transform.position, transform.position + Vector3.left * 100f, color: Color.red);
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "wall")
            {
                float distance = hit.distance;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - distance + 0.5f, transform.position.y, transform.position.z), Time.deltaTime * speed);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            Ray ray = new Ray(transform.position, Vector3.back);
            RaycastHit hit;
            Debug.DrawLine(transform.position, transform.position + Vector3.back * 100f, color: Color.red);
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "wall")
            {
                float distance = hit.distance;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - distance +0.5f), Time.deltaTime * speed);
            }
        }

    }

    public bool IsDestination ()
    {
        if (Vector2.Distance(new Vector2(transform.position.x,transform.position.z) , new Vector2(target.x,target.z) ) <  0.1f)
        {
            return true;
        }
        
        return false;
    }

    private void MoveTo(Vector3 target)
    {
        Debug.Log("Move");
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
    }

    private Vector3 SearchTarget(Ray ray)
    {
        //float distance = 0;
        Vector3 target = new Vector3();
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,50f) && hit.collider.tag=="wall")
        {
            target = hit.point;
        }
        //Debug.Log("direction" + ray.direction);
        //if (ray.direction.x > 0.5f)
        //{
        //    target = new Vector3(transform.position.x + distance - 0.5f, transform.position.y, transform.position.z);
        //    Debug.Log("vtri :" + transform.position + "target :" + target + "distance :" + distance);
        //}
        //if (ray.direction.x < -0.5f)
        //{
        //    target = new Vector3(transform.position.x - (distance - 0.5f), transform.position.y, transform.position.z);
        //    Debug.Log("vtri :" + transform.position + "target :" + target + "distance :" + distance);
        //}
        //if (ray.direction.z > 0.5f)
        //{
        //    target = new Vector3(transform.position.x, transform.position.y, transform.position.z + distance - 0.5f);
        //    Debug.Log("vtri :" + transform.position + "target :" + target + "distance :" + distance);
        //}
        //if (ray.direction.z < -0.5f)
        //{
        //    target = new Vector3(transform.position.x, transform.position.y, transform.position.z - distance + 0.5f);
        //    Debug.Log("vtri :" + transform.position + "target :" + target + "distance :" + distance);
        //}
       // target = Vector3(direction);
        return target;
    }

    private Vector3 GetDirection(Ray ray)
    {
        return ray.direction;
    }
    private Ray ray()
    {
        Ray ray;
        Vector3 driection = new Vector3();
        if (Input.GetKeyDown(KeyCode.D))
        {
            driection = Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            driection = Vector3.left;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            driection = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            driection = Vector3.forward;
        }
        ray = new Ray(transform.position, driection);
        return ray;
    }
    private void MoveController()
    {
        // IsMove = false;


        //Debug.Log("Ray :" + Ray + "Move :" + IsMove);
        //Debug.Log("isdestination :" + IsDestination(SearchTarget(Ray), Ray));
        //if (IsDestination(SearchTarget(Ray), Ray) && IsMove == true)
        //{
        //    Debug.Log("direction :" + GetDirection(Ray));
        //    Debug.Log("ray :" + Ray);
        //    Debug.Log("target :" + SearchTarget(Ray));
        //    MoveTo(SearchTarget(Ray));
        //    Debug.Log("ismoving");

        //}
        //Debug.Log("IS MOVE :" + IsMove);

        //if (direction.x > 0.5f )
        //{

        //}
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
           // Debug.Log("test");
           // Debug.Log(hit.collider.name);
            if (hit.collider.tag == "brick")
            {
               // Debug.Log("1");
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
              //  Debug.Log("1 :" + (count - 2));
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
                    Debug.Log("heest sasctk");
                    gameController.GetComponent<GameController>().ResetGame();
                }
                else
                {
                    ClearBrick();
                }
            }
            else if ((hit.collider.tag == "FinishLevel"))
            {
                NextLevel();
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


    public void NextLevel()
    {
        
        gameController.GetComponent<GameController>().NextLevel("");
    }

}
