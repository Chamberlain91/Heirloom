using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Heirloom.Math;

namespace Maintenance.TypeDump
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DumpAssemblyTypes("Heirloom.Collections");
            DumpAssemblyTypes("Heirloom.Collections.Spatial");
            DumpAssemblyTypes("Heirloom.Drawing");
            DumpAssemblyTypes("Heirloom.Game");
            DumpAssemblyTypes("Heirloom.Math");
            DumpAssemblyTypes("Heirloom.Sound");
            DumpAssemblyTypes("Heirloom.IO");
        }

        public static void DumpAssemblyTypes(string name)
        {
            var assembly = Assembly.Load(name);
            using var fs = new FileStream($"./{assembly.GetName().Name}.txt", FileMode.Create);
            using var wr = new StreamWriter(fs);

            foreach (var type in assembly.ExportedTypes)
            {
                if (!type.IsVisible) { continue; }

                var typeType = "???";
                if (type.IsInterface) { typeType = "interface"; }
                if (type.IsValueType) { typeType = "struct"; }
                if (type.IsClass) { typeType = "class"; }
                if (type.IsEnum) { typeType = "enum"; }

                wr.WriteLine($"{typeType} {EmitType(type)}\n{{");

                foreach (var fieldInfo in type.GetFields())
                {
                    if (fieldInfo.IsSpecialName) { continue; }

                    var access = "";
                    if (fieldInfo.IsStatic) { access += "static "; }
                    if (fieldInfo.IsInitOnly) { access += "readonly "; }
                    if (fieldInfo.IsLiteral) { access += "const "; }

                    wr.WriteLine($"    {access}{EmitType(fieldInfo.FieldType)} {fieldInfo.Name};");
                }

                foreach (var propertyInfo in type.GetProperties())
                {
                    if (propertyInfo.IsSpecialName) { continue; }

                    var access = "";
                    if (propertyInfo.GetAccessors().FirstOrDefault()?.IsStatic ?? false) { access += "static "; }

                    var getset = "";
                    if (propertyInfo.CanRead) { getset += "get;"; }
                    if (propertyInfo.CanWrite)
                    {
                        if (getset.Length == 0) { getset += " "; }
                        getset += "set;";
                    }

                    wr.WriteLine($"    {access}{EmitType(propertyInfo.PropertyType)} {propertyInfo.Name} {{ {getset} }}");
                }

                foreach (var eventInfo in type.GetEvents())
                {
                    if (eventInfo.IsSpecialName) { continue; }

                    wr.WriteLine($"    event {EmitType(eventInfo.EventHandlerType)} {eventInfo.Name};");
                }

                WriteMethods(wr, type);

                wr.WriteLine("}\n");
            }
        }

        private static bool WriteMethods(StreamWriter wr, Type type)
        {
            var methods = type.GetMethods();

            foreach (var method in methods)
            {
                if (method.GetBaseDefinition() != method) { continue; }
                if (method.IsSpecialName) { continue; } // "op_", "get_", "set_", etc

                var access = "";
                if (method.IsStatic) { access += "static "; }

                var args = string.Join(", ", method.GetParameters().Select(param => EmitParameter(param)));
                wr.WriteLine($"    {access}{EmitType(method.ReturnType)} {method.Name}({args}) {{ ... }}");
            }

            return methods.Length > 0;
        }

        private static string EmitType(Type type)
        {
            var name = type.Name;

            // enum, etc

            if (type.IsGenericType)
            {
                name = $"{name}<{string.Join(", ", type.GetGenericArguments().Select(g => EmitType(g)))}>";
            }

            if (type.IsArray) { name = $"{name}[]"; }
            return name;
        }

        private static string EmitParameter(ParameterInfo parameterInfo)
        {
            var type = parameterInfo.ParameterType;

            if (type.HasElementType)
            {
                type = type.GetElementType();

                var name = $"{EmitType(type)} {parameterInfo.Name}";
                var isRef = type.IsByRef;

                // Augment with in, out and ref
                if (isRef && parameterInfo.IsOut) { name = $"out {name}"; }
                else if (isRef && !parameterInfo.IsOut) { name = $"ref {name}"; }
                else if (parameterInfo.IsIn) { name = $"in {name}"; }

                if (type.IsDefined(typeof(ParamArrayAttribute), false)) { name = $"params {name}[]"; }

                // Default value
                if (parameterInfo.HasDefaultValue) { name = $"{name} = {parameterInfo.DefaultValue}"; }

                return name;
            }
            else
            {
                var name = $"{EmitType(type)} {parameterInfo.Name}";

                if (type.IsDefined(typeof(ParamArrayAttribute), false)) { name = $"params {name}[]"; }

                // Default value
                if (parameterInfo.HasDefaultValue) { name = $"{name} = {parameterInfo.DefaultValue}"; }

                return name;
            }
        }
    }
}
