using Microsoft.AspNetCore.Mvc;
using ServiceBus.Sender.Services;
using SharedModels;
using System;
using System.Threading.Tasks;

namespace ServiceBus.Sender.Controllers
{
    [ApiController]
    [Route("api/Message")]
    public class MessageController : ControllerBase
    {
        private readonly IMessagePublisher _messagePublisher;

        public MessageController(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        [HttpPost("PublishEmployee")]
        public async Task<IActionResult> PublishEmployee(EmployeeInputModel model)
        {
            var employee = new EmployeeCreated
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Number = model.Number,
            };

            await _messagePublisher.SendMessageAsync(employee);

            return Ok();
        }

        [HttpPost("PublishCustomer")]
        public async Task<IActionResult> PublishCustomer(CustomerInputModel model)
        {
            var employee = new CustomerCreated
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Gender = model.Gender,
            };

            await _messagePublisher.SendMessageAsync(employee);

            return Ok();
        }

    }
}