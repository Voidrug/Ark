using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    [Serializable]
    public abstract class AccumulatorMeta : ScriptableObject
    {
        [Header("- 매개변수")]
        [SerializeField] protected float paramValue;

        public abstract float Calculations();
        protected float Calculation(float value, float param, Operator oper)
        {
            if (oper == Operator.Add)
            {
                return value + param;
            }
            else if (oper == Operator.Multiply)
            {
                return value * param;
            }
            else if (oper == Operator.AdditiveMultiply)
            {
                return value + (param * value);
            }

            return value;
        }
    }
}
