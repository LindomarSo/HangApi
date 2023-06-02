namespace HangFireApi.Service;

public class Action
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public Action(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public void RegisterMessage(string message)
    {
        var mensagem = $"{Environment.NewLine} EXECUTOU {message} {DateTime.Now}";
        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Resouces", "loghang.txt");
        System.IO.File.AppendAllText(path, mensagem);
    }
}
 