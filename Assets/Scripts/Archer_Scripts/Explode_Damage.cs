using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Explode_Damage : MonoBehaviour {

    MoveTest Enemy_Move_Scripts;
    Rigidbody Enemy_rb;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("폭탄 사거리에 들어옴");
        //여기서 데미지주고 밀어내자
        //그 후 몇초정도 기절 후 안죽은 적들은 다시 내려오는 걸로
        //전사의 함성스킬과 많이 흡사함
        if (other.tag != "Enemy") return;
        Enemy_Move_Scripts = other.GetComponent<MoveTest>();
        Enemy_Move_Scripts.Is_Velocity = false;//적의 움직임을 중단시킨다.
        Rigidbody rb = other.GetComponent<Rigidbody>();
        Enemy_rb = rb;
        rb.velocity = Vector3.zero;
        Enemy_rb.AddExplosionForce(500f, transform.position, 5f);
        Debug.Log("aaa");


        //여기서 코루틴 써서 0.5초올라가고 0.5초기절후 다시 내려오는걸로
        StartCoroutine(After_Collider());
    }

    IEnumerator After_Collider()
    {//여기서 코루틴 써서 0.5초올라가고 0.5초기절후 다시 내려오는걸로
        yield return new WaitForSeconds(0.5f);//0.5초 동안 올라감
        Enemy_rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(1f);//1초기절
        Enemy_rb.velocity = -Vector3.forward;
    }

}
