using System;
using StubbUnity.Unity.View;

namespace Client.Scripts.Grids.Views
{
    public class TileViewLink : EcsViewLink
    {
        [NonSerialized]
        public int GridX;
        [NonSerialized]
        public int GridY;
    }
}