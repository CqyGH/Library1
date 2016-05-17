using System.Collections.Generic;

namespace LibraryDAL
{
    internal class Query
    {
        public string Name { get; set; }
        public string SQL { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
}
