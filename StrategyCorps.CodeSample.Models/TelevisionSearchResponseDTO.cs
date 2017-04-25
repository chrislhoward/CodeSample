using System.Collections.Generic;

namespace StrategyCorps.CodeSample.Models
{
    public class TelevisionSearchResponseDTO
    {
        public IList<TelevisionResultDTO> Results { get; set; }

        public int Page { get; set; }

        public int TotalResults { get; set; }

        public int TotalPages { get; set; }
    }
}
