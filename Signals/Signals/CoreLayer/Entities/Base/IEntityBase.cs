using SQLite;

namespace Signals.CoreLayer.Entities.Base;

        public interface IEntityBase<TId>
    {
        [PrimaryKey]
        TId Id { get; set; }
    }
