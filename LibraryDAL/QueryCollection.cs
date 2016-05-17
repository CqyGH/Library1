using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace LibraryDAL
{
    internal class QueryCollection
    {
        private List<Query> QuerySet;
        private static readonly object lockHelper = new object();

        public List<Query> GetQuerySet(string typeName)
        {
            lock (lockHelper)
            {
                QuerySet = ReadQuerySet(typeName);
                return QuerySet;
            }
        }

        private List<Query> ReadQuerySet(string typeName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();//读取嵌入式资源
            string filePath = "LibraryDAL.DBConfiguration." + typeName + ".xml";
            Stream configStream = asm.GetManifestResourceStream(filePath);
            XmlReader reader = XmlReader.Create(configStream);
            XDocument doc = XDocument.Load(reader);
            var Result = from q in doc.Descendants("Query")
                         select new Query
                         {
                             Name = q.Attribute("name").Value,
                             SQL = q.Element("QuerySQL").Value,
                             Parameters = (from p in q.Element("Parameters").Descendants("Parameter")
                                           select new Parameter
                                           {
                                               ParameterName = p.Value,
                                               Type = p.Attribute("type").Value
                                           }).ToList()
                         };
            return Result.ToList();
        }
    }
}
