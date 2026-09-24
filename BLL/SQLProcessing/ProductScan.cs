using BLL.Common;
using BLL.DomainObject;
using BLL.Enums;
using BLL.Exceptions;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SQLProcessing
{
    public class ProductScan:Scan
    {
        private Scan s1;
        private Scan s2;
        private FPRDBSchema schema;
        private ProbabilisticCombinationStrategy probCombinationStrategy;
        private List<AbstractFuzzyProbabilisticValue> currentTuple;
        private (float, float) currentTupleLowerMembershipDegree;
        public ProductScan(Scan s1, Scan s2, FPRDBSchema schema, ProbabilisticCombinationStrategy probCombinationStrategy)
        {
            this.s1 = s1;
            this.s2 = s2;
            this.probCombinationStrategy = probCombinationStrategy;
            this.schema = schema;
            s1.next();
        }
        public void beforeFirst()
        {
            s1.beforeFirst();
            s1.next();
            s2.beforeFirst();
        }
        public bool hasField(string fldname)
        {
            return s1.hasField(fldname) || s2.hasField(fldname);
        }
        public bool next()
        {
            bool hasNext;
            if (s2.next())
            {
                
                hasNext=true;
            }
            else
            {
                s2.beforeFirst();
                hasNext = s1.next() && s2.next();
            }

            if (hasNext)
            {
                this.currentTuple = new List<AbstractFuzzyProbabilisticValue>();
                foreach (AbstractFuzzyProbabilisticValue v in s1.getCurrentTuple())
                {
                    this.currentTuple.Add(v);
                }
                foreach (AbstractFuzzyProbabilisticValue v in s2.getCurrentTuple())
                {
                    this.currentTuple.Add(v);
                }
                //calculate the current tuple's membership degree
                (float, float) t1i_membership_degree = s1.getCurrentTupleMembershipDegree();
                (float, float) t2j_membership_degree = s2.getCurrentTupleMembershipDegree();
                List<float> tmp = ProbabilisticCombinationStrategyUtilities.combine(t1i_membership_degree.Item1, t1i_membership_degree.Item2,
                    t2j_membership_degree.Item1, t2j_membership_degree.Item2, this.probCombinationStrategy);
                this.currentTupleLowerMembershipDegree.Item1 = tmp[0];
                this.currentTupleLowerMembershipDegree.Item2 = tmp[1];
            }
            else
            {
                this.currentTuple = null;
                this.currentTupleLowerMembershipDegree = (0, 9);
            }
            return hasNext;
        }

        public void close() { }
        private int getFieldIndexInTuple(string fldName)
        {
            List<Field> fields = this.schema.getFields();
            for (int i = 0; i < fields.Count; ++i)
            {
                if (fields[i].getFieldName() == fldName)
                    return i;
            }
            return -1;
        }
        public FuzzyProbabilisticValue<T> getFieldContent<T>(String fldName)
        {
            int index = getFieldIndexInTuple(fldName);
            if (index == -1)
                throw new QueryDataNotExistException($"Schema doesn't have attribute {fldName}");
            var fprobValue = this.currentTuple[index];
            if (!(fprobValue is FuzzyProbabilisticValue<T>))
                throw new InvalidCastException($"Fuzzy probabilistic value of {fldName} doesn't contain fuzzy sets defined on domain of {typeof(T).Name}");
            return (FuzzyProbabilisticValue<T>)(object)fprobValue;
        }
        //public FPRDBSchema getSchema();
        public List<AbstractFuzzyProbabilisticValue> getCurrentTuple() => this.currentTuple;

        public (float, float) getCurrentTupleMembershipDegree()
        {
            return this.currentTupleLowerMembershipDegree;
        }
    }
}
