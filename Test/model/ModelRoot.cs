using System.Collections.Generic;
using Test.model;

namespace Test
{
    class ModelRoot
    {
        public List<ModelProduct> Products { get; set; }
        public List<ModelCategory> Categories { get; set; }
    }
}
