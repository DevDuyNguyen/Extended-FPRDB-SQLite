using BLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainObject
{
    public class CartesianProductList
    {
        public List<string> relationList;
        public List<ProbabilisticCombinationStrategy> probCombinationStrategyList;

        public CartesianProductList(List<string> relationList, List<ProbabilisticCombinationStrategy> probCombinationStrategyList)
        {
            this.relationList = relationList;
            this.probCombinationStrategyList = probCombinationStrategyList;
        }
        public List<string> getRelationList()
        {
            return this.relationList;
        }
    }
}
