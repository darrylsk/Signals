using System;

namespace Signals.CoreLayer.Attributes;

    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : Attribute
    {
        public Type RelatedType { get; }
        public ForeignKeyAttribute(Type relatedType)
        {
            RelatedType = relatedType;
        }
    }