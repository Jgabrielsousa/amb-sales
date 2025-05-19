using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.Application;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of UsersController
        /// </summary>
        /// <param name="mediator">The mediator instance</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new Sale
        /// </summary>
        /// <param name="request">The Sale creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleRequest>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateSaleCommand>(request);

            Result response = await _mediator.Send(command, cancellationToken);

            if (!response.Success)
                return BadRequest(response);

            return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
            {
                Success = response.Success,
                Message = response.Message,
                Data = _mapper.Map<CreateSaleResponse>(response.Data)
            });
        }

        /// <summary>
        /// Retrieve a Sale by ID
        /// </summary>
        /// <param name="id">The unique identifier of the Sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The Sale details if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetSaleRequest(id);
            var validator = new GetSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetSaleCommand>(request);
            Result response = await _mediator.Send(command, cancellationToken);

            if (!response.Success)
                return NotFound(response);

                return Ok(new ApiResponseWithData<GetSaleResponse>
                {
                    Success = response.Success,
                    Message = response.Message,
                    Data = _mapper.Map<GetSaleResponse>(response.Data)
                });
        }

        /// <summary>
        /// Delete a Sale by ID
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the sale was deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteSaleRequest { Id = id };
            var validator = new DeleteSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteSaleCommand>(request.Id);
            

            Result response = await _mediator.Send(command, cancellationToken);

            if (!response.Success)
                return NotFound(response);

            return Ok(new ApiResponse
            {
                Success = response.Success,
                Message = response.Message
            });
        }

        /// <summary>
        /// Update a Sale by ID
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete</param>
        /// <param name="request">The Sale update request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the sale was updated</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
        {
            request.SetId(id);
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateSaleCommand>(request);
            Result response = await _mediator.Send(command, cancellationToken);

            if (!response.Success)
                return NotFound(response);

            return Accepted(string.Empty, new ApiResponseWithData<UpdateSaleResponse>
            {
                Success = response.Success,
                Message = response.Message,
                Data = _mapper.Map<UpdateSaleResponse>(response.Data)
            });
        }


        private IActionResult NotFound(Result response)
        {
            return NotFound(new ApiResponseWithData<Result>
            {
                Success = response.Success,
                Message = response.Message,
                Data = null
            });
        }

        private IActionResult BadRequest(Result response)
        {
            return BadRequest(new ApiResponseWithData<Result>
            {
                Success = response.Success,
                Message = response.Message,
                Data = null
            });
        }
    }
}