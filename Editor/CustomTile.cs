using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TilemapGenerator
{
    public class CustomTile
    {
        public Vector3Int localPosition { get; set; }
        public int id { get; set; }
        public string guid { get; set; }
        public long localId { get; set; }
    }
}
