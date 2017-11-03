using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_Attack_Management : MonoBehaviour {

    public GameObject Bolt;
    public GameObject Shot_Spawn;
    public static GameObject Shot_Spawn_Point;
    ///////////////////////////////////////////////////////////////////

    MultiShot Mul;
    public int MultiShot_Lev;
    ///////////////////////////////////////////////////////////////////



    GuidedShot Guided;
    public int GuidedShot_Lev;
    public AnimationCurve GuidedShot_ac;
    public GameObject Guided_Enemy;
    /// ///////////////////////////////////////////////////////////////////


    Layzer_Trap_Shot Lazer_Shot;
    public GameObject Lazer;
    public int LazerShot_Lev;

    /// ////////////////////////////////////////////////////////////////

    Explode_Trap_Shot Explode_Shot;
    public GameObject Explode;
    public int Explode_Lev;


    
    void Awake()
    {
        Shot_Spawn_Point = gameObject;
        GuidedShot_Lev = 5;
        MultiShot_Lev = 5;//우선 임의의 값을 넣음
        LazerShot_Lev = 3;
        Explode_Lev = 1;


        Mul = new MultiShot(Bolt,Shot_Spawn,gameObject,MultiShot_Lev);
        Guided = new GuidedShot(Bolt, Shot_Spawn, gameObject, GuidedShot_Lev, GuidedShot_ac);
        Lazer_Shot = new Layzer_Trap_Shot(Bolt, Shot_Spawn, gameObject, LazerShot_Lev,Lazer);
        Explode_Shot = new Explode_Trap_Shot(Bolt, Shot_Spawn, gameObject, LazerShot_Lev,Explode);

    }
	// Update is called once per frame
    void FixedUpdate () 
    {
        Guided_Shot();
    }



    void Explode_Shot_()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Explode_Shot.Shot();
        }
    }


    void Lazer_Shot_()
    {
        if (Input.GetButtonDown("Fire1") && Layzer_Trap_Shot.Is_Lazer == false)
        {
            StartCoroutine(Lazer_Shot.Set_Lazer());
        }
    }


    void Mul_Shot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Mul.Shot();
        }
    }
    



    //지금 하고있는 작엄 : 최대한 OPP의 컨셉에 맞춰서 코드 정리하기
    void Guided_Shot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //GameObject[] Bolt = new GameObject[GuidedShot_Lev];
            Guided.Generate_Bolt();

            for (int i = 0; i < GuidedShot_Lev; i++)
            {
                //Bolt_Management Attribute = Bolt[i].GetComponent<Bolt_Management>();
                //Attribute.Bolt_attribute = "Guided";
                StartCoroutine(Guided.GuidedShot_(i));
            }
        }
    }
       
    

}
