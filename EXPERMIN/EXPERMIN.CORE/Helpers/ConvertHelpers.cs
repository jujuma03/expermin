using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.CORE.Helpers
{
    public class ConvertHelpers
    {
        #region DATEPICKER CONVERTERS
        public static DateTime DatepickerToDatetime(string date)
        {
            return DateTime.ParseExact(date, ConstantHelpers.FORMATS.DATE, System.Globalization.CultureInfo.InvariantCulture);
        }

        public static DateTime DatepickerToUtcDateTime(string date)
        {
            var dt = DateTime.ParseExact(date, ConstantHelpers.FORMATS.DATE, System.Globalization.CultureInfo.InvariantCulture).ToUniversalTime();
            dt = DateTime.SpecifyKind(dt, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeToUtc(dt, FindOperatingSystemTimeZoneById(ConstantHelpers.TIMEZONEINFO.LINUX_TIMEZONE_ID, ConstantHelpers.TIMEZONEINFO.OSX_TIMEZONE_ID, ConstantHelpers.TIMEZONEINFO.WINDOWS_TIMEZONE_ID));
        }

        #endregion

        #region TIMEPICKER CONVERTERS

        public static DateTime TimepickerToDateTime(string time)
        {
            var dt = DateTime.ParseExact(time, ConstantHelpers.FORMATS.TIME, System.Globalization.CultureInfo.InvariantCulture);
            dt = DateTime.SpecifyKind(dt, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTime(dt, FindOperatingSystemTimeZoneById(ConstantHelpers.TIMEZONEINFO.LINUX_TIMEZONE_ID, ConstantHelpers.TIMEZONEINFO.OSX_TIMEZONE_ID, ConstantHelpers.TIMEZONEINFO.WINDOWS_TIMEZONE_ID), ConvertHelpers.FindOperatingSystemTimeZoneById(ConstantHelpers.TIMEZONEINFO.LINUX_TIMEZONE_ID, ConstantHelpers.TIMEZONEINFO.OSX_TIMEZONE_ID, ConstantHelpers.TIMEZONEINFO.WINDOWS_TIMEZONE_ID));
        }

        public static DateTime TimepickerToUtcDateTime(string time)
        {
            var dt = DateTime.ParseExact(time, ConstantHelpers.FORMATS.TIME, System.Globalization.CultureInfo.InvariantCulture);
            dt = DateTime.SpecifyKind(dt, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeToUtc(dt, FindOperatingSystemTimeZoneById(ConstantHelpers.TIMEZONEINFO.LINUX_TIMEZONE_ID, ConstantHelpers.TIMEZONEINFO.OSX_TIMEZONE_ID, ConstantHelpers.TIMEZONEINFO.WINDOWS_TIMEZONE_ID));
        }

        public static TimeSpan TimepickerToTimeSpan(string time)
        {
            return TimepickerToDateTime(time).TimeOfDay;
        }
        public static TimeSpan TimepickerToUtcTimeSpan(string time)
        {
            return TimepickerToUtcDateTime(time).TimeOfDay;
        }

        #endregion

        #region TIMEZONEINFO CONVERTERS

        public static TimeZoneInfo FindOperatingSystemTimeZoneById(string linuxId, string osxId, string windowsId)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return TimeZoneInfo.FindSystemTimeZoneById(windowsId);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return TimeZoneInfo.FindSystemTimeZoneById(osxId);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return TimeZoneInfo.FindSystemTimeZoneById(linuxId);
            }

            return null;
        }

        #endregion
    }
}
