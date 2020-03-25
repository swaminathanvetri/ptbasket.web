using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ptbasket.api.Repository;
using ptbasket.models;

namespace ptbasket.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderRepository orderRepository;
        public OrderController(IOrderRepository _orderRepository)
        {
            orderRepository = _orderRepository;
        }

        // GET: api/Order
        [HttpGet]
        public IActionResult Get()
        {
            var userId = GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                var myOrders = orderRepository.GetMyOrders(userId);
                return myOrders != null && myOrders.Count > 0 ? Ok(myOrders) : (IActionResult)NotFound();
            }
            else
            {
                return BadRequest();
            }
        }

        // GET: api/Order/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            var order = orderRepository.Get(id);
            if (order != null && isOrderUser(order))
            {
                return Ok(order);
            }
            else
            {
                return (IActionResult)NotFound();
            }
        }

        // POST: api/Order
        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            try
            {
                if (isOrderUser(order))
                {
                    var orderId = orderRepository.Create(order);
                    return Created(orderId, orderId);
                }
                else
                {
                    return Forbid();
                }
            }
            catch (Exception)
            {
                return NoContent();
            }
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Order order)
        {
            string orderId = string.Empty;
            if (isAdminUser() || (isOrderUser(order) && order.Status == OrderStatus.Cancelled))
            {
                orderId = orderRepository.Update(order);
                return Created(orderId, orderId);
            }
            else
            {
                return StatusCode(403);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (isAdminUser())
            {
                var deleteStatus = orderRepository.Delete(id);
                return deleteStatus == 0 ? Ok() : (IActionResult)NotFound();
            }
            else
            {
                return Forbid();
            }
        }

        private string GetUserId()
        {
            string userId = string.Empty;

            if (Request.Headers.Keys.Contains(Constants.UserIdKey))
            {
                Request.Headers.TryGetValue(Constants.UserIdKey, out var userIdValue);
                userId = userIdValue.ToString();
            }
            return userId;
        }

        private bool isAdminUser()
        {
            return string.Compare(GetUserId(), Constants.AdminUser, true) == 0 ? true : false;
        }

        private bool isOrderUser(Order order)
        {
            return string.Compare(GetUserId(), order.FlatNumber, true) == 0 ? true : false;
        }
    }
}
