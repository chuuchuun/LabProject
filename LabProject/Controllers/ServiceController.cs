﻿using LabProject.Application.Dtos.ServiceDtos;
using LabProject.Application.Features.Services;
using LabProject.Application.Features.Services.Commands;
using LabProject.Application.Features.Services.Queries;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController(IMediator mediator): ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        /// <summary>
        /// Gets all services in the system.
        /// </summary>
        /// <returns>List of all services.</returns>
        /// <response code="200">OK – A list of all services.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
        {
            return Ok(await _mediator.Send(new GetAllServicesQuery()));
        }

        /// <summary>
        /// Gets a specific service by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the service.</param>
        /// <returns>The service matching the ID.</returns>
        /// <response code="200">OK – The service matching the ID.</response>
        /// <response code="404">Not Found – No service found with the given ID.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetServiceById([FromRoute] int id)
        {
            var service = await _mediator.Send(new GetServiceByIdQuery(id));
            if (service is null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        /// <summary>
        /// Creates a new service.
        /// </summary>
        /// <param name="service">The service data to create.</param>
        /// <returns>The created service with assigned ID.</returns>
        /// <response code="201">Created – The created service with assigned ID.</response>
        [HttpPost]
        public async Task<ActionResult<ServiceDto>> CreateService([FromBody] ServiceCreateDto service)
        {
            if(service is null)
            {
                return BadRequest("Service data cannot be null.");
            }
            var newId = await _mediator.Send(new CreateServiceCommand(service));
            if (newId <= 0)
            {
                return BadRequest("Failed to create service.");
            }
            var createdService = await _mediator.Send(new GetServiceByIdQuery(newId));
            return CreatedAtAction(nameof(GetServiceById), new { id = newId}, createdService);
        }

        /// <summary>
        /// Updates an existing service.
        /// </summary>
        /// <param name="id">The ID of the service to update.</param>
        /// <param name="updatedService">The updated service data.</param>
        /// <returns>The updated service.</returns>
        /// <response code="200">OK – The updated service.</response>
        /// <response code="404">Not Found – No service found with the given ID.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateService([FromRoute] int id, [FromBody] ServiceUpdateDto updatedService)
        {
            if (updatedService is null)
            {
                return BadRequest("Updated service data cannot be null.");
            }
            var success = await _mediator.Send(new UpdateServiceCommand(id, updatedService));
            if (!success)
            {
                return NotFound("Service not found or update failed.");
            }
            var service = await _mediator.Send(new GetServiceByIdQuery(id));
            return Ok(service);
        }

        /// <summary>
        /// Deletes a service by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the service to delete.</param>
        /// <returns>Status of the deletion.</returns>
        /// <response code="200">OK – Deletion successful.</response>
        /// <response code="404">Not Found – No service found with the given ID.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteService([FromRoute] int id)
        {
            var success = await _mediator.Send(new DeleteServiceCommand(id));
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
