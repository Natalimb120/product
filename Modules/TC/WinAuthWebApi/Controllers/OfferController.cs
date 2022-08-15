using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Product.Ck.WinAuthWebApi.Controllers
{
    [RoutePrefix("api/ck/offers")]
    public class OfferController : ApiController
    {
        private readonly IMediator _mediator;

        public OfferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("run")]
        public IHttpActionResult RunOffer()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (((WindowsIdentity) HttpContext.Current.User.Identity).Impersonate())
                {
                    try
                    {
                        string result = "Run";

                        return Ok(result);
                    }
                    catch (ConnectionException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }

            return BadRequest("Błąd użytkownika.");
        }

        [HttpGet]
        [Route("{offerId}")]
        public async Task<IHttpActionResult> GetOfferData([FromUri] Guid offerId)
        {
            var commandResult =
                await _mediator.Offer(new GetOfferQuery(offerId));

            return commandResult.Match<IHttpActionResult>(error => BadRequest(error.Message), Ok);
        }

        [HttpGet]
        [Route("{offerId}/tp")]
        public async Task<IHttpActionResult> GetTempClients([FromUri] Guid offerId)
        {
            var commandResult =
                await _mediator.Offer(new GetTempClientsQuery(offerId));

            return commandResult.Match<IHttpActionResult>(error => BadRequest(error.Message), Ok);
        }

        [HttpPost]
        [Route("{offerId}/tp")]
        public async Task<IHttpActionResult> SetTempClient([FromUri] Guid offerId,
            [FromBody] SetTempClientDto setTempClientDto)
        {
            var commandResult =
                await _mediator.Offer(new SetTempClientCommand(offerId,
                    setTempClientDto.TempClientId,
                    setTempClientDto.Comment));

            return commandResult.Match<IHttpActionResult>(error => BadRequest(error.Message), Ok);
        }
    }
}