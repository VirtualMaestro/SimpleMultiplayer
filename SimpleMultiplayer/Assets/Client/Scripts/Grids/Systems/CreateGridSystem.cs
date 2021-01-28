using System.Runtime.CompilerServices;
using Client.Scripts.Grids.Components;
using Client.Scripts.Grids.Views;
using Client.Scripts.Levels.Components;
using Client.Scripts.Levels.SO;
using Client.Scripts.Pathfinding;
using DG.Tweening;
using Leopotam.Ecs;
using StubbUnity.StubbFramework.Extensions;
using UnityEngine;

namespace Client.Scripts.Grids.Systems
{
    public class CreateGridSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<CreateGridEvent> _createGridFilter = null;
        private readonly EcsFilter<LevelSettingsComponent> _levelSettingsFilter = null;

        public void Run()
        {
            if (_createGridFilter.IsEmpty())
                return;

            _CreateGrid(_createGridFilter.Single().GridHolder, _levelSettingsFilter.Single().LevelSettings);
        }

        private void _CreateGrid(GameObject gridHolder, LevelSettingsSO levelSettings)
        {
            var grid = new GridBase(levelSettings.columns, levelSettings.rows);
            Random.InitState(9762345);

            for (var i = 0; i < levelSettings.columns; i++)
            {
                for (var j = 0; j < levelSettings.rows; j++)
                {
                    var isWalkable = Random.Range(0, 100) > 10;

                    var node = new Node(i, j, isWalkable);

                    grid.AddNode(node);

                    _CreateTile(node, levelSettings, gridHolder.transform);


                    if (!isWalkable)
                        _CreateObstacle(node, levelSettings, gridHolder.transform);
                }
            }

            _CreateBackground(levelSettings, gridHolder.transform);

            _world.NewEntity().Get<GridComponent>().Grid = grid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _CreateTile(Node node, LevelSettingsSO levelSettings, Transform gridHolder)
        {
            var tilePrefab = node.IsWalkable ? levelSettings.tileWalkablePrefab : levelSettings.tileImpassablePrefab;
            var posX = node.X * levelSettings.cellSize;
            var posZ = node.Y * levelSettings.cellSize;
            var tile = Object.Instantiate(tilePrefab, new Vector3(posX, 0, posZ), Quaternion.identity, gridHolder);
            var tileLink = tile.GetComponent<TileViewLink>();
            tileLink.GridX = node.X;
            tileLink.GridY = node.Y;
        }

        private void _CreateObstacle(Node node, LevelSettingsSO levelSettings, Transform gridHolder)
        {
            var posX = node.X * levelSettings.cellSize;
            var posZ = node.Y * levelSettings.cellSize;
            var obstaclePrefab =
                levelSettings.obstaclePrefabs[Random.Range(0, levelSettings.obstaclePrefabs.Length - 1)];

            Object.Instantiate(obstaclePrefab, new Vector3(posX, 0, posZ), Quaternion.identity, gridHolder);
        }

        private void _CreateBackground(LevelSettingsSO levelSettings, Transform parent)
        {
            var width = (levelSettings.columns - 1) * levelSettings.cellSize;
            var height = (levelSettings.rows - 1) * levelSettings.cellSize;
            var back = _CreatePlane(width, height, levelSettings.backgroundMaterial);
            back.transform.SetParent(parent);
            back.transform.SetPositionAndRotation(new Vector3(0, 0, height), Quaternion.Euler(-90, 0, 0));
            back.GetComponent<MeshRenderer>().material.DOColor(Color.black, 2f).SetLoops(-1, LoopType.Yoyo);
        }

        private GameObject _CreatePlane(float width, float height, Material material, Transform parent = null)
        {
            var go = new GameObject("Background");
            var mf = go.AddComponent<MeshFilter>();
            var mr = go.AddComponent<MeshRenderer>();
            var mesh = new Mesh();

            var vertices = new Vector3[4];
            vertices[0] = new Vector3(0, 0, 0);
            vertices[1] = new Vector3(width, 0, 0);
            vertices[2] = new Vector3(width, height, 0);
            vertices[3] = new Vector3(0, height, 0);
            mesh.SetVertices(vertices);

            var uv = new Vector2[4];
            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(0, 1);
            uv[2] = new Vector2(1, 1);
            uv[3] = new Vector2(1, 0);
            mesh.SetUVs(0, uv);

            var triangles = new[] {0, 1, 2, 0, 2, 3};
            mesh.SetTriangles(triangles, 0);

            mf.mesh = mesh;
            mr.material = material;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            if (parent != null)
                go.transform.SetParent(parent);

            return go;
        }
    }
}