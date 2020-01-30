To use add the following dependency to your project's manifest.json file:

"com.unity.tilemapgenerator": "https://github.com/tautvydaskubolis/TilemapGenerator.git"

Supported functionality:
Create JSON files for different Tilemap chunks
Add chunks to the right of the Tilemap

Limitations:
Only one Tilemap can be selected to generate JSON
Once a JSON file is created, there is no easy way to delete it
Tile Assets that are used in a chunk cannot be deleted

How to use:

1. Saving a JSON
  1.1 Select a Tilemap GameObject in the Scene
  1.2 Generate a JSON (Tilemap Generator -> Generate Tilemap Chunk)
2. Adding a chunk to the Tilemap
  2.1 Create a new MonoBehaviour
  2.2 Create an instance of Tilemap Generator
  2.3 Call TilemapGenerator.DrawTiles()
