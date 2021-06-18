using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TamagotchiLibrary
{
    [Serializable]
    public class GraphicProvider
    {
        private List<List<List<String>>> resources;

        private enum ContentState
        {
            Default = 0,
            Walk = 1,
            Sleep = 2,
            Death = 3,
            Heart = 4,
            Food = 5
        }


        public GraphicProvider(List<List<List<String>>> _resources)
        {
            resources = _resources;
        }

        public Drower GetStayAnimation()
        {
            return new Drower(resources[(int)ContentState.Default]);
        }

        public Drower GetWalkAnimation()
        {
            return new Drower(resources[(int)ContentState.Walk]);
        }

        public Drower GetSleepAnimation()
        {
            return new Drower(resources[(int)ContentState.Sleep]);
        }

        public Drower GetDeathAnimation()
        {
            var len = resources[(int)ContentState.Death].Count;
            var res = new List<List<string>>(len);

            for (int i = 0; i < len; i++)
            {
                var response = GetResourceAnimationByLineFormat(resources[(int)ContentState.Death][i]);
                res = res.Concat(response).ToList();
            }

            return new Drower(res);
        }

        public Drower GetHeart()
        {
            return new Drower(resources[(int)ContentState.Heart]);
        }
        public Drower GetFood()
        {
            return new Drower(resources[(int)ContentState.Food]);
        }


        private List<List<string>> GetResourceAnimationByLineFormat(List<string> resource)
        {
            var size = resource.Count;
            var jsize = size;
            var res = new List<List<string>>(size);

            for (int i = 0; i < size; i++)
            {
                res.Add(new List<string>(jsize));
                for (int j = 0; j < jsize; j++)
                {
                    res[i].Add(resource[j]);
                }
                jsize--;
            }

            return res;
        }
    }
}
