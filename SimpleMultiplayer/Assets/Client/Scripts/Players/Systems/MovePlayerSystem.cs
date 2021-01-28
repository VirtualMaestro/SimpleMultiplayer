using System.Collections.Generic;
using Client.Scripts.Extensions;
using Client.Scripts.Grids.Components;
using Client.Scripts.Levels.Components;
using Client.Scripts.Pathfinding;
using Client.Scripts.Players.Components;
using DG.Tweening;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace Client.Scripts.Players.Systems
{
    public class MovePlayerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovePlayerEvent> _movePlayerFilter = null;
        private readonly EcsFilter<GridComponent> _gridFilter = null;
        private readonly EcsFilter<PlayerComponent> _playerFilter = null;
        private readonly EcsFilter<LevelSettingsComponent> _levelSettingsFilter = null;

        public void Run()
        {
            if (_movePlayerFilter.IsEmpty()) return;

            var move = _movePlayerFilter.Single();
            var grid = _gridFilter.Single().Grid;
            var player = _playerFilter.Single().Player;
            var levelSettings = _levelSettingsFilter.Single().LevelSettings;
            var playerTransform = player.transform;
            var playerPosition = playerTransform.GetGridPosition(levelSettings.cellSize);
            var path = new List<Node>();
            var start = new Node((int) playerPosition.x, (int) playerPosition.y);
            var end = new Node(move.GridX, move.GridY);

            if (Pathfinder.FindPath(grid, start, end, ref path))
            {
                var position = playerTransform.position;
                var vertices = _Convert(path, levelSettings.cellSize, position.y);
                var duration = vertices.Length * levelSettings.playerSettings.playerSpeed;
                var direction = (vertices[vertices.Length - 1] - position).normalized;
                var rotation = Quaternion.LookRotation(direction);

                DOTween.Sequence()
                    .Append(playerTransform.DORotate(rotation.eulerAngles, 0.3f))
                    .Append(playerTransform.DOPath(vertices, duration).SetLookAt(1.0f, Vector3.forward, Vector3.up));
            }
        }

        private Vector3[] _Convert(List<Node> path, float cellSize, float playerPosY)
        {
            var vectors = new Vector3[path.Count];
            var i = 0;

            foreach (var node in path)
            {
                var posX = node.X * cellSize;
                var posZ = node.Y * cellSize;

                vectors[i++] = new Vector3(posX, playerPosY, posZ);
            }

            return vectors;
        }
    }
}