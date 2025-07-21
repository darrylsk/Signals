using System;
using Signals.CoreLayer.Entities.Base;
using SQLite;

namespace Signals.CoreLayer.Entities
{
    public class Settings : Entity
    {
        private double _trailingStop;
        private double _defaultHighGainMultiplier;
        
        // [PrimaryKey]
        // public Guid SettingsId { get; set; }
        
        // [PrimaryKey]
        // public Guid Id { get; set; }

        /// <summary>
        /// A version number for the database, in case the database structure needs to be
        /// changed at a later date.
        /// </summary>
        public int MetadataVersion { get; set; }

        /// <summary>
        /// Default setting for whether or not to alert when a holding has doubled in value.
        /// </summary>
        public bool DefaultUseHighGainMultiplier { get; set; }

        /// <summary>
        /// Default setting for the gain threshold as a multiple of entry price.
        /// </summary>
        public double DefaultHighGainMultiplier
        {
            get => _defaultHighGainMultiplier;
            set => _defaultHighGainMultiplier = value switch
            {
                < 1.25 => 1.25,
                _ => value
            };
        }

        /// <summary>
        /// Default setting for whether or not to use a trailing stop.
        /// </summary>
        public bool DefaultUseTrailingStop { get; set; }

        /// <summary>
        /// Default value for a trailing stop, if used
        /// </summary>
        public double DefaultTrailingStop
        {
            get => _trailingStop;
            set
            {
                _trailingStop = value switch
                {
                    < 0.05 => 0.05,
                    > 0.95 => 0.95,
                    _ => value
                };
            }
        }
    }
}
