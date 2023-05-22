using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;
using SearchResult = Common.SearchResult;

namespace ApiSearch.Controllers;
[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<SearchResult> Search(string terms, int numberOfResults)
    {
        var mSearchLogic = new SearchLogic(new Database());
        var result = new SearchResult();
        
        var wordIds = new List<int>();
        var searchTerms = terms.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        foreach (var t in searchTerms)
        {
            int id = mSearchLogic.GetIdOf(t);
            if (id != -1)
            {
                wordIds.Add(id);
            }
            else
            {
                result.IgnoredTerms.Add(t);
            }
        }

        DateTime start = DateTime.Now;

        var docIds = await mSearchLogic.GetDocumentsAsync(wordIds);

        // get details for the first 10             
        var top = new List<int>();
        foreach (var p in docIds.GetRange(0, Math.Min(numberOfResults, docIds.Count)))
        {
            top.Add(p.Key);
        }

        result.ElapsedMlliseconds = (DateTime.Now - start).TotalMilliseconds;

        int idx = 0;
        foreach (var doc in await mSearchLogic.GetDocumentDetailsAsync(top))
        {
            result.Documents.Add(new Document{Id = idx+1, Path = doc, NumberOfOccurences = docIds[idx].Value});
            idx++;
        }

        return result;
    }
}