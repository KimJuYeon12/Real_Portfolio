using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab_Swing : MonoBehaviour {

    public static Rigidbody Enemy_rb;
    public Collider Enemy;
    public float Power;
    SphereCollider col;
    float Alpha = 0;
    MoveTest Enemy_Move_Scripts;
    bool Is_Start = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy" || Is_Start) return;
        Is_Start = true;
        Enemy = other;
        Enemy_rb = other.GetComponent<Rigidbody>();

        Enemy_Move_Scripts = other.GetComponent<MoveTest>();
        if (Enemy_Move_Scripts == null) { Debug.Log("MoveTest못받음");return; }
        //다른 에너미오브젝트가 콜라이더에 부딫힌경우 해당스크립트가 없다면 발생하는 오류
        Enemy_Move_Scripts.Is_Velocity = false;
        Enemy_rb.velocity = Vector3.zero;

        Enemy_rb.transform.SetParent(transform);

        //0.5초까지 
        /////////////////////////////////////////////////////////////
        //other.transform.position = Vector3.zero;
    }


    private Grab_Swing()
    {
        Power = 1f;
    }

    private void AddForce()
    {
        if (Power >= 20f) return;
        Power = Power + 0.1f;
    }


    public void Fire()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            
            col = gameObject.GetComponent<SphereCollider>();
            col.enabled = true;
        }
        else if (Input.GetButton("Fire2"))//계속누르고있는 상태
        {
            if (Enemy_rb == null) return;
            AddForce();//누르고있는만큼 힘을 누적시킨다.
            gameObject.transform.Rotate(0,5+ Alpha, 0);
            Alpha = Alpha + 0.1f;
            if (Alpha > 10) Alpha = 10;


            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position,new Vector3(0,1,0), Time.deltaTime*2);
            //돌리고있는 중에는 부딪히고있는 상태이므로 무적화시킨다.
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            col.enabled = false;
            if (Enemy_rb == null)return;
            Enemy_rb.AddForce(new Vector3(0, 0, Power), ForceMode.Impulse);
            Enemy_rb.transform.SetParent(null);//부모에서 제거함으로써 날아갈때 부모의 영향을 안받게한다.
            Enemy_rb = null;//추가적으로 물리적인 작용을 못하게 만듬
            Is_Start = false;
        }
    }

}
