using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TilemapGenerator
{
    public class MapGenerator : MonoBehaviour
    {
        public void DrawTiles(Tilemap tilemap, string chunkName)
        {
            List<CustomTile> tiles = ReadJSON(chunkName);
            var tileSources = FindTileSources(tiles);
            Draw(tiles, tileSources, tilemap);
        }
        private List<CustomTile> ReadJSON(string chunkName)
        {
            List<CustomTile> tiles = new List<CustomTile>();

            string chunkJSON = "";
            StreamReader inp_stm = new StreamReader("Assets/" + chunkName + ".json");

            while (!inp_stm.EndOfStream)
            {
                chunkJSON += inp_stm.ReadLine();
            }
            inp_stm.Close();

            tiles = JsonConvert.DeserializeObject<List<CustomTile>>(chunkJSON);

            return tiles;
        }

        private List<Tile> FindTileSources(List<CustomTile> tiles)
        {
            var tileSources = new List<Tile>();
            foreach (var tile in tiles)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(tile.guid);
                var asset = (Tile)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Tile));
                tileSources.Add(asset);
            }
            return tileSources;
        }

        private void Draw(List<CustomTile> tiles, List<Tile> tileSources, Tilemap tilemap)
        {
            tilemap.CompressBounds();
            var boundsX = (int)tilemap.localBounds.max.x; //Tilemap rightmost x coordinates
            int additionLength = 0;                       //Length of addition

            int maxX = 0;                                 //Addition rightmost x coordinates
            //Calculate lenght of addition
            foreach (var tile in tiles)
            {
                if (tile.localPosition.x > maxX)
                {
                    additionLength = tile.localPosition.x;
                    maxX = tile.localPosition.x;
                }
            }
            //Add tiles
            for (int i = 0; i < tiles.Count && i < tileSources.Count; i++)
            {
                var pos = tiles[i].localPosition;
                tilemap.SetTile(new Vector3Int(pos.x + boundsX + additionLength, pos.y, pos.z), tileSources[i]);
            }

            tilemap.RefreshAllTiles();

            Debug.Log("Finished generating tiles!");
        }
    }
}
