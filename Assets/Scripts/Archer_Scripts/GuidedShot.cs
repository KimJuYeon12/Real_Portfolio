using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

public class GuidedShot : ArcherShot//유도샷
{
    //int min_idx = 0;
    public GameObject[] TargetArr;
    AnimationCurve GuidedShot_ac;
    Vector3 Setting_Angle;//총구의 위치 초기화
    Vector3 Setting_Position;

    float Angle;//총알사이의 각도
    float Distance;
    Rigidbody Spawn_rb;

    public GuidedShot(GameObject Bolt, GameObject Shot_Spawn, GameObject Shot_Spawn_Point, int Shot_Level, AnimationCurve GuidedShot_ac)
    :base(Bolt,Shot_Spawn, Shot_Spawn_Point,Shot_Level)
    {
        this.GuidedShot_ac = GuidedShot_ac;

        //애니메이션 연결
        

        Angle =30f;
        Distance = 2;

        Setting_Angle.Set(0, -(Angle * (Shot_Level - 1)) / 2,0 );
        //Setting_Angle.Set(0, 0, -(Angle * (Shot_Level - 1)) / 2);
        Spawn_rb = Shot_Spawn.GetComponent<Rigidbody>();
        

    }


    public override void Shot(){}

    public int Set_Target()
    {
        TargetArr = GameObject.FindGameObjectsWithTag("Enemy");

        float min_Len = Vector3.Distance(Shot_Spawn.transform.position, TargetArr[0].transform.position);
        float new_Len;
        int min_idx=0;

        for (int i = 1; i < TargetArr.Length; i++)
        {
            new_Len = Vector3.Distance(Shot_Spawn.transform.position,TargetArr[i].transform.position);
            if (new_Len < min_Len)
            {
                min_idx = i;
                min_Len = new_Len;
            }
        }
        return min_idx;
    }





    public GameObject[] Generate_Bolt(int idx)
    {
        
        Quaternion newRotation = Quaternion.LookRotation((TargetArr[idx].transform.position - Shot_Spawn.transform.position));
        Shot_Spawn.transform.localRotation = newRotation;
        //총구가 타겟을 바라보게 만듬
       

        Shot_Spawn.transform.Rotate(Setting_Angle);
        //바라본상태에서 총구의 각도 설정





        GameObject[] Guided_Bolt = new GameObject[Shot_Level];//생성될 숫자만큼의 유도탄알을 만든다.

        for (int i = 0; i < Shot_Level; i++)
        {
            Guided_Bolt[i] = MonoBehaviour.Instantiate(Bolt, Shot_Spawn_Point.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));// Spawn_rb.rotation);
            Shot_Spawn.transform.Rotate(new Vector3(0, Angle,0 ));
        }


        Shot_Spawn.transform.SetPositionAndRotation(Shot_Spawn.transform.position, Quaternion.Euler(0, 0, 0));
        return Guided_Bolt;
    }




    public IEnumerator GuidedShot_(GameObject Bolt, int idx, GameObject[] BoltArr)
    {

            for (float j = 0.0f; j < 2f; j = j + Time.deltaTime)
            {
                Bolt.transform.position = Vector3.Lerp(Bolt.transform.position, TargetArr[idx].transform.position, 0.1f);
                yield return null;

                if (Vector3.Distance(Bolt.transform.position, TargetArr[idx].transform.position) < 0.5f)
                {
                    Destroy_AllBolt(BoltArr);
                    break;
                }

            }
    }

    void Destroy_AllBolt(GameObject[] BoltArr)
    {
        for (int i = 0; i < BoltArr.Length; i++)
        {
            BoltArr[i].SetActive(false);
            MonoBehaviour.Destroy(BoltArr[i], 1f);
        }
    }

    //문제점
    //총알이 박히고 적이 사라진 후 남은 총알은 어떻게 처리?


}

