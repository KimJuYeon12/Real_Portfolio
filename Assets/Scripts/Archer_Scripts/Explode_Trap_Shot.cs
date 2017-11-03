
using UnityEngine;


public class Explode_Trap_Shot : ArcherShot
{
    GameObject Explode_Trap;


    public Explode_Trap_Shot(GameObject Bolt, GameObject Shot_Spawn, GameObject Shot_Spawn_Point, int Shot_Level,GameObject Explode_Trap) 
    : base(Bolt,Shot_Spawn, Shot_Spawn_Point,Shot_Level)
    {
        this.Explode_Trap = Explode_Trap;
    }



    public override void Shot()
    {
        MonoBehaviour.Instantiate(Explode_Trap, Shot_Spawn_Point.transform.position, Shot_Spawn_Point.transform.rotation);
    }

}

