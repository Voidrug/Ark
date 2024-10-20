using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ArithmeticData", menuName = "Base/Accumulator/ArithmeticMeta")]
public class ArithmeticMeta : AccumulatorMeta
{
    [Header("- 누산기 리스트")]
    [SerializeField] private List<Accumulator> accumulators = new List<Accumulator>();

    public override float Calculations()
    {
        float value = paramValue;        

        for(int i = 0; i < accumulators.Count; i++)
        {
            value = Calculation(value, accumulators[i].GetAccumulatorMeta().Calculations(), accumulators[i].GetOperator());                
        }

        return value;
    }
}


