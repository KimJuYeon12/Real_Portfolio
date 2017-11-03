using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MultiShot : ArcherShot
{
    Vector3 Setting_Angle;//총구의 위치 초기화
    float Angle;//총알사이의 각도


    public MultiShot(GameObject Bolt, GameObject Shot_Spawn, GameObject Shot_Spawn_Point, int Shot_Level) 
    : base(Bolt,Shot_Spawn, Shot_Spawn_Point,Shot_Level)
    {
        Angle = 10f;
        Setting_Angle.Set(0, -(Angle * (Shot_Level - 1)) / 2, 0);
        //각도에 멀티샷의 레벨만큼의 각도를 회전할 것이므로 그것의 반틈만큼 돌려서 초기각을 설정해줘야한다.
    }


    public override void Shot()
    {
        Debug.Log("멀티샷");
        Shot_Spawn.transform.SetPositionAndRotation(Shot_Spawn.transform.position, Quaternion.Euler(0, 0, 0));
        Shot_Spawn.transform.Rotate(Setting_Angle);

        for (int i = 0; i < Shot_Level; i++)
        {
            MonoBehaviour.Instantiate(Bolt, Shot_Spawn_Point.transform.position, Shot_Spawn.transform.rotation);
            Shot_Spawn.transform.Rotate(new Vector3(0, Angle, 0));

        }
        Shot_Spawn.transform.SetPositionAndRotation(Shot_Spawn.transform.position, Quaternion.Euler(0, 0, 0));
        //각도의 값을 초기화해주기 위해서 사용
    }

}

