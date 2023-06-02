using Hangfire;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HangFireApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HangController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly Service.Action _action;

    public HangController(ILogger<HangController> logger, Service.Action action)
    {
        _logger = logger;
        _action = action;
    }

    [HttpGet("RodaUmaVez")]
    public IActionResult Get()
    {
        _logger.LogInformation("Rodando apenas uma vez");

        var jobFireForget = BackgroundJob.Enqueue(() => _action.RegisterMessage("RodaUmaVez"));

        return Ok();
    }

    // Os trabalhos são executados apenas uma vez, após um determinado intervalo de tempo 
    [HttpGet("RodaAposUmTempo")]
    public IActionResult RodaAposUmTempo()
    {
        _logger.LogInformation("Rodando após um tempo");

        var jobDelayed = BackgroundJob.Schedule(() => _action.RegisterMessage("RodaAposUmTempo"), TimeSpan.FromSeconds(10));

        return Ok();
    }

    // As continuações são executadas quando seu trabalho pai foi concluido
    [HttpGet("RodaAposUmTempoContinuo")]
    public IActionResult RodaAposUmTempoContinuo()
    {
        _logger.LogInformation("Rodando após um tempo continuo");

        var jobDelayed = BackgroundJob.Schedule(() => _action.RegisterMessage("RodaAposUmTempo"), TimeSpan.FromSeconds(10));

        BackgroundJob.ContinueJobWith(jobDelayed, () => _action.RegisterMessage("RodaAposUmTempoContinuo"));

        return Ok();
    }

    [HttpGet("RodaSempre")]
    public IActionResult RodaSempre()
    {
        _logger.LogInformation("Rodando Sempre");

        RecurringJob.AddOrUpdate("RodaSempre", () => _action.RegisterMessage("RodarSempre"), "* */1 * * *");

        return Ok();
    }
}
