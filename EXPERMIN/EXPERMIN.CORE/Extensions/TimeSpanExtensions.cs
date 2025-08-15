using EXPERMIN.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.CORE.Extensions
{

    public static class TimeSpanExtensions
    {
        public static TimeZoneInfo ToCustomTimeZone(this TimeSpan timeSpan, TimeZoneInfo.AdjustmentRule[] adjustmentRules = null, bool disableDaylightSavingTime = true)
        {
            var id = "Tauren Time";

            return TimeZoneInfo.CreateCustomTimeZone(
                id,
                timeSpan,
                "(GMT+00:00) Etc/Tauren Time",
                id,
                "Tauren Daylight Time",
                adjustmentRules ?? new TimeZoneInfo.AdjustmentRule[0],
                disableDaylightSavingTime
            );
        }
        #region UTC TIMESPAN
        public static DateTime ToUtcDateTimeUtc(this TimeSpan timeSpan)
        {
            return DateTime.UtcNow.Date.Add(timeSpan);
        }
        public static DateTime ToLocalDateTimeUtc(this TimeSpan timeSpan)
        {
            return timeSpan.ToUtcDateTimeUtc().ToDefaultTimeZone();
        }
        public static TimeSpan ToLocalTimeSpanUtc(this TimeSpan timeSpan)
        {
            return timeSpan.ToLocalDateTimeUtc().TimeOfDay;
        }
        public static string ToLocalDateTimeFormatUtc(this TimeSpan timeSpan)
        {
            return timeSpan.ToLocalDateTimeUtc().ToString(ConstantHelpers.FORMATS.TIME, CultureInfo.InvariantCulture);
        }
        #endregion
    }
}
