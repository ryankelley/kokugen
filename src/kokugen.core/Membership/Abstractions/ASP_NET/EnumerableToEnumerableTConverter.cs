using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Security;
using Kokugen.Core.Membership.Security;

namespace Kokugen.Core.Membership.Abstractions.ASP_NET
{
    public class EnumerableToEnumerableTConverter<TCollectionType, TItemType> : TypeConverter where TCollectionType : IEnumerable
    {
        public override bool CanConvertTo( ITypeDescriptorContext context, Type destinationType )
        {
            return destinationType.IsAssignableFrom(typeof(IEnumerable<TItemType>))
                       ? true
                       : base.CanConvertTo( context, destinationType );
        }

        public T ConvertTo<T>( object value )
        {
            return (T)ConvertTo( value, typeof(T) );
        }

        public override object ConvertTo( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType )
        {
            var items = (TCollectionType)value;
            var destination = new List<TItemType>();
            foreach( var item in items )
                destination.Add((TItemType)item);
            return destination;
        }
    }

    public class MembershipUserCollectionToIUserConverter : EnumerableToEnumerableTConverter<MembershipUserCollection, IUser>
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            var items = (MembershipUserCollection)value;
            var destination = new List<IUser>();
            foreach (MembershipUser item in items)
                destination.Add(new User(item.UserName,item.Email,item.ProviderUserKey));
            return destination;
        }
    }
}