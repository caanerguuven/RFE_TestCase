using System.ComponentModel;

namespace RFECase.Domain
{
    public enum EqualityStatus
    {
        [Description("one of the inputs were null")]
        AreNull,
        [Description("inputs were equal")]
        AreEqual,
        [Description("inputs are of different size !")]
        AreNotEqualInSize,
        [Description("inputs have different contents !")]
        AreNotEqualInContent

    }
}
