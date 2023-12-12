namespace RPVRS.Labs.Lab2.ResponseModels;

public class ResultObject<TModel> where TModel : class
{
    public string Status { get; set; }
    public TModel? Result { get; set; }

    public IReadOnlyCollection<LinkDto> Links { get; set; }

    public ResultObject(string status,TModel? result, IReadOnlyCollection<LinkDto>? links = null)
    {
        Status = status;
        Result = result;
        Links = links ?? Array.Empty<LinkDto>();
    }
}

public class LinkDto
{
    public string Href { get; set; }
    public string Rel { get; set; }
    public string Method { get; set; }

    public LinkDto(string href, string rel, string method)
    {
        Href = href;
        Rel = rel;
        Method = method;
    }
}