using System;
using System.Linq;
using Godot;
using HolyWar.Scripts.Maps;

namespace HolyWar.Scripts.Game;

public partial class GameMap : Node
{
	public Map Map;
	private static PackedScene TileScene => field ??= ResourceLoader.Load<PackedScene>("res://Scenes/Tile.tscn");

	private static readonly Func<Vector2,Vector3> ToRenderCoord = coord =>
		new Vector3(0.5f * (2 * coord.Y + float.Abs(coord.X)) * 1.732f, 0, coord.X * 1.5f);
	public void DrawMap()
	{
		if (Map == null) return;
		for (var x = -Map.Size + 1; x < Map.Size; x++)
		{
			var y = 0;
			foreach (var tile in Map.Tiles[x + Map.Size - 1])
			{
				var coord = new Vector2(x, y);
				y++;
				var tileScene = TileScene.Instantiate<MeshInstance3D>();
				tileScene.Position = ToRenderCoord(coord);
				tileScene.Mesh = Globals.Global.Terrains[tile.MainTerrain].Mesh;
				AddChild(tileScene);
			}

			//GD.Print(string.Join(',', Map.Tiles[x + Map.Size - 1].Select(t => t.MainTerrain)));
		}
	}
}