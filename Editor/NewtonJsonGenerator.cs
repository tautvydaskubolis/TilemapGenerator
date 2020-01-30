using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TilemapGenerator
{
    public class NewtonJsonGenerator : MonoBehaviour
    {
        [MenuItem("TilemapGenerator/CreateTilemapChunk")]
        static void CreateChunk()
        {
            var tileList = new List<CustomTile>();
            var tilemap = Selection.gameObjects[0].GetComponent<Tilemap>();

            ReadTilemapData(tilemap, tileList);
            WriteToJSON(tileList);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
        private static void ReadTilemapData(Tilemap tilemap, List<CustomTile> tileList)
        {
            foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
            {
                var localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                if (!tilemap.HasTile(localPlace))
                {
                    continue;
                }

                var tile = (Tile)tilemap.GetTile(pos);
                var chunk = new CustomTile();
                chunk.localPosition = pos;
                chunk.id = tile.GetInstanceID();

                var scriptedTile = ScriptableObject.CreateInstance<NormalFloorTile>();
                tileList.Add(chunk);
            }
        }
        private static void WriteToJSON(List<CustomTile> tileList)
        {
            string path = "Packages/TilemapGenerator/Editor/TilemapChunks/" + Selection.gameObjects[0].name + ".json";

            if (File.Exists(path)) File.Delete(path);

            var JSON = JsonConvert.SerializeObject(tileList);

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(JSON);
                }
            }
        }
    }
}
