using LU2_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class WizardController : Controller
{
    private readonly ILogger<WizardController> _logger;

    public WizardController(ILogger<WizardController> logger)
    {
        _logger = logger;
    }

    [HttpPut(template: "{id}", Name = "UpdateWizard")]
    [Authorize]
    public async Task<IActionResult> UpdateWizard(int id, [FromBody] WizardModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Update logic here
        _logger.LogInformation($"Updating wizard with ID: {id}");

        var result = await UpdateWizardInDatabaseAsync(id, model);
        if (result)
        {
            return Ok(new { Message = "Wizard updated successfully." });
        }

        return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Failed to update wizard." });
    }

    private async Task<bool> UpdateWizardInDatabaseAsync(int id, WizardModel model)
    {
        // Simuleer database-update
        await Task.Delay(100); // Simuleert een asynchrone bewerking
        return true; // Geef terug dat de update geslaagd is
    }
}