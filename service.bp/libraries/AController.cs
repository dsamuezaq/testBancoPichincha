using Microsoft.AspNetCore.Mvc;
using model.bp;

namespace service.bp.libraries
{
    public class AController<T> : ControllerBase
    {
        #region variable Class
        protected string TableName;
        protected string ControllerName;
        #endregion
        ContextBancoPichincha Context { get; }
       //rivate readonly AppSettings _appSettings;
       //rivate readonly IConfiguration configuration;
       //IMapper _mapper;
        public AController(ContextBancoPichincha _contextBancoPichincha )
        {
            //_mapper = mapper;
            Context = _contextBancoPichincha;
         //   _appSettings = appSettings.Value;


        }
    
    }
}
