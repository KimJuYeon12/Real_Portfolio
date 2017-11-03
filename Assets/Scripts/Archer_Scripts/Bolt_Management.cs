using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt_Management : MonoBehaviour {



    Rigidbody rb;
    Vector3 Movement;
    int speed;
    public string Bolt_attribute;


    void Awake()
    {   
        rb = GetComponent<Rigidbody>();
        speed = 5;
        Bolt_attribute = "Nomal";
    }
    
    // Use this for initialization
	void Start () {
        Destroy(gameObject, 4f);
        //StartCoroutine(Destroy_Bolt());//일정 시간이 지난후 오브젝트 제거(맵에서 안보이는 곳까지 간 경우)
	}
	
	// Update is called once per frame
	void Update () {
            rb.velocity = rb.transform.forward * speed;
        
    }

    IEnumerator Destroy_Bolt()
    {
        yield return new WaitForSeconds(4f);
        
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && Bolt_attribute == "Guided")//태그가 타겟이고 유도샷이었을 경우 이곳에 진입
        {
            //Debug.Log("들어옴");
            //gameObject.SetActive(false);
            //Destroy(gameObject, 1f);
            //가이드샷의 Lerp메소드가 볼트가 소멸후에도 볼트에 접근을 지속적으로 함 -> 지워진 오브젝트에 지속적으로 접근을 해서 널익셉션이 뜸
            //해결: 바로 지우지않고 게임상에서만 보이지않으면 되므로 비활성화 시킨 후 1초후에 게임오브젝트를 제거함
        }
        else if (other.tag != "Player"  && other.tag != "Bolt" && Bolt_attribute == "Nomal")
        {
            Debug.Log("aaa");
            Destroy(gameObject);
        }
    }
}
