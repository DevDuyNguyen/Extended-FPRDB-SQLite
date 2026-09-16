using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainObject
{
    internal class TupleMembershipDegreeModifyData:ModifyData
    {
        private float lower_membership_degree, upper_membership_degree;

        public TupleMembershipDegreeModifyData(float lower_membership_degree, float upper_membership_degree, string relation, string assignedField, SelectionCondition selectionCondition) : base(relation, assignedField, selectionCondition)
        {
            this.lower_membership_degree = lower_membership_degree;
            this.upper_membership_degree = upper_membership_degree;
        }
        public override object getAssignValue()
        {
            return (this.lower_membership_degree, this.upper_membership_degree);
        }
    }   
}
