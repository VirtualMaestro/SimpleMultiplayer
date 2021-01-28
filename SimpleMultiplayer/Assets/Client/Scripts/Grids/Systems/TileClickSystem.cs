using Client.Scripts.Grids.Components;
using Client.Scripts.Players.Components;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;

namespace Client.Scripts.Grids.Systems
{
    public class TileClickSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<TileClickEvent> _tileClickFilter = null;
        private readonly EcsFilter<GridComponent> _gridFilter = null;

        public void Run()
        {
            if (_tileClickFilter.IsEmpty()) return;

            var tileView = _tileClickFilter.Single().Tile;
            var tile = tileView.Tile;

            var grid = _gridFilter.Single().Grid;
            var node = grid.GetNode(tile.X, tile.Y);

            if (!node.IsWalkable) return;

            ref var position = ref _world.NewEntity().Get<MovePlayerEvent>();
            position.TileX = tile.X;
            position.TileZ = tile.Y;
        }
    }
}