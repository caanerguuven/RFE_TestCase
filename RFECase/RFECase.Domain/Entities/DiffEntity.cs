using RFECase.Domain.Base.Entities;

namespace RFECase.Domain.Entities
{
    public class DiffEntity:BaseEntity
    {
        public string LeftExpression { get; set; }
        public string RightExpression { get; set; }

        public EqualityStatus EqualityStatus => string.IsNullOrWhiteSpace(LeftExpression) ||string.IsNullOrWhiteSpace(RightExpression) ? EqualityStatus.AreNull :
                                                    LeftExpression.Trim().Length != RightExpression.Trim().Length ? EqualityStatus.AreNotEqualInSize :
                                                        (LeftExpression.Trim() == RightExpression.Trim() ? EqualityStatus.AreEqual : 
                                                            EqualityStatus.AreNotEqualInContent);
    }
}
