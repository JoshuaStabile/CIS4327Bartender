using Newtonsoft.Json;
using CIS4327_Bartender.Infrastructure;
using CIS4327_Bartender.Services.Interfaces;

namespace CIS4327_Bartender.Models.Cart
{
    public class SessionCartModel : Cart
    {
        [JsonIgnore]
        public ISession Session { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>() ? .HttpContext.Session;
            SessionCartModel cart = session?.GetJson<SessionCartModel>("Cart") ?? new SessionCartModel();
            cart.Session = session;
            return cart;
        }

        public override void AddItem(Data.Cocktail cocktail)
        {
            base.AddItem(cocktail);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(Data.Cocktail cocktail)
        {
            base.RemoveLine(cocktail);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }



    }

    

}
