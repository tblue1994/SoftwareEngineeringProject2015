using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.BusinessLogic
{
    public class PredictionLogic
    {
        private Charon.Learning.ForestResults<object, Tuple<int, string, string, DateTime, double, double, double, Tuple<double, int, string>>> tree;

        public PredictionLogic()
        {
            tree = RandomForestModel.trainTree();
        }

        public string ApplyTree(Tuple<int, string, string, DateTime, double, double, double, Tuple<double, int, string>> data)
        {
            return RandomForestModel.applyTree(tree, data);
        }

        public void RecreateTree()
        {
            tree = RandomForestModel.trainTree();
        }


    }
}