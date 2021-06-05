using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TamagotchiLibrary
{
    public class GraphicFactory
    {
        public GraphicProvider GetGraphicProvider(string PATH = @".\..\..\Resources.txt")
        {
            using (StreamReader file = new StreamReader(PATH))
            {
                string ln;
                List<List<List<string>>> models = new List<List<List<string>>>();
                List<List<string>> model = new List<List<string>>();
                
                int i = 0;
                while ((ln = file.ReadLine()) != null)
                {
                    var t = ln.Split('.');

                    if (ln == "" && model.Count > 0)
                    {
                        models.Add(model);
                        model = new List<List<string>>();
                    }
                    else
                    {
                        ProceedResource(t, model);
                    }
                }
                file.Close();

                return new GraphicProvider(models);
            }
        }

        private void ProceedResource(string[] resourceLine, List<List<string>> to)
        {
            for (int i = 0; i < resourceLine.Length; i++)
            {
                if (String.IsNullOrEmpty(resourceLine[i]))
                    continue;
                if (to.Count - 1 < i)
                    to.Add(new List<string>());
                to[i].Add(resourceLine[i]);
            }
        }
    }
}
