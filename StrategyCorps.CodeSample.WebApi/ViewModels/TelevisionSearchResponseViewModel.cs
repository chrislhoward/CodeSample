using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrategyCorps.CodeSample.WebApi.ViewModels
{
    public class TelevisionSearchResponseViewModel
    {
        public IList<TelevisionResultViewModel> Results { get; set; }

        public int Page { get; set; }

        public int TotalResults { get; set; }

        public int TotalPages { get; set; }
    }
}