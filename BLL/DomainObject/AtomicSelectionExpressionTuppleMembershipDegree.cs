using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DomainObject
{
    internal class AtomicSelectionExpressionTuppleMembershipDegree: SelectionExpression
    {

        public override List<SelectionExpression> getAtomicSelectionExpression() => new List<SelectionExpression> { this };
        public override List<float> calculateProbabilisticInterpretation(Scan currentTuple, FPRDBSchema schema)
        {
            return new List<float> { currentTuple.getCurrentTupleMembershipDegree().Item1, currentTuple.getCurrentTupleMembershipDegree().Item2 };
        }
        public override List<string> getMentionedAttributes() => new List<string> { };

    }
}
