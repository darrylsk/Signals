namespace Signals.CoreLayer.Entities.Base;

        public interface IEntityBase<TId>
    {
        TId Id { get; }
    }
