using System.Collections.Generic;

namespace StrategyCorps.CodeSample.Models
{
    public class AlternativeTitlesResponseDto
    {
        public int Id { get; set; }
        public IList<TitleResultDto> Titles { set; get; }
    }
}
