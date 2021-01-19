using Identity.API.Utils.Enums;
using System.Collections.Generic;

namespace Identity.API.Models.UserSettings
{
    public class UserSettings
    {
        public string UserId { get; private set; }
        public Visibility PhoneNumberVisibility { get; private set; }
        public Visibility EmailVisibility { get; private set; }
        public Visibility BirthdateVisibility { get; private set; }
        public bool CanGetMessagesFromStrangers { get; private set; }
        public ICollection<ApplicationUser> IgnoreList { get; private set; }

        #region Set Methods

        public static UserSettings Create(string userId, Visibility phoneNumberVisibility, Visibility emailVisibility, Visibility birthdateVisibility, bool canGetMessagesFromStrangers, ICollection<ApplicationUser> ignoredUsers)
        {
            return new UserSettings
            {
                UserId = userId,
                PhoneNumberVisibility = phoneNumberVisibility,
                EmailVisibility = emailVisibility,
                BirthdateVisibility = birthdateVisibility,
                CanGetMessagesFromStrangers = canGetMessagesFromStrangers,
                IgnoreList = new HashSet<ApplicationUser>(ignoredUsers)
            };
        }

        public void SetInfo(Visibility phoneNumberVisibility = default, Visibility emailVisibility = default, Visibility birthdateVisilbity = default, bool canGetMessagesFromStrangers = default, ICollection<ApplicationUser> ignoredList = default)
        {
            PhoneNumberVisibility = phoneNumberVisibility == Visibility.NotSet ? PhoneNumberVisibility : phoneNumberVisibility;
            EmailVisibility = emailVisibility == Visibility.NotSet ? EmailVisibility : emailVisibility;
            BirthdateVisibility = birthdateVisilbity == Visibility.NotSet ? BirthdateVisibility : birthdateVisilbity;
            CanGetMessagesFromStrangers = canGetMessagesFromStrangers == default ? CanGetMessagesFromStrangers : canGetMessagesFromStrangers;
            IgnoreList = ignoredList.Count == 0 ? IgnoreList : new HashSet<ApplicationUser>(ignoredList);
        }

        #endregion Set Methods
    }
}