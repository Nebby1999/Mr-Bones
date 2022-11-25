using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MrBones
{
    [RequireComponent(typeof(Tilemap), typeof(TilemapCollider2D))]
    public class BreakableTilemap : MonoBehaviour, IBreakable
    {
        public Tilemap Tilemap { get; private set; }
        public TilemapCollider2D Collider { get; private set; }

        private void Awake()
        {
            Tilemap = GetComponent<Tilemap>();
            Collider = GetComponent<TilemapCollider2D>();
        }

        public bool TakeDamage(BreakablesCollisionInfo info)
        {
            var collision = info.collision;
            var t = info.collision.GetContact(0).point;
            int radiusInt = 2;
            for(int i = -radiusInt; i <= radiusInt; i++)
            {
                for(int j = -radiusInt; j <= radiusInt; j++)
                {
                    Vector3 checkCellPos = new Vector3(t.x + i, t.y + j, 0);
                    float dist = Vector3.Distance(t, checkCellPos) - 0.001f;

                    if(dist <= radiusInt)
                    {
                        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkCellPos, 0.01f);
                        if(colliders.Length > 0)
                        {
                            foreach(Collider2D collider in colliders)
                            {
                                if(collider.gameObject == gameObject)
                                {
                                    Tilemap.SetTile(Tilemap.WorldToCell(checkCellPos), null);
                                }
                            }
                        }
                    }
                }
            }
            Collider.ProcessTilemapChanges();
            return true;
        }
    }
}
