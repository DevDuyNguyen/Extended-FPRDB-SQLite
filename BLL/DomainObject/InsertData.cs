using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainObject
{
    public class InsertData
    {
        public string relation;
        public List<string> fieldList;
        public List<FuzzyProbabilisticValueParsingData> fuzzyProbabilisticValues;
        public float lower_tuple_membership_deg, upper_tuple_membership_deg;

        public InsertData(string relation, List<string> fieldList, List<FuzzyProbabilisticValueParsingData> fuzzyProbabilisticValues, float lower_tuple_membership_deg, float upper_tuple_membership_deg)
        {
            this.relation = relation;
            this.fieldList = fieldList;
            this.fuzzyProbabilisticValues = fuzzyProbabilisticValues;
            this.lower_tuple_membership_deg = lower_tuple_membership_deg;
            this.upper_tuple_membership_deg = upper_tuple_membership_deg;
        }
        public FuzzyProbabilisticValueParsingData getInsertDataByFieldName(string fldName)
        {
            for(int i=0; i<fieldList.Count; ++i)
            {
                if (this.fieldList[i] == fldName)
                    return this.fuzzyProbabilisticValues[i];
            }
            return null;
        }
    }
}
