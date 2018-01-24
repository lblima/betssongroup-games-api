using System;

namespace OnlineCassino.Domain
{
    public class User: BaseEntity
    {
        protected User()
        {

        }

        public User(string name, string accountId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            if (string.IsNullOrWhiteSpace(accountId))
                throw new ArgumentException(nameof(accountId));

            Name = name;
            AccountId = accountId;
        }

        public string Name { get; set; }
        public string AccountId { get; set; }
    }
}