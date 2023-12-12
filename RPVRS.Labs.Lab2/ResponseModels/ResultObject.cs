namespace RPVRS.Labs.Lab2.ResponseModels;

public class ResultObject<TModel> where TModel : class
{
    public string Status { get; set; }
    public TModel? Result { get; set; }

    public ResultObject(string status, TModel? result)
    {
        Status = status;
        Result = result;
    }
}