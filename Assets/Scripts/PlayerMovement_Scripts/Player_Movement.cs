using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player_Movement : MonoBehaviour, IDragHandler
{

    private RaycastHit R_Hit = new RaycastHit();
    private RaycastHit L_Hit = new RaycastHit();
    private int  Layermask = 1 << 9;
    /// <summary>
    /// /////////////////////////////////
    /// </summary>
    public GameObject Player;
    Rigidbody Player_rb;
    float LimitX;
    public float Power;
    bool On_Drag;

    public static bool CanJump = true;
    float JumpStartTime;
    float JumpEndTime = -500f;
    void Awake()
    {
        //Layermask = ~Layermask;
        Debug.Log("test");
        On_Drag = false;
        LimitX = 5.5f;
        Player_rb = Player.GetComponent<Rigidbody>();
    }
    public void OnDrag(PointerEventData data)
    {
       On_Drag = true;

        if (data.delta.x > 0)//오른쪽
        {
            Ray rightRay = new Ray(Player.transform.position, Vector3.right);
            

            if (Physics.Raycast(rightRay,out R_Hit ,0.5f, Layermask) && R_Hit.collider.tag != "Player")
            {
                return;
            }
            else
            {
                Player.transform.Translate(new Vector3(Mathf.Min(data.delta.x,3f) * Power, 0, 0));
                //하기
            }
        }
        else// 왼쪽
        {
            Ray leftRay = new Ray(Player.transform.position, Vector3.left);
            // 왼쪽
            if (Physics.Raycast(leftRay,out L_Hit, 0.5f, Layermask) && L_Hit.transform.tag != "Player")
            {
                return;
            }
            else
            {
                Player.transform.Translate(new Vector3(Mathf.Max(data.delta.x, -3f) * Power, 0, 0));
                //하기
            }
        }
        if (Mathf.Abs(Player_rb.position.x) <= LimitX) return;
        My_Clamp();
    }

    void My_Clamp()
    {
        int i = -1;
        if (Player_rb.position.x > 0) i = 1;
        Player.transform.position = new Vector3((LimitX-0.001f) * i, Player_rb.position.y, Player_rb.position.z);
    }

    public void Click()
    {
        if (!CanJump) return;
        if (On_Drag) { On_Drag = false; return;}
        StartCoroutine(Up());
    }
    IEnumerator Up()
    {
        for(float i=0;i<0.3f;i = i+ Time.deltaTime*1.3f)
        {
            Player.transform.Translate(new Vector3(0, 0, i));
        yield return null;
        }
    }

}

