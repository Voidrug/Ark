using Data;
using Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoSingle<GameManager>
{
    [SerializeField] private List<UnitRoot> rootObjects = new List<UnitRoot>();

    public List<UnitRoot> GetUnitRoots() => rootObjects;
    public UnitRoot GetUnitRoot(int index) => rootObjects.ElementAtOrDefault(index);

    public GameManager()
    {
        instance = this;
    }

}
