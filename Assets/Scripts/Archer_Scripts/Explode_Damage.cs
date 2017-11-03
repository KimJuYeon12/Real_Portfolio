using System.Collections;
using UnityEngine;

//폭발트랩샷을 위한 동작 표현
public class Explode_Damage : MonoBehaviour {


    
    MoveTest Enemy_Move_Scripts; //적에 대한 동작스크립트를 임시로 작성한 부분
    Rigidbody Enemy_rb; //적의 리지드바디 가져오기

    //데미지를 주고 밀어내는 기능이 여기에 들어간다.
    void OnTriggerEnter(Collider other)
    {

        //그 후 몇초정도 기절 후 안죽은 적들은 다시 내려오는 걸로
        //전사의 함성스킬과 많이 흡사함
        if (other.tag != "Enemy") return; //적이 아니라면  그냥 리턴

        Enemy_Move_Scripts = other.GetComponent<MoveTest>();//적의 스크립트 가져오기

        Enemy_Move_Scripts.Is_Velocity = false;//적의 움직임을 중단시킨다.

        Enemy_rb = other.GetComponent<Rigidbody>();//적 리지드 바디 가져옴

        Enemy_rb.velocity = Vector3.zero;//적이 벨로시티 값으로 움직인다면 해당값을 0으로

        Enemy_rb.AddExplosionForce(500f, transform.position, 5f);//충격을 준다.
        
        //여기서 코루틴 써서 0.5초올라가고 0.5초기절후 다시 내려오는걸로
        StartCoroutine(After_Collider());
    }


    private IEnumerator After_Collider()
    {
        yield return new WaitForSeconds(0.5f);//0.5초 동안 올라감
        Enemy_rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(1f);//1초기절
        Enemy_rb.velocity = -Vector3.forward;//그냥 천천히 내려오게 만듦
    }

}
