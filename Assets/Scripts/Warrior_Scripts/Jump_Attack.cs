using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Jump_Attack
{
    Rigidbody Warrior_rb;
    int level;
    public Jump_Attack(GameObject Warrior,int level)
    {
        Warrior_rb = Warrior.GetComponent<Rigidbody>();
    }

    public void Shot()
    {
       //위로 힘주기
        Warrior_rb.AddForce(new Vector3(0,0,800));

        //점프시에 몹에 부딫히면 데미지를 반감시키는 기능 -> 레벨에 따라서 깎이는 데미지 다르게
    }

}

