using Player;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using static UnityEngine.CompositeCollider2D;
using static UnityEngine.UI.CanvasScaler;

namespace Data
{
    [Serializable]
    public class Weapon : Base
    {
        [Header("- �ڽ� ������")]
        [SerializeField] private EffectRoot EffectRootPrefab;

        [Header("- ��Ÿ ������")]
        [SerializeField] private WeaponMeta weaponMeta;

        [Header("- �ΰ��� ������")]
        [SerializeField,ReadOnly] private WeaponData weaponData;

        private List<BoxCollider2D> weaponGrids = new List<BoxCollider2D>();
        private List<Unit> targetUnits = new List<Unit>();
        private bool isWeaponStart;
        private bool isUseCoroutine;

        public override BaseMeta GetMeta() => weaponMeta;

        void Start()
        {
            //������ 1ȸ�� �ʱ�ȭ(�ܺ��Էµ�����)
            CreateRange();
        }
        public override void CreateChild(Team team)
        {
            //�� ����
            gameObject.layer = (int)team + 11;
        }
        public override void UpdateDataAll()
        {
            //������Ʈ ����
            UpdateData();
        }
        public override void UpdateData()
        {
            //������ �ʱ�ȭ
            isWeaponStart = false;
            isUseCoroutine = false;

            WeaponData data = weaponMeta.GetWeaponData();
            weaponData.SetAtkSpeed(data.GetAtkSpeed());
            weaponData.SetDisable(data.GetDisable());
            weaponData.SetPeriod(data.GetPeriod());
            targetUnits.Clear();
        }
        public override void StartEvent()
        {

        }

        public override void PreDestroy()
        {
            DestroyManager.Inst().Destroy(this);
        }
        public override void LastDestroy()
        {
            if (gameObject.activeSelf == false)
                return;

            transform.parent = null;
            isWeaponStart = false;

            PoolManager.Inst().ReleaseObj(gameObject, weaponMeta);
        }

        private void CreateRange()
        {
            if (weaponMeta.GetRange().GetRangeType() == RangeType.Grid)
            {
                List<Vector2Int> grid = weaponMeta.GetRange().GetGridRange();

                for (int i = 0; i < grid.Count; i++)
                {
                    BoxCollider2D collider2D = gameObject.AddComponent<BoxCollider2D>();
                    collider2D.usedByComposite = true;
                    collider2D.offset = new Vector2(grid[i].x, grid[i].y);
                }

                CompositeCollider2D com2D = gameObject.AddComponent<CompositeCollider2D>();
                com2D.isTrigger = true;
                com2D.geometryType = GeometryType.Polygons;
            }
            else if(weaponMeta.GetRange().GetRangeType() == RangeType.Circle && weaponMeta.GetRange().GetCircleRange() != 0.0f)
            {
                Assert.AreNotEqual(0.0f, weaponMeta.GetRange().GetCircleRange(), "���� ������ 0�̶� �浹ü�� �������� �ʽ��ϴ�." + weaponMeta.name);
                CircleCollider2D collider2D = gameObject.AddComponent<CircleCollider2D>();
                collider2D.radius = weaponMeta.GetRange().GetCircleRange();
                collider2D.isTrigger = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Unit unitCol = collision.transform.GetComponent<Unit>();

            if (unitCol == null)
            {
                Debug.Log("���ּӼ��� ���� ���(" + collision.name + ")�� �����˴ϴ�");
                return;
            }

            //���ͷ� ���� ��� ���� ���� �˻�
            int oneselfTeam = gameObject.layer - 11;
            int targetTeam = collision.gameObject.layer - 6;

            Aliance aliancePlayer = PlayerManager.Inst().GetAliance(oneselfTeam, targetTeam);
            Aliance alianceMeta = weaponMeta.GetAliance();

            if ((aliancePlayer & alianceMeta) == 0)
                return;

            //�浹�� ������Ʈ ����Ʈ�� �߰��� ����
            targetUnits.Add(collision.GetComponent<Unit>());
            targetUnits.Sort((p1, p2) => p2.GetData().GetAtkPeriod().CompareTo(p1.GetData().GetAtkPeriod()));

            isWeaponStart = true;

            if (isUseCoroutine == false)
            {
                isUseCoroutine = true;
                WeaponStart(rootBase);
                StartCoroutine(WeaponAttack());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //�浹ü ������
            targetUnits.Remove(collision.GetComponent<Unit>());

            if (targetUnits.Count == 0)
            {
                isWeaponStart = false;
            }            
        }

        private void WeaponStart(Root target)
        {
            rootBase.EventStart(MessageType.WeaponStart, weaponMeta, "", rootBase, null, target);
        }

        private IEnumerator WeaponAttack()
        {
            while (isWeaponStart == true)
            {
                for(int i = 0; i < targetUnits.Count; i++)
                {
                    Base RootObj = PoolManager.Inst().GetObj(EffectRootPrefab.gameObject, this, ((UnitRoot)rootBase).GetUnitRootData().GetTeam());
                    RootObj.GetComponent<EffectRoot>().EffectOrder(rootBase.transform, RootObj.transform, targetUnits[i].GetRoot().transform);
                }

                yield return new WaitForSeconds(weaponData.GetAtkSpeed());
            }

            isUseCoroutine = false;

            yield return null;
        }
    }
}

