using System;

namespace OnlineStore.Core.Providers.Providers
{
    public class DatetimeProvider
    {
        public virtual DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
