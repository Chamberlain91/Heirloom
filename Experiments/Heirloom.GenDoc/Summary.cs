using System.Collections.Generic;

namespace Heirloom.GenDoc
{
    public abstract class Element
    // https://docs.microsoft.com/en-us/dotnet/csharp/codedoc
    {
        protected Element()
        {
            Children = new List<Element>();
        }

        public List<Element> Children { get; }

        public string Value { get; set; }
    }

    public class TextElement : Element
    {
        public TextElement(string value)
        {
            Value = value;
        }
    }

    public class SummaryElement : Element
    {
        // Nothing Special
    }

    public class RemarksElement : Element
    {
        // Nothing Special
    }

    public class ReturnsElement : Element
    {
        // Nothing Special
    }

    public class ValueElement : Element
    {
        // Nothing Special
    }

    public class ExampleElement : Element
    {
        // Nothing Special
        // Code Child?
    }

    public class ParaElement : Element
    {
        // Nothing Special
    }

    public class CodeElement : Element
    {
        // Nothing Special
    }

    public class CElement : Element
    {
        // Nothing Special
    }

    public class ExceptionElement : Element
    {
        public string CRef { get; }
    }

    public class SeeElement : Element
    {
        public string CRef { get; }
    }

    public class SeeAlsoElement : Element
    {
        public string CRef { get; }
    }

    public class ParamElement : Element
    {
        public string Name { get; }
    }

    public class TypeParamElement : Element
    {
        public string Name { get; }
    }

    public class ParamRefElement : Element
    {
        public string Name { get; }
    }

    public class TypeParamRefElement : Element
    {
        public string Name { get; }
    }

    public class InheritDocElement : Element
    {
        // Nothing Special
    }

    public class ListElement : Element
    {
        public string Type { get; }
        // items
    }

    public class ListItemElement : Element
    {
    }

    public class ListTermElement : Element
    {
    }

    public class ListDescriptionElement : Element
    {
    }

    public class ListHeaderElement : Element
    {
    }
}
