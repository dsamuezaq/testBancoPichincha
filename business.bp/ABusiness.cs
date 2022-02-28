using model.bp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transversal.bp.SystemViewModel;

namespace business.bp
{
    public class ABusiness
    {

        protected ContextBancoPichincha Context { get; }
        protected ReplyViewModel reply { get; set; }
        //protected IMapper _mapper;
        protected ABusiness(ContextBancoPichincha _chariotContext
            //IMapper mapper
            )
        {
            Context = _chariotContext;
            reply=   new ReplyViewModel();
            //  _mapper = mapper;

        }

        protected void  FillErrorReplyViewModel(string Error) {
            reply.messege = Error;
            reply.status = "Error";

        }
        protected void FillSuccessReplyViewModel(string message, object _data)
        {
            reply.messege = message;
            reply.status = "Ok";
            reply.data = _data;

        }
    }
}
