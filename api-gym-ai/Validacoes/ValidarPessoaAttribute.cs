using api_gym_ai.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidarPessoaAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("pessoa", out var pessoaObj) && pessoaObj is Pessoa pessoa)
        {
            if (pessoa.InfoCorporais == null || pessoa.Objetivo == null)
            {
                context.Result = new BadRequestObjectResult("As informa��es do usu�rio n�o podem ser nulas.");
            }
        }
        else
        {
            context.Result = new BadRequestObjectResult("O objeto Pessoa � obrigat�rio.");
        }
    }
}
