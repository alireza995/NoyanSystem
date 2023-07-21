using Application.Customers;
using Application.Customers.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private IActionResult HandleResult<T>(T? result)
    {
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        AddCustomerDto customer,
        CancellationToken ct = default
    )
    {
        return HandleResult(
            await _mediator.Send(
                new Add.Command
                {
                    Customer = customer
                },
                ct
            )
        );
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(
        int id,
        CancellationToken ct = default
    )
    {
        return HandleResult(
            await _mediator.Send(
                new Detail.Query
                {
                    Id = id
                },
                ct
            )
        );
    }

    [HttpGet]
    public async Task<IActionResult> List(
        string? searchText,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default
    )
    {
        return HandleResult(
            await _mediator.Send(
                new List.Query
                {
                    SearchText = searchText,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                },
                ct
            )
        );
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken ct = default
    )
    {
        await _mediator.Send(
            new Delete.Command
            {
                Id = id
            },
            ct
        );

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(
        int id,
        UpdateCustomerDto customer,
        CancellationToken ct = default
    )
    {
        await _mediator.Send(
            new Update.Command
            {
                Id = id,
                Customer = customer
            },
            ct
        );

        return Ok();
    }

    [HttpPut("setName")]
    public async Task<IActionResult> SetName(
        int id,
        string firstName,
        string lastName,
        CancellationToken ct = default
    )
    {
        await _mediator.Send(
            new SetName.Command
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            },
            ct
        );

        return Ok();
    }
}