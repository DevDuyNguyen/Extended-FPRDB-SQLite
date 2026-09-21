using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainObject
{
    public class BaseCartesianProductQueryData:QueryData
    {
        public List<SelectField> selectList;
        public CartesianProductList cartesianProductList;
        public SelectionCondition selectionCondition;
        public FPRDBSchema schema;

        public BaseCartesianProductQueryData(List<SelectField> selectList, CartesianProductList cartesianProductList, SelectionCondition selectionCondition)
        {
            this.selectList = selectList;
            this.cartesianProductList = cartesianProductList;
            this.selectionCondition = selectionCondition;
        }
        public override FPRDBSchema getSchema() => this.schema;


    }
}
