using System;

namespace OnlineCassino.Domain
{
    public class DeviceType: BaseEntity
    {
        protected DeviceType()
        {

        }

        public DeviceType(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException(nameof(description));

            Description = description;
        }

        public string Description { get; set; }
    }
}