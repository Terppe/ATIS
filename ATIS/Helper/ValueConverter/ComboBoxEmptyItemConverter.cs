using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ATIS.Ui.Helper.ValueConverter
{
    public class ComboBoxEmptyItemConverter : IValueConverter
    {
        /// <summary>
        /// this object is the empty item in the combobox. A dynamic object that
        /// returns null for all property request.
        /// </summary>
        private class EmptyItem : DynamicObject
        {
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                // just set the result to null and return true
                result = null;
                return true;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // assume that the value at least inherits from IEnumerable
            // otherwise we cannot use it.
            var container = value as IEnumerable;

            if (container != null)
            {
                // everything inherits from object, so we can safely create a generic IEnumerable
                var genericContainer = container.OfType<object>();
                // create an array with a single EmptyItem object that serves to show en empty line
                IEnumerable<object> emptyItem = new object[] { new EmptyItem() };
                // use Linq to concatenate the two enumerable
                return emptyItem.Concat(genericContainer);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
