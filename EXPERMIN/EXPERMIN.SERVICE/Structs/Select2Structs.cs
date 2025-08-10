﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERIVICE.Structs
{
    public class Select2Structs
    {
        public struct Child
        {
            public bool? Disabled { get; set; }
            public int? Id { get; set; }
            public bool? Selected { get; set; }
            public string Text { get; set; }
        }

        public struct Pagination
        {
            public bool More { get; set; }
        }

        public struct Result
        {
            public IEnumerable<Child> Children { get; set; }
            public bool? Disabled { get; set; }
            public dynamic Id { get; set; }
            public bool? Selected { get; set; }
            public string Text { get; set; }
        }

        public struct RequestParameters
        {
            public int CurrentPage { get; set; }
            public string Query { get; set; }
            public string RequestType { get; set; }
            public string SearchTerm { get; set; }
        }

        public struct ResponseParameters
        {
            public Pagination Pagination { get; set; }
            public IEnumerable<Result> Results { get; set; }
        }
    }
}
