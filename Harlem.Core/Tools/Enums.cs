using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Harlem.Core.Tools
{
    public static class Enums
    {
        public enum StockType : ushort
        {
            [Description("Bilinmiyor")]
            None = 0,
            [Description("Adet")]
            Adet = 1,
            [Description("Kilogram")]
            Kilogram = 2,
            [Description("Paket")]
            Paket = 3
        }
        public enum BLLResultType : ushort
        {
            Success = 1,
            Error = 2,
            Empty = 3

        }
        public enum ViewStatus : ushort
        {
            Insert = 1,
            Update = 2,
            Empty = 3

        }

        public enum PaymentType : ushort
        {

        }

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            try
            {
                if (e is Enum)
                {
                    Type type = e.GetType();
                    Array values = System.Enum.GetValues(type);

                    foreach (ushort val in values)
                    {
                        if (val == e.ToInt32(CultureInfo.InvariantCulture))
                        {
                            var memInfo = type.GetMember(type.GetEnumName(val));
                            var descriptionAttribute = memInfo[0]
                                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                .FirstOrDefault() as DescriptionAttribute;

                            if (descriptionAttribute != null)
                            {
                                return descriptionAttribute.Description;
                            }
                        }
                    }
                }
                return null; // could also return string.Empty

            }
            catch (Exception ex)
            {

                //TODO : Loglama Yap
                return null;
            }
        }
    }
}
