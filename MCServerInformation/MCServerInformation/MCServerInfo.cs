// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
using System.Collections.Generic;

public class Description
{
    public string text { get; set; }
}

public class Sample
{
    public string id { get; set; }
    public string name { get; set; }
}

public class Players
{
    public int max { get; set; }
    public int online { get; set; }
    public List<Sample> sample { get; set; }
}

public class Version
{
    public string name { get; set; }
    public int protocol { get; set; }
}

public class MCServerInfo
{
    public Description description { get; set; }
    public Players players { get; set; }
    public Version version { get; set; }
    public string favicon { get; set; }
}

