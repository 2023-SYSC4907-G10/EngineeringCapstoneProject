using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using UnityEditor;

class MetaData
{

    public const string TAG_NAME = "Name",
        TAG_TYPE = "Type",
        TAG_SIZE = "FileSize",
        TAG_DESCRIPTION = "Description",
        TAG_AUTHOR = "Author",
        TAG_ORIGIN = "Origin",
        TAG_CONTENT = "Content",
        TAG_TIME_TILL_INFECTION = "InfectionTime";

    public const float NOT_VIRUS_TIME = -1;

    public string Name { get; set; }
    public string Type { get; set; }
    public string Size { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public string Origin { get; set; }
    public string Content { get; set; }
    public float TimeTillInfection { get; set; }

    public bool isVirus { get { return TimeTillInfection != NOT_VIRUS_TIME; } }


    public static MetaData FromXml(XElement element)
    {
        var output = new MetaData();
        output.Name = element.Element(TAG_NAME).Value;
        output.Type = element.Element(TAG_TYPE).Value;
        output.Size = element.Element(TAG_SIZE).Value;
        output.Description = element.Element(TAG_DESCRIPTION).Value;
        output.Author = element.Element(TAG_AUTHOR).Value;
        output.Origin = element.Element(TAG_ORIGIN).Value;
        output.Content = element.Element(TAG_CONTENT).Value;
        output.TimeTillInfection = float.Parse(element.Element(TAG_TIME_TILL_INFECTION).Value);

        return output;
    }

    public static IList<MetaData> ListFromXml(XElement element)
    {
        var output = new List<MetaData>();
        foreach (var file in element.Elements())
        {
            output.Add(FromXml(file));
        }
        return output;
    }

    public XElement toXml()
    {
        var output = new XElement("MetaData",
            new XElement(TAG_NAME, Name),
            new XElement(TAG_TYPE, Type),
            new XElement(TAG_SIZE, Size),
            new XElement(TAG_DESCRIPTION, Description),
            new XElement(TAG_AUTHOR, Author),
            new XElement(TAG_ORIGIN, Origin),
            new XElement(TAG_CONTENT, Content),
            new XElement(TAG_TIME_TILL_INFECTION, TimeTillInfection));

        return output;
    }


    public static XElement listToXml(IList<MetaData> files) 
    {
        var output = new XElement("MetaDataCollection");

        foreach (var file in files) 
        {
            output.Add(file.toXml());
        }
        return output;
    
    }

    public override string ToString()
    {
        var output = $"Name: {Name}\n";
        output += $"Type: {Type}\n";
        output += $"Size: {Size}\n";
        output += $"Description: {Description}\n";
        output += $"Author: {Author}\n";
        output += $"Origin: {Origin}\n";
        output += $"Content: {Content}";
        return output;
    }

}
