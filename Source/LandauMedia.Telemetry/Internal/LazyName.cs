using System;
using System.Linq;
using System.Reflection;

namespace LandauMedia.Telemetry.Internal
{
    class LazyName
    {
        readonly Type _baseType;
        readonly Func<string> _getCurrentBaseName;

        public LazyName(Func<string> getCurrentBaseName, Type baseType)
        {
            _getCurrentBaseName = getCurrentBaseName;
            _baseType = baseType;
        }

        public Lazy<string> Get(object thisHandle)
        {
            return new Lazy<string>(() =>
            {
                var name = _baseType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                    .Where(p => p.GetValue(null, null) == thisHandle).Select(p => p.Name).FirstOrDefault();
                if(name == null)
                    name = _baseType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                        .Where(f => f.GetValue(null) == thisHandle).Select(f => f.Name).FirstOrDefault();

                var subType = _baseType.FullName;
                var subTypesIndex = subType.IndexOf('+');
                if(subTypesIndex > -1)
                    name = subType.Substring(subTypesIndex + 1).Replace('+', '.') + "." + name;

                return ( _getCurrentBaseName() + "." + name ).ToLowerInvariant();
            });
        }
    }
}