﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Angeronia.Converters
{
    public class UriConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string uriString = String.Join(string.Empty, values);
            return new Uri(uriString);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
