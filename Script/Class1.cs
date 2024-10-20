using Data;
using Level;
using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Test : MonoBehaviour
{
    public bool check;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (check)
                return;

            List<UnitRoot> unitRoots = GameManager.Inst().GetUnitRoots();

            for(int i = 0; unitRoots.Count > i; i++)
            {
                unitRoots[i].GetUnit().Damage(1200, DamageType.Physics);
            }

            check = true;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            check = false;
        }

    }
}
