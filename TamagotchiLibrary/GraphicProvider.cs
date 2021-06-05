using System;
using System.Collections.Generic;
using System.Text;

namespace TamagotchiLibrary
{
    public class GraphicProvider
    {
        private List<List<List<String>>> resources;

        private enum ContentState
        {
            Default = 0,
            Walk = 1,
            Heart = 2
        }


        public GraphicProvider(List<List<List<String>>> _resources)
        {
            resources = _resources;
        }

        public Drower GetStayAnimation()
        {
            return new Drower(new List<List<List<String>>> { resources[(int)ContentState.Default] });
        }

        public Drower GetWalkAnimation()
        {
            return new Drower(new List<List<List<String>>> { resources[(int)ContentState.Walk] });
        }

        public Drower GetHeart()
        {
            return new Drower(new List<List<List<String>>> { resources[(int)ContentState.Heart] });
        }
    }
}
